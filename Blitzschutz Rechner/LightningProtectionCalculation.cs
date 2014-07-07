using System;
using System.Collections.Generic;
using System.Text;

namespace Blitzschutz_Rechner 
{
    internal class LightningProtectionCalculation
    {

        //Allowed Border Values

        private readonly double[] values_upper_value_border = { 30, 20, 30, 30, 18, 75 };
        //The last two 30's are actually 24s but if you just want to calculate the protection radius 30 is fine. 
        //So the handling for the protection area width is at the point, when the average pole height is set.
        private readonly double[] values_lower_value_border = { 0, 1, 0, 0, 1, 5 };


        //At the moment only for the one pole part. will later be expanded to multidimensional array
        private readonly double[] values_upper_chart_border = { 18.0721, 20.5314, 22.2571, 23.8227, 25.6187, 26.9989, 28.5016, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30 };
        private readonly double[] values_lower_chart_border = { 1.1720, 2.2321, 3.2466, 4.1892, 5.0890, 6.1104, 7.1310, 8.1611, 8.9416, 10.0295, 10.9904, 11.8418, 13.0007, 13.9913, 14.9657, 16.0861, 16.9282, 18.0794, 19.0405, 19.9652 };


        //Calculation Values

        #region Values_One_Pole
        private readonly double[,] Table_values_One_Pole = {
                                                {-0.000001392,0.000082846,-0.001858798,0.018063017,-0.042708078,1.441784477,-1.656879163},
                                                {0.000000151,-0.000004578,-0.000030332,0.001299886,0.017701899,1.211554417,-2.805948888},
                                                {0.000000561,-0.000038373,0.000977406,-0.012067323,0.099647662,0.839289882,-3.457619571},
                                                {0.000000568,-0.000042174,0.001181046,-0.016249949,0.143331192,0.477034787,-3.631530852},
                                                {0.000000338,-0.000026777,0.000801722,-0.011893797,0.115960435,0.504432987,-4.454812625},
                                                {0.000000311,-0.000026748,0.000867088,-0.013642533,0.132745172,0.340273633,-4.920129082},
                                                {0.000000432,-0.000043956,0.001773034,-0.036595437,0.429022256,-1.577400119,-1.128686254},
                                                {0.000000343,-0.000036598,0.001545926,-0.033440503,0.411275595,-1.633672717,-1.517175316},
                                                {0.000000067,-0.000004243,0.000014608,0.003790574,-0.072187314,1.413719747,-9.464338238},
                                                {0.000000127,-0.000010775,0.000277652,-0.000773321,-0.048494604,1.549910878,-11.731688858},
                                                {0.000000978,-0.000117902,0.005772329,-0.147130035,2.078087781,-14.427810477,35.839248256},
                                                {0.000001101,-0.000130354,0.006253124,-0.156172426,2.169921897,-15.078991355,37.967626093},
                                                {0.000000446,-0.000048313,0.002034479,-0.042102604,0.455840978,-1.514455642,-7.171736285},
                                                {0.000001341,-0.000174946,0.009321447,-0.259992559,4.027232821,-32.010215038,98.133195946},
                                                {0.000002313,-0.000306194,0.016652998,-0.476741565,7.598269203,-63.085687775,208.820188274},
                                                {0.000000917,-0.000121808,0.006623613,-0.188576219,2.973070390,-23.654064926,67.933263728},
                                                {0.000005110,-0.000701524,0.039720434,-1.187909941,19.823839161,-174.386350785,626.972271805},
                                                {0.000016080,-0.002304541,0.136730038,-4.299563431,75.605137351,-704.179061468,2708.448647105},
                                                {0.000005848,-0.000829451,0.048649548,-1.511558764,26.275691308,-241.726987104,913.654552851},
                                                {0.000073344,-0.010923572,0.676165314,-22.266142304,411.425549375,-4043.964707961,16513.187792803}
                                          };
        #endregion

        #region Values_Two_Poles
        private readonly double[,] Table_values_two_poles_part_1 = {
                                                        {-0.000000382, 0.000027988, -0.000818819, 0.012163113, -0.096297482, 0.717596331, -0.74680513},
                                                        {0.000000094, -0.000006571, 0.000158475, -0.001183346, -0.010383420, 0.543963821, -1.184139904},
                                                        {0.000000123, -0.000009232, 0.000254521, -0.002749428, -0.001141228, 0.582483296, -1.899321019},
                                                        {-0.000000149, 0.000012973, -0.000483221, 0.010072044, -0.125596098, 1.250959865, -3.861743296},
                                                        {-0.000000235, 0.000020166, -0.000718887, 0.013980874, -0.162853111, 1.490624706, -5.082530255},
                                                        {-0.000000093, 0.000007159, -0.000246470, 0.005568988, -0.090677838, 1.288621422, -5.907564383},
                                                        {0.000000220, -0.000022459, 0.000883317, -0.016494832, 0.137329262, 0.163371132, -4.511895593},
                                                        {0.000000335, -0.000035502, 0.001487780, -0.030871410, 0.318824639, -0.936128647, -2.755316669},
                                                        {0.000000707, -0.000073201, 0.003051994, -0.064776122, 0.720956797, -3.371072373, 2.459697435},
                                                        {0.000001460, -0.000152415, 0.006475609, -0.142414732, 1.691512860, -9.655333338, 18.133556673},
                                                        {0.000002297, -0.000255566, 0.011637455, -0.276644167, 3.601602795, -23.706976429, 59.127182320},
                                                        {-0.000001714, 0.000173299, -0.007238644, 0.161225581, -2.047411801, 14.781366246, -49.934168142},
                                                        {-0.000003869, 0.000513040, -0.027865390, 0.794945246, -12.580615113, 105.306487031, -366.419469610},
                                                        {0.000004754, -0.000505827, 0.021850297, -0.486246125, 5.792807686, -33.548773876, 64.346864579},
                                                        {0.000018869, -0.002160441, 0.102034079, -2.540696309, 35.114014892, -254.258323005, 747.093840742}
                                                  };
        private readonly double[,] Table_values_two_poles_part_2 = {
                                                        {0.026925313, -0.233871205, 0.755586716, -1.293762104, 2.024353111, 2.454178996, -0.845258079},
                                                        {-0.031765960, 0.405977169, -2.030741720, 4.908713127, -5.349525032, 5.926562447, -2.509725040},
                                                        {0.003929501, -0.057893375, 0.325243927, -0.934769732, 1.903657645, 1.202489379, -2.248654281},
                                                        {0.012372327, -0.212733065, 1.456745332, -5.128184846, 10.271311077, -7.890490026, 0.872974107},
                                                        {0.001220076, -0.024181433, 0.180086268, -0.690395200, 1.907271063, -0.214008878, -2.567092818},
                                                        {0.006801420, -0.150645068, 1.346982792, -6.283074551, 16.617578083, -20.752389596, 8.438275609},
                                                        {0.010201810, -0.234732083, 2.196568510, -10.770983097, 29.742403345, -41.352529914, 21.197707422},
                                                        {0.006260449, -0.160184556, 1.668335485, -9.096480540, 27.806075293, -42.558470087, 23.554808618},
                                                        {0.009845243, -0.268259738, 2.985505028, -17.406798861, 56.474980338, -94.231422968, 60.926738427},
                                                        {-0.001894494, 0.068397301, -0.998465041, 7.501218746, -30.289821084, 65.165471596, -60.382871953},
                                                        {0.017200226, -0.493248519, 5.813480822, -36.091718504, 124.931540674, -226.656742655, 165.329304790},
                                                        {0.081753004, -2.502284771, 31.683149628, -212.475111861, 796.443778675, -1580.212209904, 1293.157955948},
                                                        {0.044504886, -1.458010565, 19.734374703, -141.314733714, 565.143051952, -1195.080879637, 1040.470637220},
                                                        {0.680192596, -22.130665412, 299.034521696, -2148.006035754, 8651.373029454, -18522.881079212, 16466.008127973},
                                                        {0, 0, -0.019169673, 0.374535030, -2.496442738, 9.258911251, -18.223367561},
                                                        {0, 0, 0, -0.242565202, 4.404287831, -23.550832362, 37.614039874},
                                                        {0, 0, 0, 0, 0.280965344, -0.511061025, -5.599449673},
                                                        {0, 0, 0, 0, 0, 2.698400300, -15.618880614}
                                                  };
        #endregion

        internal double Calculate_Protection_Radius(double pole_height, double protection_height)
        {
            double result = 0, lower_result = 0, higher_result = 0, difference = 0, factorized_difference = 0;//factorized_difference is just used for better understanding

            if (protection_height != (int)protection_height)
            {
                lower_result = Calculate_protection_radius_raw_values(pole_height, (int)protection_height);
                higher_result = Calculate_protection_radius_raw_values(pole_height, (int)protection_height + 1);
                difference = higher_result - lower_result;
                factorized_difference = difference * (protection_height - (int)protection_height);
                result = lower_result + factorized_difference;
            }
            else if (protection_height == (int)protection_height)
            {
                result = Calculate_protection_radius_raw_values(pole_height, (int)protection_height);
            }
            return result;
        }

        private double Calculate_protection_radius_raw_values(double pole_height, int protection_height)
        {
            double raw_result = 0;
            for (int i = 0, j = 6; i <= 6; i++, j--)
            {
                raw_result += (double)Math.Pow(pole_height, j) * Table_values_One_Pole[protection_height - 1, i]; //The -1 is because the array starts at 0
            }
            return raw_result;
        }

        internal double Calculate_Protection_Floor_Width(double average_pole_height, double protection_height, double pole_distance)
        {
            double protocol = 0, temp_result = 0, result = 0, lower_result = 0, higher_result = 0, difference = 0, factorized_difference = 0;//factorized_difference is just used for better understanding

            //PART 1
            if (pole_distance != (five_inting(pole_distance)))
            {
                protocol = five_inting(pole_distance);
                higher_result = Calculate_Protection_Floor_Width_raw_values_part1(average_pole_height, five_inting(pole_distance));
                lower_result = Calculate_Protection_Floor_Width_raw_values_part1(average_pole_height, five_inting(pole_distance) + 5);
                difference = higher_result - lower_result;
                factorized_difference = difference * (5 - (pole_distance - five_inting(pole_distance))) / 5;
                // 5- ... because the higher the pole distance the lower the result, because you calculate with the
                // lower and higher pole distance, the higher pole distance has a higher value than the lower
                // therefore it has to be taken 5 - difference_between_value_and_five_inted_pole_distance
                temp_result = lower_result + factorized_difference;
            }   // '/5' because the difference between the numbers is 5 but the factor must be something between 0 and 1.
            else if (pole_distance == (five_inting(pole_distance)))
            {
                temp_result = Calculate_Protection_Floor_Width_raw_values_part1(average_pole_height, pole_distance);
            }

            //PART 2
            if (protection_height != (int)protection_height)
            {
                higher_result = Calculate_Protection_Floor_Width_raw_values_part2(temp_result, (int)protection_height);
                lower_result = Calculate_Protection_Floor_Width_raw_values_part2(temp_result, (int)protection_height + 1);
                difference = higher_result - lower_result;
                factorized_difference = difference * (1 - (protection_height - (int)protection_height));
                result = lower_result + factorized_difference;
            }
            else if (protection_height == (int)protection_height)
            {
                result = Calculate_Protection_Floor_Width_raw_values_part2(temp_result, protection_height);
            }


            return result;
        }

        private double Calculate_Protection_Floor_Width_raw_values_part1(double average_pole_height, double pole_distance)
        {
            double temp_result = 0;
            int converted_pole_distance = 0;

            converted_pole_distance = five_inting(pole_distance) / 5;
            //must still be taken -1 because the array starts with 0

            for (int i = 0, j = 6; i <= 6; i++, j--)
            {
                temp_result += (double)Math.Pow(average_pole_height, j) * Table_values_two_poles_part_1[converted_pole_distance - 1, i];
            }

            return temp_result;
        }
        private double Calculate_Protection_Floor_Width_raw_values_part2(double temp_result, double protection_height)
        {
            double final_result = 0;

            for (int i = 0, j = 6; i <= 6; i++, j--)
            {
                final_result += (double)Math.Pow(temp_result, j) * Table_values_two_poles_part_2[(int)protection_height - 1, i];
            }

            return final_result;
        }

        private int five_inting(double input)
        {   //to get the next lower 5*x number
            int result = 0;

            result = Convert.ToInt32(input);
            result -= (result % 5);
            return result;
        }
    }
}