using System;
using Bank.Types;

namespace Bank.Objects
{
    public class Official : User
    {
        public OfficialType OfficialType { get; set; }
        public String CompanyNumber { get; set; }
        public String Password { get; set; }
    }
}
