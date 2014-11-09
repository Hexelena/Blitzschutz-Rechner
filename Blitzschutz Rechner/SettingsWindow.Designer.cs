namespace Blitzschutz_Rechner
{
    partial class SettingsWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.BtnClose = new System.Windows.Forms.Button();
            this.checkBoxShowSingleRod = new System.Windows.Forms.CheckBox();
            this.toolTipShowSingleRod = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.toolTipShowSingleRod.SetToolTip(this.BtnClose, resources.GetString("BtnClose.ToolTip"));
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // checkBoxShowSingleRod
            // 
            resources.ApplyResources(this.checkBoxShowSingleRod, "checkBoxShowSingleRod");
            this.checkBoxShowSingleRod.Name = "checkBoxShowSingleRod";
            this.toolTipShowSingleRod.SetToolTip(this.checkBoxShowSingleRod, resources.GetString("checkBoxShowSingleRod.ToolTip"));
            this.checkBoxShowSingleRod.UseVisualStyleBackColor = true;
            this.checkBoxShowSingleRod.CheckedChanged += new System.EventHandler(this.checkBoxShowSingleRod_CheckedChanged);
            // 
            // comboBoxLanguage
            // 
            resources.ApplyResources(this.comboBoxLanguage, "comboBoxLanguage");
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.toolTipShowSingleRod.SetToolTip(this.comboBoxLanguage, resources.GetString("comboBoxLanguage.ToolTip"));
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // SettingsWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.checkBoxShowSingleRod);
            this.Controls.Add(this.BtnClose);
            this.Name = "SettingsWindow";
            this.toolTipShowSingleRod.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.CheckBox checkBoxShowSingleRod;
        private System.Windows.Forms.ToolTip toolTipShowSingleRod;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
    }
}