using System;
using System.Linq;
using System.Text.RegularExpressions;
using Bank.Objects;

namespace Bank.Validator
{
    public class Validator
    {
        public static bool StringAllLetter(string text)
        {
            return text.All(Char.IsLetter);
        }

        public static bool StringAllDigit(string text)
        {
            return text.All(Char.IsDigit);
        }              

        public static bool IsCompanyNumber(string text)
        {
            return text.Length == 3;
        }

        public static bool IsSSN(string text)
        {
            return text.Length == 10;
        }

        internal static bool CredentialsEmpty(string login, string password)
        {
            return string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password);
        }

        /// <summary>
        ///  3 from 5 conditions must be met
        ///    1) At least one lower case letter,
        ///    2) At least one upper case letter,
        ///    3) At least special character,
        ///    4) At least one number
        ///    5) At least 4 characters length
        /// </summary>
        /// <param name="passWord"></param>
        /// <returns></returns>
        internal static bool ValidatePassword(string passWord)
        {
            int validConditions = 0;

            if (passWord.Length >= User.passwordMinLength)
            {
                validConditions++;
            }

            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }

            if (validConditions == 0) return false;

            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }

            if (validConditions == 1) return false;

            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=', '.' };
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }

        internal static bool IsAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex("[^a-zA-Z0-9]");

            //if has non AlpahNumeric char, return false, else return true.
            return rg.IsMatch(strToCheck) == true ? false : true;
        }

        internal static bool NameValidator(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-ZáčďéěíňóřšťúůýžÁČĎÉĚÍŇÓŘŠŤÚÝŽ]+((\s|\-)[a-zA-ZáčďéěíňóřšťúůýžÁČĎÉĚÍŇÓŘŠŤÚÝŽ]+)?$");

            return rg.IsMatch(strToCheck) == true ? true : false;
        }

        internal static bool EmailValidator(string strToCheck)
        {
            Regex rg = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");

            return rg.IsMatch(strToCheck) == true ? true : false;
        }

        internal static bool PhoneValidator(string strToCheck)
        {
            Regex rg = new Regex(@"^(\+420)? ?[1-9][0-9]{2} ?[0-9]{3} ?[0-9]{3}$");

            return rg.IsMatch(strToCheck) == true ? true : false;
        }

        internal static bool StreetValidator(string strToCheck)
        {
            Regex rg = new Regex(@"^(.*[^0-9]+)$");

            return rg.IsMatch(strToCheck) == true ? true : false;

        }

        internal static bool StreetNumberValidator(string strToCheck)
        {
            Regex rg = new Regex(@"^(([1-9][0-9]*)/)?([1-9][0-9]*[a-cA-C]?)$");

            return rg.IsMatch(strToCheck) == true ? true : false;

        }

        internal static bool CityValidator(string strToCheck)
        {
            Regex rg = new Regex(@"^([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$");

            return rg.IsMatch(strToCheck) == true ? true : false;
        }

        internal static bool PostalCodeValidator(string strToCheck)
        {
            Regex rg = new Regex(@"\d{3} ?\d{2}");

            return rg.IsMatch(strToCheck) == true ? true : false;

        }

        internal static bool OfficialUserNameValidator(string strToCheck)
        {
            Regex rg = new Regex(@"[0-9]{3}");

            return rg.IsMatch(strToCheck) == true ? true : false;
        }

    }
}

