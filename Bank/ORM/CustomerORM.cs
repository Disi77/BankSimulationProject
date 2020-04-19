using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Bank.Objects;
using Bank.Types;

namespace Bank.ORM
{
    public partial class UsersORM
    {
        private static String createCustomer =
            "insert into users values (@Guid, @Name, @Surname, @Address, @Mail, @Phone, @Valid, NULL, NULL, @Password, NULL, @SSN, @Type, NULL, NULL)";
        
        private static String selectCustomerbySSN =
            "SELECT * FROM users WHERE SSN=@SSN";
       
        private static String selectCountCustomerbySSN =
            "SELECT COUNT(*) FROM users WHERE SSN=@SSN";
        
        private static String updateCustomer =
            "update users set Name = @Name, Surname = @Surname, Mail = @Mail, Phone = @Phone, AddressId = @AddressId, CustomerType = @CustomerType where SSN = @SSN";

        private static String selectAllCustomers =
            "SELECT* FROM users WHERE SSN is not null";

        public static bool CreateNewCustomer(Customer customer)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();
            SqlCommand command = connection.CreateCommand(createCustomer);

            command.Parameters.AddWithValue("@GUID", customer.Guid);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@Surname", customer.SurName);
            command.Parameters.AddWithValue("@Address", customer.Address.Id);
            command.Parameters.AddWithValue("@Mail", customer.Mail);
            command.Parameters.AddWithValue("@Phone", customer.Phone);
            command.Parameters.AddWithValue("@Valid", customer.Valid);
            command.Parameters.AddWithValue("@Type", customer.CustomerType);
            command.Parameters.AddWithValue("@Password", customer.Password);
            command.Parameters.AddWithValue("@SSN", customer.SSN);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public static Customer GetCustomerBySSN(string SSN)
        {
            int count = 0;

            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectCustomerbySSN);
            command.Parameters.AddWithValue("@SSN", SSN);

            SqlCommand commandCount = connection.CreateCommand(selectCountCustomerbySSN);
            commandCount.Parameters.AddWithValue("@SSN", SSN);

            count = (int)commandCount.ExecuteScalar();

            SqlDataReader reader = command.ExecuteReader();

            Customer customer = new Customer();

            switch (count)
            {
                case 0:
                    MessageBox.Show("Customer doesn't exist. ");
                    connection.CloseConnection();
                    return null;
                case 1:
                    while (reader.Read())
                    {
                        customer.Guid = reader.GetGuid(0);
                        customer.Name = reader.GetString(1);
                        customer.SurName = reader.GetString(2);
                        customer.Address = new Address();
                        customer.Address.Id = reader.GetInt32(3);
                        customer.Mail = reader.GetString(4);
                        customer.Phone = reader.GetString(5);
                        customer.Valid = reader.GetBoolean(6);
                        customer.Password = reader.GetString(9);
                        customer.SSN = reader.GetString(11);
                        customer.CustomerType = (CustomerType)reader.GetInt32(12);                     
                    }

                    connection.CloseConnection();
                    return customer;
                default:
                    MessageBox.Show("v DB existuje více uživatelů se stéjným ID! ");
                    return null;
            }



        }

        public static bool UpdateCustomer(Customer customer)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(updateCustomer);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@Surname", customer.SurName);
            command.Parameters.AddWithValue("@Mail", customer.Mail);
            command.Parameters.AddWithValue("@Phone", customer.Phone);
            command.Parameters.AddWithValue("@AddressId", customer.Address.Id);
            command.Parameters.AddWithValue("@CustomerType", customer.CustomerType);
            command.Parameters.AddWithValue("@SSN", customer.SSN);


            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) 
                return true;
            return false;
        }

        public static List<Customer> GetAllCustomers()
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectAllCustomers);

            SqlDataReader reader = command.ExecuteReader();
            List<Customer> customers = new List<Customer>();
            while (reader.Read())
            {
                Customer customer = new Customer();
                customer.Guid = reader.GetGuid(0);
                customer.Name = reader.GetString(1);
                customer.SurName = reader.GetString(2);
                customer.Address = new Address();
                customer.Address.Id = reader.GetInt32(3);
                customer.Mail = reader.GetString(4);
                customer.Phone = reader.GetString(5);
                customer.Valid = reader.GetBoolean(6);
                customer.CustomerType = (CustomerType)reader.GetInt32(12);
                customer.SSN = reader.GetString(11);
                customer.Password = reader.GetString(9);
                customers.Add(customer);
            }

            return customers;
        }

    }


}
