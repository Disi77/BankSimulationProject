using System;
using Bank.Types;

namespace Bank.Objects
{
    public class Customer : User
    {
        public CustomerType CustomerType { get; set; }
        public String SSN { get; set; }
        public String Password { get; set; }      
    }
}
