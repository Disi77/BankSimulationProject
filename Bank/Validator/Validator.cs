using System;
using System.Linq;

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
}
