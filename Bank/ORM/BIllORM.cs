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
    }
}
