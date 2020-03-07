using Bank.Objects;
using Bank.Types;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Documents;

namespace Bank.ORM
{
    public partial class UsersORM
    {
        private static String createNewAddress = 
            "INSERT INTO Address VALUES (@AddressId,@Street,@StreetNumber,@City,@PostalCode,@Country)";
        private static String getAddressWithLastId = 
            "SELECT TOP(1) * FROM address ORDER BY AddressId desc";
        private static String selectByAddressId =
            "SELECT * FROM Address WHERE AddressId=@AddressId";
        private static String selectCountbyAddressId =
            "SELECT count(*) FROM Address WHERE AddressId=@AddressId";

        public static bool CreateAddress(Address address)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();
            SqlCommand command = connection.CreateCommand(createNewAddress);

            command.Parameters.AddWithValue("@AddressId", address.Id);
            command.Parameters.AddWithValue("@Street", address.Street);
            command.Parameters.AddWithValue("@StreetNumber", address.StreetNumber);
            command.Parameters.AddWithValue("@City", address.City);
            command.Parameters.AddWithValue("@PostalCode", address.PostalCode);
            command.Parameters.AddWithValue("@Country", address.Country);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;

        }

        public static Address SelectAddressById(int addressId)
        {
            int count;
            Address address = new Address();
            address.Id = addressId;


            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectByAddressId);
            command.Parameters.AddWithValue("@AddressId", address.Id);

            SqlCommand commandCount = connection.CreateCommand(selectCountbyAddressId);
            commandCount.Parameters.AddWithValue("@AddressId", address.Id);

            count = (int)commandCount.ExecuteScalar();

            SqlDataReader reader = command.ExecuteReader();

            switch (count)
            {
                case 0:
                    MessageBox.Show("Address does not exist.");
                    connection.CloseConnection();
                    return null;
                case 1:
                    Address newAddress = new Address();
                    while (reader.Read())
                    {
                        newAddress.Id = reader.GetInt32(0);
                        newAddress.Street = reader.GetString(1);
                        newAddress.StreetNumber = reader.GetString(2);
                        newAddress.City = reader.GetString(3);
                        newAddress.PostalCode = reader.GetString(4);
                        newAddress.Country = reader.GetString(5);
                    }
                    connection.CloseConnection();
                    return newAddress;
                default:
                    MessageBox.Show("More than one Address with same ID in Database. Check your reques.");
                    return null;
            }
        }

        public static int GetNewAddressId()
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();
            SqlCommand command = connection.CreateCommand(getAddressWithLastId);

            SqlDataReader reader = command.ExecuteReader();

            Address address = new Address();
            while (reader.Read())
            {
                address.Id = reader.GetInt32(0);
            }
            connection.CloseConnection();
            int result = address.Id;
            result++;

            return result;

        }

    }
}
