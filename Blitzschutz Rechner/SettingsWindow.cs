using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Resources;

namespace Blitzschutz_Rechner
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            checkBoxShowSingleRod.Checked = Properties.Settings.Default.ShowSingleRod;

            toolTipShowSingleRod.SetToolTip(checkBoxShowSingleRod, "Dies ist in der normalen Ansicht schon möglich. Dazu müssen nur die benötigten Felder ausgefüllt werden.");

            comboBoxLanguage.Items.Add("Deutsch");
            comboBoxLanguage.Items.Add("English");
            this.comboBoxLanguage.SelectedIndexChanged -= new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            switch (Properties.Settings.Default.Language)
            {
                case "de":
                    comboBoxLanguage.SelectedItem = "Deutsch";
                    break;
                case "en":
                    comboBoxLanguage.SelectedItem = "English";
                    break;
            }
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
        }

        private void checkBoxShowSingleRod_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowSingleRod = checkBoxShowSingleRod.Checked;
            Properties.Settings.Default.Save();
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxLanguage.SelectedItem.ToString())
            {
                case "Deutsch":
                    //ChangeLanguage("de");
                    Properties.Settings.Default.Language = "de";
                    break;
                case "English":
                    //ChangeLanguage("en");
                    Properties.Settings.Default.Language = "en";
                    break;
            }
            Properties.Settings.Default.Save();
            MessageBox.Show("Die Änderung der Sprache wird erst bei einem Neustart der Anwendung erfolgen", "Neustart nötig", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //if (MessageBox.Show("Um die Sprache zu ändern muss die Anwendung neu gestartet werden. möchten sie das jetzt tun?", "Neustarten?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    Application.Restart(); //TODO :Doesn't always work correctly. workaround to be found
            //}
        }

        //private void ChangeLanguage(string lang)
        //{
        //    foreach (Control c in this.Controls)
        //    {
        //        ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsWindow));
        //        resources.ApplyResources(c, c.Name, new CultureInfo(lang));
        //    }
        //}
    }
}
