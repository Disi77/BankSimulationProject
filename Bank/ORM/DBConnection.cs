using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace Bank.ORM
{
    public class DBConnection
    {
        static string connectionString = @"Server=localhost\SQLEXPRESS;Database=Bank;Trusted_Connection=True;";
        private SqlConnection Connection { get; set; }

        public DBConnection()
        {
            Connection = new SqlConnection();
        }
                                            
        public bool OpenConection()
        {
            if (Connection.State != ConnectionState.Open)
                try
                {
                    Connection.ConnectionString = connectionString;
                    Connection.Open();
                    //MessageBox.Show("Connection Open ! ");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBoxResult result = MessageBox.Show("Chyba řpipojení do DB, kontaktujte správce systému", "Connection problem", MessageBoxButton.YesNoCancel, MessageBoxImage.Stop);
                    switch (result)
                    {
                        case MessageBoxResult.Cancel:
                            MessageBox.Show("Bylo stisknuto Cancel");
                            break;

                        case MessageBoxResult.No:
                            MessageBox.Show("Bylo stisknuto No");
                            break;

                        case MessageBoxResult.Yes:
                            MessageBox.Show("Bylo stisknuto Yes");
                            break;
                    }
                    // logování = zachycení výjimky a zapsání do souboru, jsou na to extra třídy
                    // např. se dá použít 
                    //ex.Message je string a to si pošlu do souboru
                    // vytvořím si třídu Logger a ošetřit to všude v programu, aby prostě program nespadl, když je tam chyba
                    // NVC model? vlákna? 
                    return false;
                }
            //MessageBox.Show("Connection is already oppened ! ");
            return false;
        }

        public SqlCommand CreateCommand(string strCommand)
        {
            SqlCommand command = new SqlCommand(strCommand, Connection);
            return command;
        }

        public void CloseConnection()
        {
            Connection.Close();
        }
    }
}
