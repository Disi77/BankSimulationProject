using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Bank.Logger;
using Bank.Objects;
using Bank.ORM;
using Bank.Validator;
using MahApps.Metro.Controls;

namespace Bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static Admin admin;
        public Official Official { get; set; }

        public Log log;
        public MainWindow()
        {
            InitializeComponent();
            log = new Log();
            log.Info("Reservation system app started");
            Login.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.Validator.CredentialsEmpty(Login.Text, PasswordBox.Password))
            {
                MessageBox.Show("Login or password missing.");
                return;
            }

            if (Validator.Validator.StringAllLetter(Login.Text))
            {
                AdminLogin();
            }

            else if (Validator.Validator.StringAllDigit(Login.Text))
            {
                if (Validator.Validator.IsCompanyNumber(Login.Text))
                {
                    OfficialLogin();
                }    
                else if (Validator.Validator.IsSSN(Login.Text))
                {
                    CustomerLogin();       
                }            
            }               
            else
            {
                MessageBox.Show("Invalid login and password. Try Again.");
            }    
        }

        private void OfficialLogin()
        {
            Official = UsersORM.GetOfficialById(Login.Text);
            if (Official != null)
            {
                if (Official.Password == PasswordBox.Password)
                {
                    OfficialWindow officialWindow = new OfficialWindow(Official);
                    log.Info(string.Format("Official login: {0} {1} {2}", Official.Name, Official.SurName, Official.Guid));
                    officialWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Incorrect password.");
                }
            }
        }

        private void AdminLogin()
        {
            admin = UsersORM.GetAdmin(Login.Text, PasswordBox.Password);
            if (admin != null)
            {
                AdminWindow adminWindow = new AdminWindow(admin);
                log.Info(string.Format("Admin login: {0} {1} {2}", admin.Name, admin.SurName, admin.Guid));
                adminWindow.Show();
                Close();
            }
        }

        private void CustomerLogin()
        {
            MessageBox.Show("Customer");
        }

        private void PasswordBoxOnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
            if (PasswordBox.Password == "Password ...")
            {
                PasswordBox.Password = "";
                PasswordBox.Foreground = Brushes.Black;
            }
        }

        private void LoginKeyDown_Handler(object sender, KeyEventArgs e)
        {
            if (Login.Text == "User name ...")
            {
                Login.Text = "";
                Login.Foreground = Brushes.Black;
                Login.FontStyle = FontStyles.Normal;
            }
        }
    }
}
