using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Objects
{
    public class Transaction
    {
        public int Id { get; set; }

        public int VariableSymbol { get; set; }

        public DateTime DateTransaction { get; set; }

        public int PayerBillNum { get; set; }

        public int RecipientBillNum { get; set; }

        public int Amount { get; set; }

        public Boolean Valid { get; set; }

    }
}
