/////////////////////////////////////////////////////////////////////////////////
//
// Base64 FileType Plugin for Paint.NET
//
// This software is provided under the MIT License:
//   Copyright (c) 2013-2018, 2020, 2021, 2022 Nicholas Hayes
//
// See LICENSE.txt for complete licensing and attribution information.
//
/////////////////////////////////////////////////////////////////////////////////

// Portions of this file has been adapted from:
/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using PaintDotNet;
using System.Text;
using System.Globalization;

namespace Base64FileTypePlugin
{
    public sealed class Base64FileType : FileType
    {
        public Base64FileType() : base("Base64",
                                       FileTypeFlags.SupportsSaving | FileTypeFlags.SupportsLoading,
                                       new string[] { ".b64" })
        {
        }

        private static readonly string[] ImageFormats = new string[] { "bmp", "png", "jpeg", "gif" };
        private static readonly string[] DataEndMarkers = new string[2] { "alt=\"", ")" };

        private const string DataURIFormat = "data:image/{0};base64,";

        private enum SavableBitDepths
        {
            Rgba32, // 2^24 colors, plus a full 8-bit alpha channel
            Rgb24,  // 2^24 colors
            Rgb8,   // 256 colors
            Rgba8   // 255 colors + 1 transparent
        }

        protected override SaveConfigToken OnCreateDefaultSaveConfigToken()
        {
            return new Base64SaveConfigToken(FileFormat.Png,
                                             string.Empty,
                                             false,
                                             false,
                                             UriDataType.None,
                                             string.Empty,
                                             new CssTokenData());
        }

        public override SaveConfigWidget CreateSaveConfigWidget()
        {
            return new Base64SaveConfigWidget();
        }

        protected override Document OnLoad(Stream input)
        {
            using (StreamReader sr = new StreamReader(input, Encoding.UTF8))
            {
                string data = sr.ReadToEnd();

                // The base 64 data can optionally be surrounded by single or double quotes.
                bool isQuotedBase64String = false;
                char quoteChar = '\0';

                for (int i = 0; i < ImageFormats.Length; i++)
                {
                    string dataFormat = string.Format(CultureInfo.InvariantCulture, DataURIFormat, ImageFormats[i]);

                    int dataStartIndex = data.IndexOf(dataFormat, StringComparison.OrdinalIgnoreCase);

                    if (dataStartIndex >= 0)
                    {
                        if (dataStartIndex > 0)
                        {
                            char startChar = data[dataStartIndex - 1];

                            if (startChar == '\'' || startChar == '"')
                            {
                                isQuotedBase64String = true;
                                quoteChar = startChar;
                            }
                        }

                        data = data.Remove(0, dataFormat.Length + dataStartIndex);
                        break;
                    }
                }

                if (isQuotedBase64String)
                {
                    int endIndex = data.IndexOf(quoteChar);

                    if (endIndex >= 0)
                    {
                        data = data.Remove(endIndex, data.Length - endIndex).TrimEnd();
                    }
                }
                else
                {
                    for (int i = 0; i < DataEndMarkers.Length; i++)
                    {
                        int uriEndIndex = data.IndexOf(DataEndMarkers[i], StringComparison.OrdinalIgnoreCase);
                        if (uriEndIndex >= 0)
                        {
                            if (uriEndIndex != data.Length)
                            {
                                // Remove any trailing characters.
                                data = data.Remove(uriEndIndex, data.Length - uriEndIndex).TrimEnd();
                            }

                            break;
                        }
                    }
                }

                byte[] bytes = Convert.FromBase64String(data);

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    using (Image image = Image.FromStream(stream))
                    {
                        return Document.FromImage(image);
                    }
                }
            }
        }

        private static EncoderParameters SetEncodeParameters(SavableBitDepths bitDepth)
        {
            int colorDepth = 0;
            switch (bitDepth)
            {
                case SavableBitDepths.Rgba32:
                    colorDepth = 32;
                    break;
                case SavableBitDepths.Rgb24:
                    colorDepth = 24;
                    break;
                case SavableBitDepths.Rgb8:
                case SavableBitDepths.Rgba8:
                    colorDepth = 8;
                    break;
            }

            EncoderParameters encodeOptions = new EncoderParameters(1);
            try
            {
                encodeOptions.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth,
                                                              colorDepth);
            }
            catch (Exception)
            {
                encodeOptions.Dispose();
                throw;
            }

            return encodeOptions;
        }

        private static ImageCodecInfo GetImageCodecInfo(ImageFormat format)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo icf in encoders)
            {
                if (icf.FormatID == format.Guid)
                {
                    return icf;
                }
            }

            return null;
        }

        private static unsafe void SquishSurfaceTo24Bpp(Surface surface)
        {
            byte* dst = (byte*)surface.GetRowAddress(0);
            int byteWidth = surface.Width * 3;
            int stride24bpp = ((byteWidth + 3) / 4) * 4; // round up to multiple of 4
            int delta = stride24bpp - byteWidth;

            for (int y = 0; y < surface.Height; ++y)
            {
                ColorBgra* src = surface.GetRowAddress(y);
                ColorBgra* srcEnd = src + surface.Width;

                while (src < srcEnd)
                {
                    dst[0] = src->B;
                    dst[1] = src->G;
                    dst[2] = src->R;
                    ++src;
                    dst += 3;
                }

                dst += delta;
            }
        }

        private static unsafe Bitmap CreateAliased24BppBitmap(Surface surface)
        {
            int stride = surface.Width * 3;
            int realStride = ((stride + 3) / 4) * 4; // round up to multiple of 4
            return new Bitmap(surface.Width,
                              surface.Height,
                              realStride,
                              PixelFormat.Format24bppRgb,
                              new IntPtr(surface.Scan0.VoidStar));
        }

        private static unsafe void Analyze(Surface scratchSurface,
                                           out bool allOpaque,
                                           out bool all0or255Alpha,
                                           out int uniqueColorCount)
        {
            allOpaque = true;
            all0or255Alpha = true;
            HashSet<ColorBgra> uniqueColors = new HashSet<ColorBgra>();

            for (int y = 0; y < scratchSurface.Height; ++y)
            {
                ColorBgra* srcPtr = scratchSurface.GetRowAddress(y);
                ColorBgra* endPtr = srcPtr + scratchSurface.Width;

                while (srcPtr < endPtr)
                {
                    ColorBgra p = *srcPtr;

                    if (p.A != 255)
                    {
                        allOpaque = false;
                    }

                    if (p.A > 0 && p.A < 255)
                    {
                        all0or255Alpha = false;
                    }

                    if (p.A == 255 && !uniqueColors.Contains(p) && uniqueColors.Count < 300)
                    {
                        uniqueColors.Add(*srcPtr);
                    }

                    ++srcPtr;
                }
            }

            uniqueColorCount = uniqueColors.Count;
        }

        private long GetSavedFileLength(Surface scratchSurface, ImageFormat format, SavableBitDepths bitDepth)
        {
            long size = 0;

            using (MemoryStream ms = new MemoryStream())
            {
                switch (bitDepth)
                {
                    case SavableBitDepths.Rgba32:

                        using (Bitmap temp = scratchSurface.CreateAliasedBitmap())
                        {
                            temp.Save(ms, format);
                        }
                        break;
                    case SavableBitDepths.Rgb24:

                        SquishSurfaceTo24Bpp(scratchSurface);
                        using (Bitmap temp = CreateAliased24BppBitmap(scratchSurface))
                        {
                            temp.Save(ms, format);
                        }
                        break;
                    case SavableBitDepths.Rgb8:
                    case SavableBitDepths.Rgba8:

                        using (Bitmap temp = Quantize(scratchSurface,
                                                      7,
                                                      256,
                                                      bitDepth == SavableBitDepths.Rgba8,
                                                      null))
                        {
                            temp.Save(ms, format);
                        }
                        break;
                }

                size = ms.Length;
            }

            return size;
        }

        private SavableBitDepths ChooseBestBitDepth(Document input, Surface scratchSurface, ImageFormat format)
        {
            scratchSurface.Clear(ColorBgra.Transparent);
            input.Flatten(scratchSurface);

            Analyze(scratchSurface, out bool allOpaque, out bool all0or255Alpha, out int uniqueColorCount);
            HashSet<SavableBitDepths> allowedBitDepths = new HashSet<SavableBitDepths>();

            if (allOpaque || format == ImageFormat.Bmp)
            {
                allowedBitDepths.Add(SavableBitDepths.Rgb24);
                if (uniqueColorCount <= 256)
                {
                    allowedBitDepths.Add(SavableBitDepths.Rgb8);
                }
            }
            else
            {
                allowedBitDepths.Add(SavableBitDepths.Rgba32);
                if (all0or255Alpha && uniqueColorCount <= 255)
                {
                    allowedBitDepths.Add(SavableBitDepths.Rgba8);
                }
            }

            SavableBitDepths bestBitDepth;
            if (allowedBitDepths.Count == 1)
            {
                bestBitDepth = allowedBitDepths.First();
            }
            else
            {
                long totalPixelSize = scratchSurface.Width * scratchSurface.Height;
                if (allowedBitDepths.SetEquals(new SavableBitDepths[] { SavableBitDepths.Rgb8, SavableBitDepths.Rgb24 })
                    && totalPixelSize <= 65536)
                {
                    long rgb8Length;
                    long rgb24Length;

                    try
                    {
                        rgb8Length = GetSavedFileLength(scratchSurface, format, SavableBitDepths.Rgb8);
                        rgb24Length = GetSavedFileLength(scratchSurface, format, SavableBitDepths.Rgb24);
                    }
                    catch (OutOfMemoryException)
                    {
                        return SavableBitDepths.Rgb8;
                    }

                    bestBitDepth = rgb8Length <= rgb24Length ? SavableBitDepths.Rgb8 : SavableBitDepths.Rgb24;
                }
                else if (allowedBitDepths.SetEquals(new SavableBitDepths[] { SavableBitDepths.Rgba8, SavableBitDepths.Rgba32 })
                         && totalPixelSize <= 65536)
                {
                    long rgba8Length;
                    long rgba32Length;

                    try
                    {
                        rgba8Length = GetSavedFileLength(scratchSurface, ImageFormat.Png, SavableBitDepths.Rgb8);
                        rgba32Length = GetSavedFileLength(scratchSurface, ImageFormat.Png, SavableBitDepths.Rgba32);
                    }
                    catch (OutOfMemoryException)
                    {
                        return SavableBitDepths.Rgba8;
                    }

                    bestBitDepth = rgba8Length <= rgba32Length ? SavableBitDepths.Rgba8 : SavableBitDepths.Rgba32;
                }
                else if (allowedBitDepths.Contains(SavableBitDepths.Rgb8))
                {
                    bestBitDepth = SavableBitDepths.Rgb8;
                }
                else if (allowedBitDepths.Contains(SavableBitDepths.Rgba8))
                {
                    bestBitDepth = SavableBitDepths.Rgba8;
                }
                else if (allowedBitDepths.Contains(SavableBitDepths.Rgb24))
                {
                    bestBitDepth = SavableBitDepths.Rgb24;
                }
                else
                {
                    bestBitDepth = SavableBitDepths.Rgba32;
                }
            }

            return bestBitDepth;
        }

        protected override void OnSave(Document input,
                                       Stream output,
                                       SaveConfigToken token,
                                       Surface scratchSurface,
                                       ProgressEventHandler progressCallback)
        {
            Base64SaveConfigToken configToken = (Base64SaveConfigToken)token;

            ImageFormat gdipFormat = null;
            switch (configToken.ImageType)
            {
                case FileFormat.Bmp:
                    gdipFormat = ImageFormat.Bmp;
                    break;
                case FileFormat.Png:
                    gdipFormat = ImageFormat.Png;
                    break;
                default:
                    throw new InvalidEnumArgumentException("configToken.ImageType", (int)configToken.ImageType, typeof(FileFormat));
            }

            SavableBitDepths bitDepth = ChooseBestBitDepth(input, scratchSurface, gdipFormat);

            scratchSurface.Clear(ColorBgra.Transparent);
            input.Flatten(scratchSurface);

            // if bit depth is 24 or 8, then we have to do away with the alpha channel
            // for 8-bit, we must have pixels that have either 0 or 255 alpha
            if (bitDepth == SavableBitDepths.Rgb8 ||
                bitDepth == SavableBitDepths.Rgba8 ||
                bitDepth == SavableBitDepths.Rgb24)
            {
                UserBlendOps.NormalBlendOp blendOp = new UserBlendOps.NormalBlendOp();

                unsafe
                {
                    for (int y = 0; y < scratchSurface.Height; ++y)
                    {
                        ColorBgra* ptr = scratchSurface.GetRowAddressUnchecked(y);
                        ColorBgra* endPtr = ptr + scratchSurface.Width;

                        while (ptr < endPtr)
                        {
                            if (ptr->A < 128 && bitDepth == SavableBitDepths.Rgba8)
                            {
                                ptr->Bgra = 0;
                            }
                            else
                            {
                                *ptr = blendOp.Apply(ColorBgra.White, *ptr);
                            }
                            ptr++;
                        }
                    }
                }
            }

            ImageCodecInfo codecInfo = GetImageCodecInfo(gdipFormat);
            string base64 = null;
            using (MemoryStream ms = new MemoryStream())
            {
                EncoderParameters encoderParams = SetEncodeParameters(bitDepth);

                switch (bitDepth)
                {
                    case SavableBitDepths.Rgba32:

                        using (Bitmap temp = scratchSurface.CreateAliasedBitmap())
                        {
                            temp.Save(ms, codecInfo, encoderParams);
                        }
                        break;
                    case SavableBitDepths.Rgb24:

                        SquishSurfaceTo24Bpp(scratchSurface);
                        using (Bitmap temp = CreateAliased24BppBitmap(scratchSurface))
                        {
                            temp.Save(ms, codecInfo, encoderParams);
                        }
                        break;
                    case SavableBitDepths.Rgb8:
                    case SavableBitDepths.Rgba8:

                        using (Bitmap temp = Quantize(scratchSurface, 7, 256, bitDepth == SavableBitDepths.Rgba8, progressCallback))
                        {
                            temp.Save(ms, codecInfo, encoderParams);
                        }
                        break;
                }

                Base64FormattingOptions options = configToken.LineBreaks ? Base64FormattingOptions.InsertLineBreaks : Base64FormattingOptions.None;

                base64 = Convert.ToBase64String(ms.GetBuffer(), options);
            }

            byte[] encodedBytes = null;

            if (configToken.UriEncode)
            {
                string uriEncodedText;
                switch (configToken.DataType)
                {
                    case UriDataType.None:
                    case UriDataType.Html:
                        uriEncodedText = string.Format(CultureInfo.InvariantCulture, configToken.Base64Format, codecInfo.MimeType, base64);
                        break;
                    case UriDataType.Css:
                        uriEncodedText = configToken.CssData.ToString(codecInfo.MimeType, base64);
                        break;
                    default:
                        throw new InvalidEnumArgumentException("configToken.DataType", (int)configToken.DataType, typeof(UriDataType));
                }

                encodedBytes = Encoding.UTF8.GetBytes(uriEncodedText);
            }
            else
            {
                encodedBytes = Encoding.UTF8.GetBytes(base64);
            }

            output.Write(encodedBytes, 0, encodedBytes.Length);
        }
    }
}
