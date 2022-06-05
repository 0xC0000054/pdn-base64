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

using System;
using System.Globalization;
using System.Text;

namespace Base64FileTypePlugin
{
    [Serializable]
    public sealed class CssTokenData
    {
        internal string className;
        internal string color;
        internal CssRepeatType repeat;
        internal string attachment;
        internal string position;

        public CssTokenData()
        {
            this.className = "body";
            this.color = "ffffff";
            this.repeat = CssRepeatType.None;
            this.attachment = string.Empty;
            this.position = string.Empty;
        }

        public string ToString(string mimeType, string base64)
        {
            string body = !string.IsNullOrEmpty(this.className) ? this.className : "body";

            StringBuilder sb = new StringBuilder(body);
            sb.AppendFormat(CultureInfo.InvariantCulture,
                            "{ background: #{0} ",
                            !string.IsNullOrEmpty(this.color) ? this.color : "ffffff");

            string base64Format = string.Format(CultureInfo.InvariantCulture, Base64SaveConfigWidget.Base64ContentType, mimeType, base64);
            sb.AppendFormat(CultureInfo.InvariantCulture, "url({0}) ", base64Format);

            switch (repeat)
            {
                case CssRepeatType.None:
                    sb.Append("no-repeat ");
                    break;
                case CssRepeatType.Horizontal:
                    sb.Append("repeat-x ");
                    break;
                case CssRepeatType.Vertical:
                    sb.Append("repeat-y ");
                    break;
                case CssRepeatType.Both:
                    sb.Append("repeat ");
                    break;
                default:
                    throw new System.ComponentModel.InvalidEnumArgumentException("repeat", (int)repeat, typeof(CssRepeatType));
            }

            if (!string.IsNullOrEmpty(this.attachment))
            {
                sb.Append(this.attachment + " ");
            }

            if (!string.IsNullOrEmpty(this.position))
            {
                sb.Append(this.position);
            }

            sb.Append(";}");

            return sb.ToString();
        }
    }
}