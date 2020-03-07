using System;
using Bank.Types;

namespace Bank.Objects
{
    public class Admin : User
    {
        public AdminType AdminType { get; set; }
        public String Password { get; set; }
        public String Login { get; set; }
    }
}
