using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPOS_LCD210
{
    public class Helper
    {
        private string DEFAULT_STRING_20 = "                    ";//20 spaces
        private string DEFAULT_STRING_40 = "                                        ";//40 spaces
        private string DEFAULT_WELCOME = "POA SUPERMARKET     WELCOME TO          ";//40 spaces
        private string DEFAULT_TOTAL = "TOTAL               ";//20 spaces
        public Helper() { }

        private string setCommas(string str) {
            return String.Format("{0:n0}", str);
        }

        private string getCustomString(string str)
        {
            int original_len = str.Length;
            if (original_len == 0 || original_len > 20) {
                str = DEFAULT_STRING_20;
                original_len = str.Length;
            }

            int spaces_to_add = 20 - original_len;
            string spaces = "";
            for (int i = 0; i < spaces_to_add;i++ )
            {
                spaces += " ";
            }
            //combine the string with valid spaces
            string res = str + spaces;
            return res + DEFAULT_TOTAL;//40
        }


        public string getCustomPriceToDisplay(string str){
            string input = str;//no need to set commas
            string customPriceInput = getCustomString(input);
            if (str == "0" || str == "0.0")
            {
                return DEFAULT_WELCOME;
            }
            if (customPriceInput.Length != 40)
            {
                string res = "";
                //res = DEFAULT_TOTAL + DEFAULT_STRING_20;
                res = DEFAULT_WELCOME;
                return res;
            }
            else { 
                string res = "";
                res = customPriceInput;
                return res;
            }
        }


    }
}
