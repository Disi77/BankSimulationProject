using Bank.Logger;
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
        private static String selectAdminCount = 
            "SELECT count(*) FROM users WHERE AdminLogin=@AdminLogin and WorkPassword=@Password";
        private static String selectByLogin = 
            "SELECT * FROM users WHERE AdminLogin=@AdminLogin and WorkPassword=@Password";
        private static String insertNewAdmin =
           "insert into Users values (@GUID,@Name,@Surname,@Address,@Mail,@Phone,@valid,NULL,NULL,NULL,@Password,NULL,NULL,@Type,@Login)";
        private static String selectAllAdmins = 
            "SELECT * FROM users WHERE AdminType IS NOT NULL";
        private static String changePassword = 
            "UPDATE users SET WorkPassword = @Password where id=@GUID";
        private static String getAdminByGuid =
            "SELECT * FROM users WHERE Id=@GUID";
        private static String getCountAdminByGuid =
            "SELECT count(*) FROM users WHERE Id=@GUID";
        private static String updateAdmin =
            "UPDATE users SET Name=@Name, Surname=@Surname, AddressId=@AddressId, Mail=@Mail, Phone=@Phone, AdminType=@AdminType, AdminLogin=@Login WHERE Id=@GUID";


        public static Admin GetAdmin(string login, string password)
        {
            int count;

            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectByLogin);
            command.Parameters.AddWithValue("@AdminLogin", login);
            command.Parameters.AddWithValue("@Password", password);

            SqlCommand commandCount = connection.CreateCommand(selectAdminCount);
            commandCount.Parameters.AddWithValue("@AdminLogin", login);
            commandCount.Parameters.AddWithValue("@Password", password);
            count = (int)commandCount.ExecuteScalar();

            SqlDataReader reader = command.ExecuteReader();
            Log log = new Log();

            switch (count)
            {
                case 0:
                    MessageBox.Show("User with this login and password doesn't exist.");
                    log.Info(string.Format("Admin with login name {0} doesn't exist in DB", login));
                    return null;
                case 1:
                    Admin admin = new Admin();
                    while (reader.Read())
                    {
                        admin.Guid = reader.GetGuid(0);
                        admin.Name = reader.GetString(1);
                        admin.SurName = reader.GetString(2);
                        admin.Address = new Address();
                        admin.Address.Id = reader.GetInt32(3);
                        admin.Password = reader.GetString(10);
                        admin.AdminType = (AdminType)reader.GetInt32(13);
                    }
                    connection.CloseConnection();
                    return admin;
                default:
                    MessageBox.Show("Invalid login combination.");
                    log.Warning(string.Format("Multiple records with same login name = \'{0}\' and password in DB", login));
                    return null;
            }
        }

        public static Admin GetAdminByGuid(Admin admin)
        {
            int count;

            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(getAdminByGuid);
            command.Parameters.AddWithValue("@GUID", admin.Guid.ToString());

            SqlCommand commandCount = connection.CreateCommand(getCountAdminByGuid);
            commandCount.Parameters.AddWithValue("@GUID", admin.Guid.ToString());
            count = (int)commandCount.ExecuteScalar();

            SqlDataReader reader = command.ExecuteReader();

            switch (count)
            {
                case 0:
                    MessageBox.Show("User does not exist.");
                    return null;
                case 1:
                    while (reader.Read())
                    {
                        admin.Guid = reader.GetGuid(0);
                        admin.Name = reader.GetString(1);
                        admin.SurName = reader.GetString(2);
                        admin.Address = new Address();
                        admin.Address.Id = reader.GetInt32(3);
                        admin.Mail = reader.GetString(4);
                        admin.Phone = reader.GetString(5);
                        admin.Valid = reader.GetBoolean(6);
                        admin.Password = reader.GetString(10);
                        admin.AdminType = (AdminType)reader.GetInt32(13);
                        admin.Login = reader.GetString(14);

                    }
                    connection.CloseConnection();
                    return admin;
                default:
                    MessageBox.Show("There is multiple records in Database with this ID.");
                    return null;
            }
        }
        
        public static List<Admin> GetAdmins()
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(selectAllAdmins);
            SqlDataReader reader = command.ExecuteReader();

            List<Admin> admins = new List<Admin>();
            while (reader.Read())
            {
                Admin admin = new Admin();
                admin.Guid = reader.GetGuid(0);
                admin.Name = reader.GetString(1);
                admin.SurName = reader.GetString(2);
                admin.Address = new Address();
                admin.Address.Id = reader.GetInt32(3);
                admin.Mail = reader.GetString(4);
                admin.Phone = reader.GetString(5);
                admin.Valid = reader.GetBoolean(6);
                admin.AdminType = (AdminType)reader.GetInt32(13);
                admin.Login = reader.GetString(14);
                admins.Add(admin);
            }
            connection.CloseConnection();
            return admins;         
        }

        public static bool CreateAdmin(Admin admin)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();
            SqlCommand command = connection.CreateCommand(insertNewAdmin);

            command.Parameters.AddWithValue("@GUID", admin.Guid);
            command.Parameters.AddWithValue("@Name", admin.Name);
            command.Parameters.AddWithValue("@Surname", admin.SurName);
            command.Parameters.AddWithValue("@Address", admin.Address.Id);
            command.Parameters.AddWithValue("@Mail", admin.Mail);
            command.Parameters.AddWithValue("@Phone", admin.Phone);
            command.Parameters.AddWithValue("@valid", admin.Valid);
            command.Parameters.AddWithValue("@Password", admin.Password);
            command.Parameters.AddWithValue("@Type", admin.AdminType);
            command.Parameters.AddWithValue("@Login", admin.Login);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public static bool UpdateAdmin(Admin admin)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();

            SqlCommand command = connection.CreateCommand(updateAdmin);

            command.Parameters.AddWithValue("@GUID", admin.Guid);
            command.Parameters.AddWithValue("@Name", admin.Name);
            command.Parameters.AddWithValue("@Surname", admin.SurName);
            command.Parameters.AddWithValue("@AddressId", admin.Address.Id);
            command.Parameters.AddWithValue("@Mail", admin.Mail);
            command.Parameters.AddWithValue("@Phone", admin.Phone);
            command.Parameters.AddWithValue("@AdminType", admin.AdminType);
            command.Parameters.AddWithValue("@Login", admin.Login);

            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public static bool ChangePassword(Admin admin)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();
            SqlCommand command = connection.CreateCommand(changePassword);

            command.Parameters.AddWithValue("@GUID", admin.Guid);
            command.Parameters.AddWithValue("@Password", admin.Password);
            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

        public static bool ChangePassword(Official official)
        {
            DBConnection connection = new DBConnection();
            connection.OpenConection();
            SqlCommand command = connection.CreateCommand(changePassword);

            command.Parameters.AddWithValue("@GUID", official.Guid);
            command.Parameters.AddWithValue("@Password", official.Password);
            int result = command.ExecuteNonQuery();
            connection.CloseConnection();
            if (result == 1) return true;
            return false;
        }

    }
}
