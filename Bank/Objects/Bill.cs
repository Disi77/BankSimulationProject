using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Objects
{
    public class Bill
    {
        public int BillNumber { get; set; }

        public Guid CustomerId { get; set; }

        public int Balance { get; set; }

        public Boolean Valid { get; set; }

        public override string ToString()
        {
            return BillNumber.ToString();
        }
    }
}
