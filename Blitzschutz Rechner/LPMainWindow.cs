using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace Blitzschutz_Rechner
{
    public partial class BlitzschutzRechner : Form
    {
        public BlitzschutzRechner()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
            Blitzschutz = new LightningProtectionCalculation();
        }

        private LightningProtectionCalculation Blitzschutz;


        double[] values_upper_value_border = { 30, 20, 30, 30, 18, 75 };
        //The last two 30's are actually 24s but if you just want to calculate the protection radius 30 is fine. 
        //So the handling for the protection area width is at the point, when the average pole height is set.
        double[] values_lower_value_border = { 0, 1, 0, 0, 1, 5 };


        //At the moment only for the one pole part. will later be expanded to multidimensional array
        double[] values_upper_chart_border = { 18.0721, 20.5314, 22.2571, 23.8227, 25.6187, 26.9989, 28.5016, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30 };
        double[] values_lower_chart_border = { 1.1720, 2.2321, 3.2466, 4.1892, 5.0890, 6.1104, 7.1310, 8.1611, 8.9416, 10.0295, 10.9904, 11.8418, 13.0007, 13.9913, 14.9657, 16.0861, 16.9282, 18.0794, 19.0405, 19.9652 };


        double One_Pole_Pole_Height = 0,
               One_Pole_Protection_Height = 0,
               Two_Poles_1st_Pole_Height = 0,
               Two_Poles_2nd_Pole_Height = 0,
               Two_Poles_Protection_Height = 0,
               Two_Poles_Pole_Distance = 0;

        
        #region Input_Handling

        private void Handle_Input_One_Pole_groupBox()
        {
            #region Handling_via_Check_Textbox_Input

            Txtbox_One_Pole_Protection_Radius.Clear();

            if (Check_Textbox_Input(1) & Check_Textbox_Input(2))
            {
                if (One_Pole_Protection_Height == (int)One_Pole_Protection_Height)
                {
                    if ((One_Pole_Pole_Height <= values_upper_chart_border[(int)One_Pole_Protection_Height - 1]) && (One_Pole_Pole_Height >= values_lower_chart_border[(int)One_Pole_Protection_Height - 1]))
                    {
                        Txtbox_One_Pole_Protection_Radius.Text = Blitzschutz.Calculate_Protection_Radius(One_Pole_Pole_Height, One_Pole_Protection_Height).ToString("F");
                    }
                    else
                    {
                        Display_Error_Messages(7, "Wert nicht erlaubt", true);
                    }
                }
                else
                {
                    if ((One_Pole_Pole_Height <= values_upper_chart_border[(int)One_Pole_Protection_Height]) && (One_Pole_Pole_Height >= values_lower_chart_border[(int)One_Pole_Protection_Height]))
                    {
                        Txtbox_One_Pole_Protection_Radius.Text = Blitzschutz.Calculate_Protection_Radius(One_Pole_Pole_Height, One_Pole_Protection_Height).ToString("F");
                    }
                }
            }
            #endregion 
        }

        private void Handle_Irregular_Input_Two_Poles_groupBox()
        {
        /*
         * first it is checked if the first pole protection radius can be calculated if possible it is calculated
         * same goes for the second pole.
         * if then the pole distance is fine too, the protection area width is calculated (if all values are in range)
         */
            Txtbox1stRodProtectionRadius.Clear();
            Txtbox2ndRodProtectionRadius.Clear();
            TxtboxProtectedZone.Clear();

            if (Groupbox1stRodProtectionRadiusError.Visible)
            {
                Display_Error_Messages(7, null, false);
            }

            if ((Check_Textbox_Input(3)) & (Check_Textbox_Input(5)))
            {
                if (Two_Poles_Protection_Height == (int)Two_Poles_Protection_Height)
                {
                    if ((Two_Poles_1st_Pole_Height <= values_upper_chart_border[(int)Two_Poles_Protection_Height - 1]) && (Two_Poles_1st_Pole_Height >= values_lower_chart_border[(int)Two_Poles_Protection_Height - 1]))
                    {
                        Txtbox1stRodProtectionRadius.Text = Blitzschutz.Calculate_Protection_Radius(Two_Poles_1st_Pole_Height, Two_Poles_Protection_Height).ToString("F");
                    }
                    else
                    {
                        Display_Error_Messages(7, "Wert nicht erlaubt", true);
                    }
                }
                else
                {
                    if ((Two_Poles_1st_Pole_Height <= values_upper_chart_border[(int)Two_Poles_Protection_Height]) && (Two_Poles_1st_Pole_Height >= values_lower_chart_border[(int)Two_Poles_Protection_Height]))
                    {
                        if (Groupbox1stRodProtectionRadiusError.Visible)
                        {
                            Display_Error_Messages(7, null, false);
                        }
                        Txtbox1stRodProtectionRadius.Text = Blitzschutz.Calculate_Protection_Radius(Two_Poles_1st_Pole_Height, Two_Poles_Protection_Height).ToString("F");
                    }
                    else
                    {
                        Display_Error_Messages(7, "Wert nicht erlaubt", true);
                    }
                }
            }

            if (Groupbox2ndRodProtectionRadiusError.Visible)
            {
                Display_Error_Messages(8, null, false);
            }

            if ((Check_Textbox_Input(4)) & (Check_Textbox_Input(5)))
            {
                if (Two_Poles_Protection_Height == (int)Two_Poles_Protection_Height)
                {
                    if ((Two_Poles_2nd_Pole_Height <= values_upper_chart_border[(int)Two_Poles_Protection_Height - 1]) && (Two_Poles_2nd_Pole_Height >= values_lower_chart_border[(int)Two_Poles_Protection_Height - 1]))
                    {
                        Txtbox2ndRodProtectionRadius.Text = Blitzschutz.Calculate_Protection_Radius(Two_Poles_2nd_Pole_Height, Two_Poles_Protection_Height).ToString("F");
                    }
                    else
                    {
                        Display_Error_Messages(8, "Wert nicht erlaubt", true);
                    }
                }
                else
                {
                    if ((Two_Poles_1st_Pole_Height <= values_upper_chart_border[(int)Two_Poles_Protection_Height]) && (Two_Poles_1st_Pole_Height >= values_lower_chart_border[(int)Two_Poles_Protection_Height]))
                    {
                        Txtbox2ndRodProtectionRadius.Text = Blitzschutz.Calculate_Protection_Radius(Two_Poles_2nd_Pole_Height, Two_Poles_Protection_Height).ToString("F");
                    }
                    else
                    {
                        Display_Error_Messages(8, "Wert nicht erlaubt", true);
                    }
                }
            }
            if ((Check_Textbox_Input(3)) & (Check_Textbox_Input(4)) & (Check_Textbox_Input(5)) & (Check_Textbox_Input(6)))
            {
                double average_pole_height = 0;

                average_pole_height = (Two_Poles_1st_Pole_Height + Two_Poles_2nd_Pole_Height) / 2;

                if (average_pole_height > 24)
                {
                    Display_Error_Messages(3, "Wert nicht erlaubt", true);
                    Display_Error_Messages(4, "Wert nicht erlaubt", true);
                    return;
                }
                TxtboxProtectedZone.Text = Blitzschutz.Calculate_Protection_Floor_Width(average_pole_height, Two_Poles_Protection_Height, Two_Poles_Pole_Distance).ToString("F");
            }
        }

        private bool Check_Textbox_Input(int Textbox_Number)
        {
            switch (Textbox_Number)
            {
                case 1:
                    if ((Txtbox_One_Pole_Pole_Height.Text != "")
                        && (double.TryParse(Txtbox_One_Pole_Pole_Height.Text.ToString(), NumberStyles.Float, null, out One_Pole_Pole_Height))
                        && ((One_Pole_Pole_Height <= values_upper_value_border[Textbox_Number - 1]))
                        && (One_Pole_Pole_Height >= values_lower_value_border[Textbox_Number - 1]))
                    {
                        if (groupBox_One_Pole_Pole_Height_Error.Visible)
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                        return true;
                    }
                    else
                    {                                                  //The null causes that only the geographical standard that is set in Windows is accepted                        {
                        if ((Txtbox_One_Pole_Pole_Height.Text != "") && (!double.TryParse(Txtbox_One_Pole_Pole_Height.Text.ToString(), NumberStyles.Float, null, out One_Pole_Pole_Height)))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültige Eingabe", true); // 1 => Errorbox beneath Txtbox_Pole_Height
                        }
                        else if (!((One_Pole_Pole_Height <= values_upper_value_border[Textbox_Number - 1]) && (One_Pole_Pole_Height >= values_lower_value_border[Textbox_Number - 1])))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültiger Wert", true);
                        }
                        else if (Txtbox_One_Pole_Pole_Height.Text == "")
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                    }
                    break;
                case 2:
                    if ((Txtbox_One_Pole_Protection_Height.Text != "") && (double.TryParse(Txtbox_One_Pole_Protection_Height.Text.ToString(), NumberStyles.Float, null, out One_Pole_Protection_Height)) && ((One_Pole_Protection_Height <= values_upper_value_border[Textbox_Number - 1])) && (One_Pole_Protection_Height >= values_lower_value_border[Textbox_Number - 1]))
                    {
                        if (groupBox_One_Pole_Protection_Height_Error.Visible)
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                        return true;
                    }
                    else
                    {
                        if ((Txtbox_One_Pole_Protection_Height.Text != "") && (!double.TryParse(Txtbox_One_Pole_Protection_Height.Text.ToString(), NumberStyles.Float, null, out One_Pole_Protection_Height)))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültige Eingabe", true); // 1 => Errorbox beneath Txtbox_Pole_Height
                        }
                        else if ((One_Pole_Protection_Height != 0) && !((One_Pole_Protection_Height <= values_upper_value_border[Textbox_Number - 1]) && (One_Pole_Protection_Height >= values_lower_value_border[Textbox_Number - 1])))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültiger Wert", true);
                        }
                        else if (Txtbox_One_Pole_Protection_Height.Text == "")
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                    }
                    break;
                case 3:
                    if ((Txtbox1stRodHeight.Text != "") && (double.TryParse(Txtbox1stRodHeight.Text.ToString(), NumberStyles.Float, null, out Two_Poles_1st_Pole_Height)) && ((Two_Poles_1st_Pole_Height <= values_upper_value_border[Textbox_Number - 1])) && (Two_Poles_1st_Pole_Height >= values_lower_value_border[Textbox_Number - 1]))
                    {
                        if (Groupbox1stRodHeightError.Visible)
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                        return true;
                    }
                    else
                    {
                        if ((Txtbox1stRodHeight.Text != "") && (!double.TryParse(Txtbox1stRodHeight.Text.ToString(), NumberStyles.Float, null, out Two_Poles_1st_Pole_Height)))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültige Eingabe", true); // 1 => Errorbox beneath Txtbox_Pole_Height
                        }
                        else if (!((Two_Poles_1st_Pole_Height <= values_upper_value_border[Textbox_Number - 1]) && (Two_Poles_1st_Pole_Height >= values_lower_value_border[Textbox_Number - 1])))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültiger Wert", true);
                        }
                        else if (Txtbox1stRodHeight.Text == "")
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                    }
                    break;
                case 4:
                    if ((Txtbox2ndRodHeight.Text != "") && (double.TryParse(Txtbox2ndRodHeight.Text.ToString(), NumberStyles.Float, null, out Two_Poles_2nd_Pole_Height)) && ((Two_Poles_2nd_Pole_Height <= values_upper_value_border[Textbox_Number - 1])) && (Two_Poles_2nd_Pole_Height >= values_lower_value_border[Textbox_Number - 1]))
                    {
                        if (Groupbox2ndRodHeightError.Visible)
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                        return true;
                    }
                    else
                    {
                        if ((Txtbox2ndRodHeight.Text != "") && (!double.TryParse(Txtbox2ndRodHeight.Text.ToString(), NumberStyles.Float, null, out Two_Poles_2nd_Pole_Height)))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültige Eingabe", true); // 1 => Errorbox beneath Txtbox_Pole_Height
                        }
                        else if (!((Two_Poles_2nd_Pole_Height <= values_upper_value_border[Textbox_Number - 1]) && (Two_Poles_2nd_Pole_Height >= values_lower_value_border[Textbox_Number - 1])))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültiger Wert", true);
                        }
                        else if (Txtbox2ndRodHeight.Text == "")
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                    }
                    break;
                case 5:
                    if ((TxtboxProtectionHeight.Text != "") && (double.TryParse(TxtboxProtectionHeight.Text.ToString(), NumberStyles.Float, null, out Two_Poles_Protection_Height)) && ((Two_Poles_Protection_Height <= values_upper_value_border[Textbox_Number - 1])) && (Two_Poles_Protection_Height >= values_lower_value_border[Textbox_Number - 1]))
                    {
                        if (GroupboxProtectionHeightError.Visible)
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                        return true;
                    }
                    else
                    {
                        if ((TxtboxProtectionHeight.Text != "") && (!double.TryParse(TxtboxProtectionHeight.Text.ToString(), NumberStyles.Float, null, out Two_Poles_Protection_Height)))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültige Eingabe", true); // 1 => Errorbox beneath Txtbox_Pole_Height
                        }
                        else if ((Two_Poles_Protection_Height != 0) && !((Two_Poles_Protection_Height <= values_upper_value_border[Textbox_Number - 1]) && (Two_Poles_Protection_Height >= values_lower_value_border[Textbox_Number - 1])))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültiger Wert", true);
                        }
                        else if (TxtboxProtectionHeight.Text == "")
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                    }
                    break;
                case 6:
                    if ((TxtboxRodDistance.Text != "") && (double.TryParse(TxtboxRodDistance.Text.ToString(), NumberStyles.Float, null, out Two_Poles_Pole_Distance)) && ((Two_Poles_Pole_Distance <= values_upper_value_border[Textbox_Number - 1])) && (Two_Poles_Pole_Distance >= values_lower_value_border[Textbox_Number - 1]))
                    {
                        if (GroupboxRodDistanceError.Visible)
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                        return true;
                    }
                    else
                    {
                        if ((TxtboxRodDistance.Text != "") && (!double.TryParse(TxtboxRodDistance.Text.ToString(), NumberStyles.Float, null, out Two_Poles_Pole_Distance)))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültige Eingabe", true); // 1 => Errorbox beneath Txtbox_Pole_Height
                        }
                        else if ((Two_Poles_Pole_Distance != 0) && !((Two_Poles_Pole_Distance <= values_upper_value_border[Textbox_Number - 1]) && (Two_Poles_Pole_Distance >= values_lower_value_border[Textbox_Number - 1])))
                        {
                            Display_Error_Messages(Textbox_Number, "Ungültiger Wert", true);
                        }
                        else if (TxtboxRodDistance.Text == "")
                        {
                            Display_Error_Messages(Textbox_Number, null, false);
                        }
                    }
                    break;
            }

            return false;
        }

        
        #region Triggering
        private void Txtbox_One_Pole_Pole_Height_TextChanged(object sender, EventArgs e)
        {
            Handle_Input_One_Pole_groupBox();
        }
        private void Txtbox_One_Pole_Protection_Height_TextChanged(object sender, EventArgs e)
        {
            Handle_Input_One_Pole_groupBox();
        }

        private void Txtbox_Two_Poles_1st_Pole_Pole_Height_TextChanged(object sender, EventArgs e)
        {
            Handle_Irregular_Input_Two_Poles_groupBox();
        }
        private void Txtbox_Two_Poles_2nd_Pole_Pole_Height_TextChanged(object sender, EventArgs e)
        {
            Handle_Irregular_Input_Two_Poles_groupBox();
        }
        private void Txtbox_Two_Poles_Protection_Height_TextChanged(object sender, EventArgs e)
        {
            Handle_Irregular_Input_Two_Poles_groupBox();
        }
        private void Txtbox_Two_Poles_Pole_Distance_TextChanged(object sender, EventArgs e)
        {
            Handle_Irregular_Input_Two_Poles_groupBox();
        }
        #endregion

        #endregion

        #region Output

        //The real Output is in Handle_Irregular_Input_Two_Poles_groupBox(); 
        //If the Check for input Errors was unseccussful the Output is written and calculated directly.

        private void Display_Error_Messages(int Error_Label, string Error_Text, bool display_state)
        {
            #region Label_Enumeration
            //Error_Label says which Label shall be changed = 
            // One pole :  Used for value debugging only          
            //1 = Pole height
            //2 = Protection height
            // Two poles : 
            //3 = 1st Pole height
            //4 = 2nd Pole height
            //5 = Protection height
            //6 = Pole distance
            //7 = 1st Pole Protection Radius
            //8 = 2nd Pole Protection Radius
            #endregion

            #region Display_on
            if (display_state)
            {
                switch (Error_Label)
                {
                    case 1: //Only used for value debugging
                        Label_One_Pole_Pole_Height_Error.Text = Error_Text;
                        groupBox_One_Pole_Pole_Height_Error.Visible = true;
                        break;

                    case 2: //Only used for value debugging
                        Label_One_Pole_Protection_Height_Error.Text = Error_Text;
                        groupBox_One_Pole_Protection_Height_Error.Visible = true;
                        break;
                    case 3:
                        Label1stRodHeightError.Text = Error_Text;
                        Groupbox1stRodHeightError.Visible = true;
                        break;
                    case 4:
                        Label2ndRodHeightError.Text = Error_Text;
                        Groupbox2ndRodHeightError.Visible = true;
                        break;
                    case 5:
                        LabelProtectionHeightError.Text = Error_Text;
                        GroupboxProtectionHeightError.Visible = true;
                        break;
                    case 6:
                        LabelRodDistanceError.Text = Error_Text;
                        GroupboxRodDistanceError.Visible = true;
                        break;
                    case 7 :
                        Label1stRodProtectionRadiusError.Text = Error_Text;
                        Groupbox1stRodProtectionRadiusError.Visible = true;
                        break;
                    case 8:
                        Label2ndRodProtectionRadiusError.Text = Error_Text;
                        Groupbox2ndRodProtectionRadiusError.Visible = true;
                        break;
                }
            }
            #endregion

            #region Display_off
            else
            {
                switch (Error_Label)
                {
                    case 1: //Used for value debugging
                        groupBox_One_Pole_Pole_Height_Error.Visible = false;
                        break;
                    case 2: //Used for value debugging
                        groupBox_One_Pole_Protection_Height_Error.Visible = false;
                        break;
                    case 3:
                        Groupbox1stRodHeightError.Visible = false;
                        break;
                    case 4:
                        Groupbox2ndRodHeightError.Visible = false;
                        break;
                    case 5:
                        GroupboxProtectionHeightError.Visible = false;
                        break;
                    case 6:
                        GroupboxRodDistanceError.Visible = false;
                        break;
                    case 7:
                        Groupbox1stRodProtectionRadiusError.Visible = false;
                        break;
                    case 8:
                        Groupbox2ndRodProtectionRadiusError.Visible = false;
                        break;
                }

            }
            #endregion
        }

        #endregion

         
        # region MENU

        private void programmBeendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void wertNichtErlaubtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wert nicht erlaubt : \n" +
                            "\n" +
                            "Der Schutzradius kann für die eingegebene Schutzhöhe und Masthöhe \n" +
                            "nicht berechnet werden, weil der Wert außerhalb der Blitzschutzkurven liegt",
                            "Information zu Fehler : Wert nicht erlaubt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ungültigerWertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ungültiger Wert : \n" +
                            "\n" +
                            "Der eingegebene Wert liegt außerhalb der erlaubten Bereiches.\n"+
                            "Erlaubte Bereiche sind:\n"+
                            "Masthöhe :\n"+
                            "- bei Berechnung des Schutzradius :\t1-30\n" +
                            "- bei Berechnung der Schutzraumbreite :\t1-24\n" +
                            "Schutzhöhe : \n"+ 
                            "- bei Berechnung des Schutzradius :\t1-20\n"+
                            "- bei Berechnung der Schutzraumbreite :\t1-18\n"+
                            "Abstand der Masten : \t\t5-75\n"+
                            "\n" +
                            "Zusätzlich kann es noch vorkommen, dass manche \n"+
                            "Kombinationen von Werten nicht möglich sind.\n"+
                            "Dann kommt als Fehler : 'Wert nicht erlaubt'\n",
                            "Information zu Fehler : Wert nicht erlaubt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ungültigerEingabeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ungültige Eingabe : \n" +
                            "\n" +
                            "Sie haben ein ungültiges Zeichen eingegeben\n" +
                            "\n" +
                            "Oder sie haben eine Zahl in einem ungültigen Format eingegeben, \n"+
                            "zum Beispiel mit Tausendertrennzeichen oder mit \n"+
                            "einem '.' statt einem ',' um Dezimalstellen einzugeben\n",
                            "Information zu Fehler : Ungültige Eingabe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Blitzschutz Rechner \n\n" +
                            "Nach den Graphen zum Blitzschutz aus dem ABB-Schaltanlagenhandbuch\n\n\n" +
                            "Copyright ©  2013 Samuel Schüttler", 
                            "Über den Blitzschutz-Rechner", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsWindow SetWindow = new SettingsWindow();
            SetWindow.ShowDialog(this);

            ApplySettings();
        }

        #endregion

        private void ApplySettings()
        {
            if ((Properties.Settings.Default.ShowSingleRod) && (this.Size.Height != 467))
            {
                groupBox_One_Pole.Visible = true;
                this.Size = new Size(this.Size.Width, 476);
            }
            else if ((!Properties.Settings.Default.ShowSingleRod) && (this.Size.Height != 302))
            {
                groupBox_One_Pole.Visible = false;
                this.Size = new Size(this.Size.Width, 302);
            }

            //if ((Properties.Settings.Default.Language == "en") && (this.Text == "Blitzschutz-Rechner"))
                //Application.Restart();
            //else if ((Properties
            
        }

        private void BlitzschutzRechner_Load(object sender, EventArgs e)
        {
            ApplySettings();
        }
    }
}