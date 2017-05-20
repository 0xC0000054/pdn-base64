/////////////////////////////////////////////////////////////////////////////////
//
// Base64 FileType Plugin for Paint.NET
// 
// This software is provided under the MIT License:
//   Copyright (c) 2013-2017 Nicholas Hayes
//
// See LICENSE.txt for complete licensing and attribution information.
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Windows.Forms;
using PaintDotNet;
using System.Globalization;

namespace Base64FileTypePlugin
{
    internal partial class Base64SaveConfigWidget : SaveConfigWidget
    {
        internal const string Base64ContentType = "data:{0};base64,{1}";

        private UriDataType dataType;
        private string base64;
        private CssTokenData cssData;
        private int suppressTokenUpdateCounter;

        public Base64SaveConfigWidget()
        {
            InitializeComponent();
            this.cssData = new CssTokenData();
            this.suppressTokenUpdateCounter = 0;
        }

        private void PushSuppressTokenUpdate()
        {
            this.suppressTokenUpdateCounter++;
        }

        private void PopSuppressTokenUpdate()
        {
            this.suppressTokenUpdateCounter--;
            if (suppressTokenUpdateCounter == 0)
            {
                UpdateToken();
            }
        }

        private void UpdateConfigToken()
        {
            if (suppressTokenUpdateCounter == 0)
            {
                UpdateToken();
            }
        }

        protected override void InitFileType()
        {
            base.fileType = new Base64FileType();
        }

        protected override void InitTokenFromWidget()
        {
            Base64SaveConfigToken configToken = (Base64SaveConfigToken)token;

            configToken.ImageType = (FileFormat)this.formatCbo.SelectedIndex;
            configToken.Base64Format = base64;
            configToken.LineBreaks = this.lineBreakCb.Enabled ? this.lineBreakCb.Checked : false;
            configToken.UriEncode = this.uriEncodeCb.Checked;
            configToken.DataType = dataType;
            configToken.HtmlAltText = this.htmlAltText.Text;
            configToken.CssData = cssData;
        }

        protected override void InitWidgetFromToken(PaintDotNet.SaveConfigToken sourceToken)
        {
            Base64SaveConfigToken configToken = token as Base64SaveConfigToken;

            PushSuppressTokenUpdate();
            if (token != null)
            {
                this.formatCbo.SelectedIndex = (int)configToken.ImageType;
                this.lineBreakCb.Checked = configToken.LineBreaks;
                this.uriEncodeCb.Checked = configToken.UriEncode;
                this.uriDataTypeCbo.Enabled = configToken.UriEncode;
                this.uriDataTypeCbo.SelectedIndex = (int)configToken.DataType;
                this.htmlAltText.Text = configToken.HtmlAltText;

                if (configToken.CssData != null)
                {
                    this.cssData = configToken.CssData; 
                }

                this.cssClassNameTxt.Text = cssData.className;
                this.cssBackColorTxt.Text = cssData.color;
                this.cssRepeatCbo.SelectedIndex = (int)cssData.repeat;
                this.cssAttachTxt.Text = cssData.attachment;
                this.cssPositionTxt.Text = cssData.position;
            }
            else
            {
                this.formatCbo.SelectedIndex = 1;
                this.lineBreakCb.Checked = false;
                this.uriEncodeCb.Checked = false;
                this.uriDataTypeCbo.Enabled = false;
                this.uriDataTypeCbo.SelectedIndex = 0;
                this.htmlAltText.Text = string.Empty;

                this.cssClassNameTxt.Text = cssData.className;
                this.cssBackColorTxt.Text = cssData.color;
                this.cssRepeatCbo.SelectedIndex = (int)cssData.repeat;
                this.cssAttachTxt.Text = cssData.attachment;
                this.cssPositionTxt.Text = cssData.position;
            }
            PopSuppressTokenUpdate();
        }

        /// <summary>
        /// Determines whether the specified key is a hexadecimal character.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="shiftPressed">Set to <c>true</c> when the shift key is pressed.</param>
        /// <returns><c>true</c> if the specified key is a valid hexadecimal character; otherwise <c>false</c></returns>
        private static bool IsHexadecimalChar(Keys key, bool shiftPressed)
        {
            bool result = false;
            if (!shiftPressed && (key >= Keys.D0 && key <= Keys.D9 || key >= Keys.NumPad0 && key <= Keys.NumPad9))
            {
                result = true;
            }
            else
            {
                switch (key)
                {
                    case Keys.A:
                    case Keys.B:
                    case Keys.C:
                    case Keys.D:
                    case Keys.E:
                    case Keys.F:
                        result = true;
                        break;
                }
            }

            return result;
        }

        private void cssBackColorTxt_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;  
            if (IsHexadecimalChar(key, e.Shift) || key == Keys.Delete || key == Keys.Back || key == Keys.Left || key == Keys.Right)
            {
                e.Handled = true;
                e.SuppressKeyPress = false;
            }
            else
            {
                e.Handled = false;
                e.SuppressKeyPress = true;
            }
        }

        private void formatCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateConfigToken();
        }

        private void lineBreakCb_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfigToken();
        }

        private void uriEncodeCb_CheckedChanged(object sender, EventArgs e)
        {
            this.uriDataTypeCbo.Enabled = this.uriEncodeCb.Checked;

            if (!uriDataTypeCbo.Enabled)
            {
                this.uriDataTypeCbo.SelectedIndex = (int)UriDataType.None;
            }
            else
            {
                FormatBase64String();
            }

            UpdateConfigToken();
        }

        private void uriDataTypeCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataType = (UriDataType)this.uriDataTypeCbo.SelectedIndex;

            this.lineBreakCb.Enabled = this.dataType == UriDataType.None;
            switch (dataType)
            {
                case UriDataType.None:
                    this.htmlPanel.Visible = false;
                    this.cssPanel.Visible = false;
                    break;
                case UriDataType.Html:
                    this.cssPanel.Visible = false;
                    this.htmlPanel.Visible = true;
                    break;
                case UriDataType.Css:
                    this.htmlPanel.Visible = false;
                    this.cssPanel.Visible = true;
                    break;
                default:
                    throw new InvalidEnumArgumentException("dataType", (int)dataType, typeof(UriDataType));
            }

            FormatBase64String();
            UpdateConfigToken();
        }

        private void FormatBase64String()
        {
            if (uriEncodeCb.Checked)
            {
                switch (dataType)
                {
                    case UriDataType.None:
                        base64 = Base64ContentType;
                        break;
                    case UriDataType.Html:
                        base64 = string.Format(
                            CultureInfo.InvariantCulture,
                            "<img src={0} alt=\"{1}\" />",
                            Base64ContentType, 
                            this.htmlAltText.Text.Trim());
                        break;
                    case UriDataType.Css:
                        base64 = string.Empty;
                        break;
                }
            }
        }

        private void htmlAltText_TextChanged(object sender, EventArgs e)
        {
            FormatBase64String();
            UpdateConfigToken();
        }

        private void cssClassNameTxt_TextChanged(object sender, EventArgs e)
        {
            this.cssData.className = this.cssClassNameTxt.Text.Trim();
            UpdateConfigToken();
        }

        private void cssBackColorTxt_TextChanged(object sender, EventArgs e)
        {
            this.cssData.color = this.cssBackColorTxt.Text.Trim();
            UpdateConfigToken();
        }

        private void cssRepeatCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cssData.repeat = (CssRepeatType)this.cssRepeatCbo.SelectedIndex;
            UpdateConfigToken();
        }

        private void cssAttachTxt_TextChanged(object sender, EventArgs e)
        {
            this.cssData.attachment = this.cssAttachTxt.Text.Trim();
            UpdateConfigToken();
        }

        private void cssPositionTxt_TextChanged(object sender, EventArgs e)
        {
            this.cssData.position = this.cssPositionTxt.Text.Trim();
            UpdateConfigToken();
        }
    }
}
