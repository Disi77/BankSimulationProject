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
        public override string ToString()
        {
            return String.Format("From Payer Bill: {0} to Recipient Bill: {1} VS: {2} Date: {3} Amount: {4}", 
                PayerBillNum, 
                RecipientBillNum,
                VariableSymbol,
                DateTransaction,
                Amount);
        }

    }
}
