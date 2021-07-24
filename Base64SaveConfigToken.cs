/////////////////////////////////////////////////////////////////////////////////
//
// Base64 FileType Plugin for Paint.NET
//
// This software is provided under the MIT License:
//   Copyright (c) 2013-2018, 2020, 2021 Nicholas Hayes
//
// See LICENSE.txt for complete licensing and attribution information.
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using PaintDotNet;

namespace Base64FileTypePlugin
{
    [Serializable]
    public sealed class Base64SaveConfigToken : SaveConfigToken
    {
        public FileFormat ImageType
        {
            get;
            internal set;
        }

        public string Base64Format
        {
            get;
            internal set;
        }

        public bool LineBreaks
        {
            get;
            internal set;
        }

        public bool UriEncode
        {
            get;
            internal set;
        }

        public UriDataType DataType
        {
            get;
            internal set;
        }

        public string HtmlAltText
        {
            get;
            internal set;
        }

        public CssTokenData CssData
        {
            get;
            internal set;
        }

        public Base64SaveConfigToken(FileFormat imageType, string base64, bool lineBreaks, bool uriEncode, UriDataType dataType, string altText, CssTokenData css)
        {
            this.ImageType = imageType;
            this.Base64Format = base64;
            this.LineBreaks = lineBreaks;
            this.UriEncode = uriEncode;
            this.DataType = dataType;
            this.HtmlAltText = altText;
            this.CssData = css;
        }

        private Base64SaveConfigToken(Base64SaveConfigToken cloneMe)
        {
            this.ImageType = cloneMe.ImageType;
            this.Base64Format = cloneMe.Base64Format;
            this.LineBreaks = cloneMe.LineBreaks;
            this.UriEncode = cloneMe.UriEncode;
            this.DataType = cloneMe.DataType;
            this.HtmlAltText = cloneMe.HtmlAltText;
            this.CssData = cloneMe.CssData;
        }

        public override object Clone()
        {
            return new Base64SaveConfigToken(this);
        }
    }
}
