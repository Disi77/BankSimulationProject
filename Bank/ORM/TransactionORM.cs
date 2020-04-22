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



    }
}
