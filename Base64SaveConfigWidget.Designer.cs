namespace Base64FileTypePlugin
{
    partial class Base64SaveConfigWidget
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.formatCbo = new System.Windows.Forms.ComboBox();
            this.lineBreakCb = new System.Windows.Forms.CheckBox();
            this.uriEncodeCb = new System.Windows.Forms.CheckBox();
            this.uriDataTypeCbo = new System.Windows.Forms.ComboBox();
            this.cssPanel = new System.Windows.Forms.Panel();
            this.cssClassNameTxt = new System.Windows.Forms.TextBox();
            this.cssClassNameLbl = new PaintDotNet.HeaderLabel();
            this.cssPositionTxt = new System.Windows.Forms.TextBox();
            this.cssPositionLbl = new PaintDotNet.HeaderLabel();
            this.cssAttachTxt = new System.Windows.Forms.TextBox();
            this.cssAttachbl = new PaintDotNet.HeaderLabel();
            this.cssRepeatCbo = new System.Windows.Forms.ComboBox();
            this.cssRepeatLbl = new PaintDotNet.HeaderLabel();
            this.cssBackColorLbl = new PaintDotNet.HeaderLabel();
            this.cssBackColorTxt = new System.Windows.Forms.TextBox();
            this.htmlPanel = new System.Windows.Forms.Panel();
            this.htmlAltText = new System.Windows.Forms.TextBox();
            this.htmlAltTextLbl = new PaintDotNet.HeaderLabel();
            this.encodeTypeLbl = new PaintDotNet.HeaderLabel();
            this.fileFormatLbl = new PaintDotNet.HeaderLabel();
            this.cssPanel.SuspendLayout();
            this.htmlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // formatCbo
            // 
            this.formatCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatCbo.FormattingEnabled = true;
            this.formatCbo.Items.AddRange(new object[] {
            "Bmp",
            "Png"});
            this.formatCbo.Location = new System.Drawing.Point(4, 25);
            this.formatCbo.Name = "formatCbo";
            this.formatCbo.Size = new System.Drawing.Size(134, 21);
            this.formatCbo.TabIndex = 1;
            this.formatCbo.SelectedIndexChanged += new System.EventHandler(this.formatCbo_SelectedIndexChanged);
            // 
            // lineBreakCb
            // 
            this.lineBreakCb.AutoSize = true;
            this.lineBreakCb.Location = new System.Drawing.Point(4, 52);
            this.lineBreakCb.Name = "lineBreakCb";
            this.lineBreakCb.Size = new System.Drawing.Size(101, 17);
            this.lineBreakCb.TabIndex = 2;
            this.lineBreakCb.Text = "Insert line break";
            this.lineBreakCb.UseVisualStyleBackColor = true;
            this.lineBreakCb.CheckedChanged += new System.EventHandler(this.lineBreakCb_CheckedChanged);
            // 
            // uriEncodeCb
            // 
            this.uriEncodeCb.AutoSize = true;
            this.uriEncodeCb.Location = new System.Drawing.Point(4, 76);
            this.uriEncodeCb.Name = "uriEncodeCb";
            this.uriEncodeCb.Size = new System.Drawing.Size(118, 17);
            this.uriEncodeCb.TabIndex = 3;
            this.uriEncodeCb.Text = "Data URI encoding";
            this.uriEncodeCb.UseVisualStyleBackColor = true;
            this.uriEncodeCb.CheckedChanged += new System.EventHandler(this.uriEncodeCb_CheckedChanged);
            // 
            // uriDataTypeCbo
            // 
            this.uriDataTypeCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uriDataTypeCbo.FormattingEnabled = true;
            this.uriDataTypeCbo.Items.AddRange(new object[] {
            "None",
            "HTML",
            "CSS"});
            this.uriDataTypeCbo.Location = new System.Drawing.Point(4, 120);
            this.uriDataTypeCbo.Name = "uriDataTypeCbo";
            this.uriDataTypeCbo.Size = new System.Drawing.Size(134, 21);
            this.uriDataTypeCbo.TabIndex = 5;
            this.uriDataTypeCbo.SelectedIndexChanged += new System.EventHandler(this.uriDataTypeCbo_SelectedIndexChanged);
            // 
            // cssPanel
            // 
            this.cssPanel.Controls.Add(this.cssClassNameTxt);
            this.cssPanel.Controls.Add(this.cssClassNameLbl);
            this.cssPanel.Controls.Add(this.cssPositionTxt);
            this.cssPanel.Controls.Add(this.cssPositionLbl);
            this.cssPanel.Controls.Add(this.cssAttachTxt);
            this.cssPanel.Controls.Add(this.cssAttachbl);
            this.cssPanel.Controls.Add(this.cssRepeatCbo);
            this.cssPanel.Controls.Add(this.cssRepeatLbl);
            this.cssPanel.Controls.Add(this.cssBackColorLbl);
            this.cssPanel.Controls.Add(this.cssBackColorTxt);
            this.cssPanel.Location = new System.Drawing.Point(0, 148);
            this.cssPanel.Name = "cssPanel";
            this.cssPanel.Size = new System.Drawing.Size(148, 247);
            this.cssPanel.TabIndex = 6;
            this.cssPanel.Visible = false;
            // 
            // cssClassNameTxt
            // 
            this.cssClassNameTxt.Location = new System.Drawing.Point(4, 23);
            this.cssClassNameTxt.Name = "cssClassNameTxt";
            this.cssClassNameTxt.Size = new System.Drawing.Size(134, 20);
            this.cssClassNameTxt.TabIndex = 9;
            this.cssClassNameTxt.TextChanged += new System.EventHandler(this.cssClassNameTxt_TextChanged);
            // 
            // cssClassNameLbl
            // 
            this.cssClassNameLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cssClassNameLbl.Location = new System.Drawing.Point(4, 2);
            this.cssClassNameLbl.Name = "cssClassNameLbl";
            this.cssClassNameLbl.Size = new System.Drawing.Size(144, 14);
            this.cssClassNameLbl.TabIndex = 8;
            this.cssClassNameLbl.TabStop = false;
            this.cssClassNameLbl.Text = "Class name";
            // 
            // cssPositionTxt
            // 
            this.cssPositionTxt.Location = new System.Drawing.Point(4, 209);
            this.cssPositionTxt.Name = "cssPositionTxt";
            this.cssPositionTxt.Size = new System.Drawing.Size(131, 20);
            this.cssPositionTxt.TabIndex = 7;
            this.cssPositionTxt.TextChanged += new System.EventHandler(this.cssPositionTxt_TextChanged);
            // 
            // cssPositionLbl
            // 
            this.cssPositionLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cssPositionLbl.Location = new System.Drawing.Point(1, 189);
            this.cssPositionLbl.Name = "cssPositionLbl";
            this.cssPositionLbl.Size = new System.Drawing.Size(134, 14);
            this.cssPositionLbl.TabIndex = 6;
            this.cssPositionLbl.TabStop = false;
            this.cssPositionLbl.Text = "Position";
            // 
            // cssAttachTxt
            // 
            this.cssAttachTxt.Location = new System.Drawing.Point(4, 162);
            this.cssAttachTxt.Name = "cssAttachTxt";
            this.cssAttachTxt.Size = new System.Drawing.Size(137, 20);
            this.cssAttachTxt.TabIndex = 5;
            this.cssAttachTxt.TextChanged += new System.EventHandler(this.cssAttachTxt_TextChanged);
            // 
            // cssAttachbl
            // 
            this.cssAttachbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cssAttachbl.Location = new System.Drawing.Point(0, 142);
            this.cssAttachbl.Name = "cssAttachbl";
            this.cssAttachbl.Size = new System.Drawing.Size(144, 14);
            this.cssAttachbl.TabIndex = 4;
            this.cssAttachbl.TabStop = false;
            this.cssAttachbl.Text = "Attachment";
            // 
            // cssRepeatCbo
            // 
            this.cssRepeatCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cssRepeatCbo.FormattingEnabled = true;
            this.cssRepeatCbo.Items.AddRange(new object[] {
            "None",
            "Horizontal",
            "Vertical",
            "Both"});
            this.cssRepeatCbo.Location = new System.Drawing.Point(4, 115);
            this.cssRepeatCbo.Name = "cssRepeatCbo";
            this.cssRepeatCbo.Size = new System.Drawing.Size(134, 21);
            this.cssRepeatCbo.TabIndex = 3;
            this.cssRepeatCbo.SelectedIndexChanged += new System.EventHandler(this.cssRepeatCbo_SelectedIndexChanged);
            // 
            // cssRepeatLbl
            // 
            this.cssRepeatLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cssRepeatLbl.Location = new System.Drawing.Point(0, 95);
            this.cssRepeatLbl.Name = "cssRepeatLbl";
            this.cssRepeatLbl.Size = new System.Drawing.Size(144, 14);
            this.cssRepeatLbl.TabIndex = 2;
            this.cssRepeatLbl.TabStop = false;
            this.cssRepeatLbl.Text = "Repeat";
            // 
            // cssBackColorLbl
            // 
            this.cssBackColorLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cssBackColorLbl.Location = new System.Drawing.Point(4, 49);
            this.cssBackColorLbl.Name = "cssBackColorLbl";
            this.cssBackColorLbl.Size = new System.Drawing.Size(146, 14);
            this.cssBackColorLbl.TabIndex = 1;
            this.cssBackColorLbl.TabStop = false;
            this.cssBackColorLbl.Text = "Background color";
            // 
            // cssBackColorTxt
            // 
            this.cssBackColorTxt.Location = new System.Drawing.Point(4, 69);
            this.cssBackColorTxt.MaxLength = 6;
            this.cssBackColorTxt.Name = "cssBackColorTxt";
            this.cssBackColorTxt.Size = new System.Drawing.Size(134, 20);
            this.cssBackColorTxt.TabIndex = 0;
            this.cssBackColorTxt.TextChanged += new System.EventHandler(this.cssBackColorTxt_TextChanged);
            this.cssBackColorTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cssBackColorTxt_KeyDown);
            // 
            // htmlPanel
            // 
            this.htmlPanel.Controls.Add(this.htmlAltText);
            this.htmlPanel.Controls.Add(this.htmlAltTextLbl);
            this.htmlPanel.Location = new System.Drawing.Point(0, 147);
            this.htmlPanel.Name = "htmlPanel";
            this.htmlPanel.Size = new System.Drawing.Size(148, 217);
            this.htmlPanel.TabIndex = 8;
            // 
            // htmlAltText
            // 
            this.htmlAltText.Location = new System.Drawing.Point(4, 24);
            this.htmlAltText.Name = "htmlAltText";
            this.htmlAltText.Size = new System.Drawing.Size(134, 20);
            this.htmlAltText.TabIndex = 1;
            this.htmlAltText.TextChanged += new System.EventHandler(this.htmlAltText_TextChanged);
            // 
            // htmlAltTextLbl
            // 
            this.htmlAltTextLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.htmlAltTextLbl.Location = new System.Drawing.Point(3, 4);
            this.htmlAltTextLbl.Name = "htmlAltTextLbl";
            this.htmlAltTextLbl.Size = new System.Drawing.Size(144, 14);
            this.htmlAltTextLbl.TabIndex = 0;
            this.htmlAltTextLbl.TabStop = false;
            this.htmlAltTextLbl.Text = "Alt Text";
            // 
            // encodeTypeLbl
            // 
            this.encodeTypeLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.encodeTypeLbl.Location = new System.Drawing.Point(3, 99);
            this.encodeTypeLbl.Name = "encodeTypeLbl";
            this.encodeTypeLbl.Size = new System.Drawing.Size(144, 14);
            this.encodeTypeLbl.TabIndex = 4;
            this.encodeTypeLbl.TabStop = false;
            this.encodeTypeLbl.Text = "URI data type";
            // 
            // fileFormatLbl
            // 
            this.fileFormatLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.fileFormatLbl.Location = new System.Drawing.Point(4, 4);
            this.fileFormatLbl.Name = "fileFormatLbl";
            this.fileFormatLbl.Size = new System.Drawing.Size(144, 14);
            this.fileFormatLbl.TabIndex = 0;
            this.fileFormatLbl.TabStop = false;
            this.fileFormatLbl.Text = "File format";
            // 
            // Base64SaveConfigWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cssPanel);
            this.Controls.Add(this.uriDataTypeCbo);
            this.Controls.Add(this.encodeTypeLbl);
            this.Controls.Add(this.uriEncodeCb);
            this.Controls.Add(this.lineBreakCb);
            this.Controls.Add(this.formatCbo);
            this.Controls.Add(this.fileFormatLbl);
            this.Controls.Add(this.htmlPanel);
            this.Name = "Base64SaveConfigWidget";
            this.Size = new System.Drawing.Size(150, 399);
            this.cssPanel.ResumeLayout(false);
            this.cssPanel.PerformLayout();
            this.htmlPanel.ResumeLayout(false);
            this.htmlPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PaintDotNet.HeaderLabel fileFormatLbl;
        private System.Windows.Forms.ComboBox formatCbo;
        private System.Windows.Forms.CheckBox lineBreakCb;
        private System.Windows.Forms.CheckBox uriEncodeCb;
        private PaintDotNet.HeaderLabel encodeTypeLbl;
        private System.Windows.Forms.ComboBox uriDataTypeCbo;
        private System.Windows.Forms.Panel cssPanel;
        private PaintDotNet.HeaderLabel cssRepeatLbl;
        private PaintDotNet.HeaderLabel cssBackColorLbl;
        private System.Windows.Forms.TextBox cssBackColorTxt;
        private System.Windows.Forms.TextBox cssPositionTxt;
        private PaintDotNet.HeaderLabel cssPositionLbl;
        private System.Windows.Forms.TextBox cssAttachTxt;
        private PaintDotNet.HeaderLabel cssAttachbl;
        private System.Windows.Forms.ComboBox cssRepeatCbo;
        private System.Windows.Forms.Panel htmlPanel;
        private System.Windows.Forms.TextBox htmlAltText;
        private PaintDotNet.HeaderLabel htmlAltTextLbl;
        private PaintDotNet.HeaderLabel cssClassNameLbl;
        private System.Windows.Forms.TextBox cssClassNameTxt;
    }
}