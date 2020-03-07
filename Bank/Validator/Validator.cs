using System;
using System.Linq;

namespace Bank.Validator
{
    public class Validator
    {
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
