using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Objects;

namespace Bank.ORM
{
    public class TransactionORM
    {
        private static String getLastTransactionId =
            "select top(1) * from Transactions order by Id desc";

        private static String insertNewTransaciton =
            "insert into transactions values (@Id, @VariableSymbol, @DateTransaction, @PayerBillNum, @RecipientBillNum, @Amount, @Valid)";

        private static String selectTransactionsByBillId =
            "select * from transactions where Payer=@BillPayer or Recipient=@BillRecipient";

        public static int GetNewTransactionId()
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(getLastTransactionId);

            SqlDataReader reader = command.ExecuteReader();

            Transaction transaction = new Transaction();
            while (reader.Read())
            {
                transaction.Id = reader.GetInt32(0);
            }

            int newTransactionId = transaction.Id + 1;

            connection.CloseConnection();
            return newTransactionId;
        }

        public static bool CreateNewTransaction(Transaction transaction)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();
            SqlCommand command = connection.CreateCommand(insertNewTransaciton);

            command.Parameters.AddWithValue("@Id", transaction.Id);
            command.Parameters.AddWithValue("@VariableSymbol", transaction.VariableSymbol);
            command.Parameters.AddWithValue("@DateTransaction", transaction.DateTransaction);
            command.Parameters.AddWithValue("@PayerBillNum", transaction.PayerBillNum);
            command.Parameters.AddWithValue("@RecipientBillNum", transaction.RecipientBillNum);
            command.Parameters.AddWithValue("@Amount", transaction.Amount);
            command.Parameters.AddWithValue("@Valid", transaction.Valid);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public static List<Transaction> GetTransactionByBillId(Bill bill)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectTransactionsByBillId);
            command.Parameters.AddWithValue("@BillPayer", bill.BillNumber);
            command.Parameters.AddWithValue("@BillRecipient", bill.BillNumber);

            SqlDataReader reader = command.ExecuteReader();

            List<Transaction> transactions = new List<Transaction>();
            while (reader.Read())
            {
                Transaction t = new Transaction();
                t.Id = reader.GetInt32(0);
                t.VariableSymbol = reader.GetInt32(1);
                t.DateTransaction = reader.GetDateTime(2);
                t.PayerBillNum = reader.GetInt32(3);
                t.RecipientBillNum = reader.GetInt32(4);
                t.Amount = reader.GetInt32(5);             
                t.Valid = reader.GetBoolean(6);
                transactions.Add(t);
            }
            connection.CloseConnection();
            return transactions;
        }

    }
}
