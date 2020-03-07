using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bank.Objects;
using Bank.ORM;
using Bank.Types;

namespace Bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Admin admin;
        public Official Official { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            if (!Login.Text.All(Char.IsDigit))
            {
                admin = UsersORM.GetAdmin(Login.Text, Password.Text); 
                if (admin != null)
                {
                    AdminWindow adminWindow = new AdminWindow(admin);
                    adminWindow.Show();
                    Close();
                }
            }
            // velice důležité je testování, že objekt není null
            //System.NullReferenceException: 'Object reference not set to an instance of an object.' 

            else
            {
                Official = UsersORM.GetOfficialById(Official);
                if (Official != null)
                {
                    OfficialWindow officialWindow = new OfficialWindow();
                    officialWindow.Show();
                    Close();
                
                }


            
            }
                


            

            //Official official = UsersORM.GetOfficial(Login.Text, Password.Text);

            //if (official != null)
            //{
            //    AdminWindow adminWindow = new AdminWindow();
            //    adminWindow.Show();
            //    Close();

            //}


        }
    }
}
