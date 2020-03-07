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
        private static String selectCountbyCompanyNumber =
            "SELECT count(*) FROM users WHERE CompanyNumber=@CompanyNumber";

        private static String selectByCompanyNumber =
            "SELECT * FROM users WHERE CompanyNumber=@CompanyNumber";

        private static String createOfficial =
            "insert into Users values (@Guid, @Name, @Surname, @Address, @Mail, @Phone, @valid, @Type, @CompanyNumber, @Password, NULL, NULL, NULL, NULL, NULL)";

        private static String selectOfficials =
            "SELECT * FROM users WHERE CompanyNumber is not null";

        private static String selectAddressById =
            "SELECT * FROM address WHERE AddressId = @AddressId";

        private static String deleteOfficial =
            "update users set valid = 0 where CompanyNumber = @CompanyNumber";

        private static String updateOfficial =
            "update users set Name = @Name, Surname = @Surname, Mail = @Mail, Phone = @Phone, OfficialType = @OfficialType where CompanyNumber = @CompanyNumber ";

               
        public static bool CreateNewOfficial(Official official)
        {
            if (!CreateAddress(official.Address))
                return false;

            DBConnection connection = new DBConnection();
            connection.OpenConection();
            SqlCommand command = connection.CreateCommand(createOfficial);

            command.Parameters.AddWithValue("@GUID", official.Guid);
            command.Parameters.AddWithValue("@Name", official.Name);
            command.Parameters.AddWithValue("@Surname", official.SurName);
            command.Parameters.AddWithValue("@Address", official.Address.Id);
            command.Parameters.AddWithValue("@Mail", official.Mail);
            command.Parameters.AddWithValue("@Phone", official.Phone);
            command.Parameters.AddWithValue("@valid", official.Valid);
            command.Parameters.AddWithValue("@Password", official.Password);
            command.Parameters.AddWithValue("@OfficialType", official.OfficialType);
            command.Parameters.AddWithValue("@CompanyNumber", official.CompanyNumber);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public static bool DeleteOfficial(Official official)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();
            SqlCommand command = connection.CreateCommand(deleteOfficial);
            command.Parameters.AddWithValue("@CompanyNumber", official.CompanyNumber);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public static Official GetOfficialById(Official official)
        {
            int count = 0;

            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectByCompanyNumber);
            command.Parameters.AddWithValue("@CompanyNumber", official.CompanyNumber);

            SqlCommand commandCount = connection.CreateCommand(selectCountbyCompanyNumber);
            commandCount.Parameters.AddWithValue("@CompanyNumber", official.CompanyNumber);
        
            count = (int)commandCount.ExecuteScalar();

            SqlDataReader reader = command.ExecuteReader();

            switch (count)
            {
                case 0:
                    MessageBox.Show("Zadaný úředník neexistuje");
                    connection.CloseConnection();
                    return null;
                case 1:
                    while (reader.Read())
                    {
                        official.Guid = reader.GetGuid(0);
                        official.Name = reader.GetString(1);
                        official.SurName = reader.GetString(2);
                        official.Address = new Address();
                        official.Address.Id = reader.GetInt32(3);
                        official.Mail = reader.GetString(4);
                        official.Phone = reader.GetString(5);
                        official.Valid = reader.GetBoolean(6);
                        official.OfficialType = (OfficialType)reader.GetInt32(7); //GetInt32 vrací číselnou hodnotu a (OfficialType) to přetypuje na ten správný typ
                        official.CompanyNumber = reader.GetString(8);
                        official.Password = reader.GetString(9);
                    }


                    connection.CloseConnection();
                    return official;
                default:
                    MessageBox.Show("v DB existuje více uživatelů se stéjným ID! ");
                    return null;
            }




        }

        public static List<Official> GetAllOfficials()
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectOfficials);

            SqlDataReader reader = command.ExecuteReader();
            List<Official> officials = new List<Official>();
            while (reader.Read())
            {
                Official official = new Official();
                official.Guid = reader.GetGuid(0);
                official.Name = reader.GetString(1);
                official.SurName = reader.GetString(2);
                official.Address = new Address();
                official.Address.Id = reader.GetInt32(3);
                official.Mail = reader.GetString(4);
                official.Phone = reader.GetString(5);
                official.Valid = reader.GetBoolean(6);
                official.OfficialType = (OfficialType)reader.GetInt32(7);
                official.CompanyNumber = reader.GetString(8);
                official.Password = reader.GetString(10);
                officials.Add(official);
            }

            return officials;
        }


        public static Address GetAddress(Int32 addressId)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectAddressById);
            command.Parameters.AddWithValue("@AddressId", addressId);

            Address address = new Address();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                address.Id = reader.GetInt32(0);
                address.Street = reader.GetString(1);
                address.StreetNumber = reader.GetString(2);
                address.City = reader.GetString(3);
                address.PostalCode = reader.GetString(4);
                address.Country = reader.GetString(5);
            }
            return address;
        }


        public static bool UpdateOfficial(Official official)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(updateOfficial);
            command.Parameters.AddWithValue("@Name", official.Name);
            command.Parameters.AddWithValue("@Surname", official.SurName);
            command.Parameters.AddWithValue("@Mail", official.Mail);
            command.Parameters.AddWithValue("@Phone", official.Phone);
            command.Parameters.AddWithValue("@OfficialType", official.OfficialType);
            command.Parameters.AddWithValue("@CompanyNumber", official.CompanyNumber);


            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }
    }
}
