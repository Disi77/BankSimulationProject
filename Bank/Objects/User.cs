using System;

namespace Bank.Objects
{
    public class User
    {
        public Guid Guid { get; set; }
        public String Name { get; set; }
        public String SurName { get; set; }
        public Address Address { get; set; }
        public String Mail { get; set; }
        public String Phone { get; set; }
        public Boolean Valid { get; set; }

        public static int passwordMinLength = 4;
    }
}
