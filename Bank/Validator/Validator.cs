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

        public static bool ValidateStreet(String street)
        {
            try
            {
                return street.All(Char.IsLetter);// ověření, že ulice obsahuje pouze písmena. vratí true nebo false
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;

            }

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

        internal static bool isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex("[^a-zA-Z0-9]");

            //if has non AlpahNumeric char, return false, else return true.
            return rg.IsMatch(strToCheck) == true ? false : true;
        }
    }



    // vyjímka se má zachytit do souboru s časem a kde s



    /*
    Pro ty kteří by chtěli použít hotové validace existuje knihovna
    using System.ComponentModel.DataAnnotations;

    Ale pozor přidání této knihovny je třeba udělat přes NuGet Manager….
    Na netu je toho k Nuget manageru hafo

    Např toto:

    public bool IsValidEmail(string mail)
    {
        return new EmailAddressAttribute().IsValid(mail);
    }

    */
}

