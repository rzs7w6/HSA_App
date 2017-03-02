using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSA_App
{
    internal class RegistrationParser
    {
        static int check;
        enum errors { act_num, act_name, act_pass };
        public static int checkAccount(string actNum)
        {
            if (actNum == null || actNum == "" || actNum.Length != 11 || int.TryParse(actNum, out check) == false)
            {
                return (int) errors.act_num;
            }
            else
            {
                //correct value
                return 39;
            }
        }

        public static int checkName(string actName)
        {
            if (actName == null || actName == "" || actName.Length > 45 || int.TryParse(actName, out check) == true || actName.All(char.Equals(" ")) == true)
            {
                return act_name;
            }
            //correct value
            return 39;
        }


        public static int checkPassword(string actPass)
        {
            bool symbol = false;
            bool uppercase = false;
            bool lowercase = false;
            bool number = false;

            if(actPass == null || actPass == "" || actPass.Length() > 45)
            {
                return act_pass;
            }

            foreach ( Char letter in actPass)
            {
                if(letter.IsSymbol())
                {
                    symbol = true;
                }
                if (letter.IsUpper())
                {
                    uppercase = true;
                }
                if (letter.IsLower())
                {
                    lowercase = true;
                }
                if (letter.IsDigit())
                {
                    number = true;
                }
            }
            if(symbol == false || uppercase == false || lowercase == false || number == false)
            {
                return act_pass;
            }

          //correct value
          return 39;
        } 


     }
}
