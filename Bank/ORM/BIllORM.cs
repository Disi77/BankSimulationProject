using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Objects;

namespace Bank.ORM
{
    public class BillORM
    {
        private static String selectBillByCustomerId =
            "select * from bill where Id=@CustomerId";

        private static String selectBillByBillNum =
            "SELECT * FROM bill where BillNumber=@BillNumber";

        private static String selectAllBills =
            "SELECT * FROM bill";

        private static String setBillBalance =
            "UPDATE bill set Balance = @Balance WHERE BillNumber = @BillNumber";

        public static List<Bill> GetBillsByCustomerId(Customer customer)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectBillByCustomerId);
            command.Parameters.AddWithValue("@CustomerId", customer.Guid);

            SqlDataReader reader = command.ExecuteReader();

            List<Bill> bills = new List<Bill>();
            while (reader.Read())
            {
                Bill bill = new Bill();
                bill.BillNumber = reader.GetInt32(0);
                bill.CustomerId = reader.GetGuid(1);
                bill.Balance = reader.GetInt32(2);
                bill.Valid = reader.GetBoolean(3);
                bills.Add(bill);
            }
            connection.CloseConnection();
            return bills;
        }

        public static Bill GetBillbyBillNumber(int billNum)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectBillByBillNum);
            command.Parameters.AddWithValue("@BillNumber", billNum);

            SqlDataReader reader = command.ExecuteReader();

            Bill bill = new Bill();
            while (reader.Read())
            {
                bill.BillNumber = reader.GetInt32(0);
                bill.CustomerId = reader.GetGuid(1);
                bill.Balance = reader.GetInt32(2);
                bill.Valid = reader.GetBoolean(3);
            }
            connection.CloseConnection();
            return bill;
        }

        public static List<Bill> GetBills()
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectAllBills);
            SqlDataReader reader = command.ExecuteReader();

            List<Bill> bills = new List<Bill>();
            while (reader.Read())
            {
                Bill bill = new Bill();
                bill.BillNumber = reader.GetInt32(0);
                bill.CustomerId = reader.GetGuid(1);
                bill.Balance = reader.GetInt32(2);
                bill.Valid = reader.GetBoolean(3);
                bills.Add(bill);
            }
            connection.CloseConnection();
            return bills;
        }

        public static bool SetNewBillBalance(Bill bill)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();
            SqlCommand command = connection.CreateCommand(setBillBalance);

            command.Parameters.AddWithValue("@Balance", bill.Balance);
            command.Parameters.AddWithValue("@BillNumber", bill.BillNumber);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public static void UpdateBillBalance(Transaction transaction)
        {
            Bill billPayer = GetBillbyBillNumber(transaction.PayerBillNum);
            Bill billRecipient = GetBillbyBillNumber(transaction.RecipientBillNum);

            billPayer.Balance -= transaction.Amount;
            billRecipient.Balance += transaction.Amount;

            SetNewBillBalance(billPayer);
            SetNewBillBalance(billRecipient);

        }

    }
}
