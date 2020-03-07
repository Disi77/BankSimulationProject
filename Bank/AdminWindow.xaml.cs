using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Bank.Objects;
using Bank.ORM;
using Bank.Types;

namespace Bank
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        //private int countLinq;
        private Admin activeAdmin;
        private Admin tempAdmin;
         
        private bool currentPasswordCheck = false;
        private string process = "";
        private List<Control> changePasswordControlsList = new List<Control>();
        private List<Control> userControlsList = new List<Control>();
        private List<Control> addressControlsList = new List<Control>();

        public AdminWindow(Admin admin)
        {
            InitializeComponent();
            activeAdmin = admin;
            tempAdmin = new Admin();
            CreateControlsLists();
            SetDefaultSettings();
        }
        private void CreateControlsLists()
        {
            changePasswordControlsList.Add(CurrentPassword);
            changePasswordControlsList.Add(NewPassword1);
            changePasswordControlsList.Add(NewPassword2);
            changePasswordControlsList.Add(CurrentPasswordLabel);
            changePasswordControlsList.Add(NewPasswordLabel);

            userControlsList.Add(NameLabel);
            userControlsList.Add(NameTextBox);
            userControlsList.Add(SurnameLabel);
            userControlsList.Add(SurnameTextBox);
            userControlsList.Add(EmailLabel);
            userControlsList.Add(EmailTextBox);
            userControlsList.Add(PhoneLabel);
            userControlsList.Add(PhoneTextBox);
            userControlsList.Add(ValidLabel);
            userControlsList.Add(ValidTextBox);
            userControlsList.Add(UserTypeLabel);
            userControlsList.Add(UserSubTypeLabel);
            userControlsList.Add(LoginLabel);
            userControlsList.Add(LoginTextBox);
            userControlsList.Add(UserSubTypeComboBox);
            userControlsList.Add(UserTypeComboBox);

            addressControlsList.Add(AddressLabel);
            addressControlsList.Add(StreetLabel);
            addressControlsList.Add(StreetTextBox);
            addressControlsList.Add(StreetNumberLabel);
            addressControlsList.Add(StreetNumberTextBox);
            addressControlsList.Add(CityLabel);
            addressControlsList.Add(CityTextBox);
            addressControlsList.Add(PostalCodeLabel);
            addressControlsList.Add(PostalCodeTextBox);
            addressControlsList.Add(CountryLabel);
            addressControlsList.Add(CountryTextBox);
        }

        /// <summary>
        /// Set Visibility of all text boxes and labels on page to hidden
        /// </summary>
        private void SetDefaultSettings()
        {
            tempAdmin = new Admin();

            // Fields for password change
            foreach (Control c in changePasswordControlsList)
            {
                c.Visibility = Visibility.Hidden;
                c.IsEnabled = true;
            }
            CurrentPassword.Text = "Enter current password";
            NewPassword1.Text = "Enter new password";
            NewPassword2.Text = "Enter new password again";

            // Confirm and Storno Button
            ConfirmButton.Visibility = Visibility.Hidden;
            ConfirmButton.IsEnabled = true;
            StornoButton.Visibility = Visibility.Hidden;
            StornoButton.IsEnabled = true;

            // Fields for Add, Update, Read Admin      
            foreach (Control c in userControlsList)
            {
                c.Visibility = Visibility.Hidden;
                c.IsEnabled = true;
            }
            NameTextBox.Text = "";
            SurnameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneTextBox.Text = "";
            ValidTextBox.Text = "Yes";
            LoginTextBox.Text = "";
            UserSubTypeComboBox.SelectedItem = UserSubTypeComboBox_Admin;
            UserTypeComboBox.SelectedItem = UserTypeComboBox_Admin;

            foreach (Control c in addressControlsList)
            {
                c.Visibility = Visibility.Hidden;
                c.IsEnabled = true;
            }
            StreetTextBox.Text = "";
            StreetNumberTextBox.Text = "";
            CityTextBox.Text = "";
            PostalCodeTextBox.Text = "";
            CountryTextBox.Text = "";

            // All Users view
            AllUsersListView.Visibility = Visibility.Hidden;
            AllUsersListView.IsEnabled = true;
            ViewDetails.Visibility = Visibility.Hidden;
            ViewDetails.IsEnabled = true;
            EditModeButton.Visibility = Visibility.Hidden;
            EditModeButton.IsEnabled = true;

            UpdateUserButton.Visibility = Visibility.Hidden;
            UpdateUserButton.IsEnabled = true;

        }

        private void CurrentPasswordOnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CurrentPassword.IsEnabled = false;
                CurrentPasswordLabel.Visibility = Visibility.Visible;
                if (CurrentPassword.Text == activeAdmin.Password)
                {
                    currentPasswordCheck = true;
                    CurrentPasswordLabel.Content = "Password correct";
                    NewPassword1.IsEnabled = true;
                    NewPassword2.IsEnabled = true;
                }
                else
                {
                    CurrentPassword.IsEnabled = true;
                    CurrentPassword.Text = "";
                    CurrentPasswordLabel.Content = "Password INCORRECT";
                }
            }
        }

        private void CurrentPasswordOnMouseClick(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Text = "";
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            switch (process)
            {
                case "Password change":
                    if (currentPasswordCheck)
                    {
                        if (NewPassword1.Text == NewPassword2.Text && !String.IsNullOrEmpty(NewPassword1.Text))
                        {
                            //Validace hesla
                            if (User.minimalniDelkaHesla > NewPassword1.Text.Length)
                            {
                                MessageBox.Show(String.Format("Password too short. Minimal lenght is {0} characters", User.minimalniDelkaHesla));
                                return;
                            }

                            activeAdmin.Password = NewPassword1.Text;
                            bool result = UsersORM.ChangePassword(activeAdmin);
                            if (result)
                            {
                                MessageBox.Show("Password was changed.");
                                SetDefaultSettings();
                            }

                            else
                                MessageBox.Show("Sometring wrong. Error num: 45679");
                        }
                        else
                        {
                            MessageBox.Show("Enter twice same new password.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sometring wrong. Error num: 45700");
                    }
                    process = "";
                    break;
                case "Add new admin":
                    Admin newAdmin = new Admin();
                    Address newAddress = new Address();

                    if (!allAddressFieldsAreNotEmpty())
                    {
                        MessageBox.Show("Some address field is empty.");
                        return;
                    }

                    newAddress.Id = UsersORM.GetNewAddressId();
                    newAddress.Street = StreetTextBox.Text;
                    newAddress.StreetNumber = StreetNumberTextBox.Text;
                    newAddress.City = CityTextBox.Text;
                    newAddress.PostalCode = PostalCodeTextBox.Text;
                    newAddress.Country = CountryTextBox.Text;

                    bool addressIsCreated = UsersORM.CreateAddress(newAddress);
                    if (!addressIsCreated)
                    {
                        MessageBox.Show("Sometring wrong. Error num: 45681");
                        return;
                    }

                    newAdmin.Guid = Guid.NewGuid();
                    newAdmin.Name = NameTextBox.Text;
                    newAdmin.SurName = SurnameTextBox.Text;
                    newAdmin.Address = newAddress;
                    newAdmin.Mail = EmailTextBox.Text;
                    newAdmin.Phone = PhoneTextBox.Text;
                    newAdmin.Valid = true;
                    newAdmin.Password = "heslo";
                    newAdmin.Login = LoginTextBox.Text;

                    if (UserSubTypeComboBox.SelectedItem == UserSubTypeComboBox_Superadmin)
                        newAdmin.AdminType = AdminType.SuperAdmin;

                    else if (UserSubTypeComboBox.SelectedItem == UserSubTypeComboBox_Admin)
                        newAdmin.AdminType = AdminType.Admin;

                    if (!allUserFieldsAreNotEmpty())
                    {
                        MessageBox.Show("Musíš vyplnit všechna pole");
                        return;
                    }

                    bool userIsCreated = UsersORM.CreateAdmin(newAdmin);
                    if (userIsCreated)
                    {
                        MessageBox.Show(String.Format("New User was created: {0} {1}", newAdmin.Name, newAdmin.SurName));
                        OpenDetailViewOfUser(newAdmin);
                    }
                    else
                    {
                        MessageBox.Show("Sometring wrong. Error num: 45678");
                    }
                    process = "";
                    break;
                case "":
                    MessageBox.Show("Sometring wrong. Error num: 45680");
                    break;
            }
        }

        private void StornoButton_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            process = "";
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            process = "Password change";
            CurrentPassword.Visibility = Visibility.Visible;
            CurrentPassword.Text = "Enter current password";
            CurrentPassword.IsEnabled = true;

            NewPassword1.Visibility = Visibility.Visible;
            NewPassword1.IsEnabled = false;
            NewPassword1.Text = "Enter new password";

            NewPassword2.Visibility = Visibility.Visible;
            NewPassword2.IsEnabled = false;
            NewPassword2.Text = "Enter new password again";

            ConfirmButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;

            CurrentPasswordLabel.Content = "";
            NewPasswordLabel.Content = "";

        }

        private void CreateNewAdmin_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            process = "Add new admin";
            foreach (Control c in userControlsList)
                c.Visibility = Visibility.Visible;
            foreach (Control c in addressControlsList)
                c.Visibility = Visibility.Visible;

            ConfirmButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;

            ValidTextBox.Text = "Yes";
            ValidTextBox.IsEnabled = false;

            UserTypeComboBox.Text = "Admin";
            UserTypeComboBox.IsEnabled = false;

            CountryTextBox.Text = "CZE";
            CountryTextBox.IsEnabled = false;

            switch (activeAdmin.AdminType)
            {
                case AdminType.Admin:
                    UserSubTypeComboBox_Superadmin.Visibility = Visibility.Hidden;
                    UserSubTypeComboBox.IsEnabled = false;
                    break;
                case AdminType.SuperAdmin:
                    break;
            }

        }

        private bool allAddressFieldsAreNotEmpty()
        {
            if (String.IsNullOrEmpty(StreetTextBox.Text))
                return false;
            if(String.IsNullOrEmpty(StreetNumberTextBox.Text))
                return false;
            if (String.IsNullOrEmpty(CityTextBox.Text))
                return false;
            if (String.IsNullOrEmpty(PostalCodeTextBox.Text))
                return false;
            if (String.IsNullOrEmpty(CountryTextBox.Text))
                return false;
            return true;
        }

        private bool allUserFieldsAreNotEmpty()
        {
            if (String.IsNullOrEmpty(NameTextBox.Text))
                return false;
            if (String.IsNullOrEmpty(SurnameTextBox.Text))
                return false;
            if (String.IsNullOrEmpty(EmailTextBox.Text))
                return false;
            if (String.IsNullOrEmpty(PhoneTextBox.Text))
                return false;
            if (String.IsNullOrEmpty(LoginTextBox.Text))
                return false;

            return true;

        }

        private void SelectAllAdmins_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            List<Admin> admins = UsersORM.GetAdmins();
            AllUsersListView.ItemsSource = UsersORM.GetAdmins();

            AllUsersListView.Visibility = Visibility.Visible;
            ViewDetails.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;


            //int count = 0;
            //foreach (Admin a in admins)
            //{
            //    if (a.AdminType == AdminType.Admin)
            //        count++;
            //}
            ////MessageBox.Show(String.Format("Count of admins in DB = {0}", count));

            //countLinq = admins.Where(X => X.AdminType == AdminType.SuperAdmin).Count();

            //countLinq = admins.Where(A => A.AdminType == AdminType.SuperAdmin).Count();
            //countLinq = admins.Count(A => A.AdminType == AdminType.SuperAdmin);

            //int countName = 0;
            //countName = admins.Count(Y => Y.Name == "Petra" && Y.SurName == "Cihalova");
            //MessageBox.Show(String.Format("Count of Petra Cihalova = {0}", countName));
        }

        private void ClickOnButtonViewDetails(object sender, RoutedEventArgs e)
        {
            try
            {
                Admin admin = (Admin)AllUsersListView.SelectedItems[0];
                OpenDetailViewOfUser(admin);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select some user.");            
            }
        }

        private void OpenDetailViewOfUser(Admin admin)
        {
            SetDefaultSettings();
            foreach (Control c in userControlsList)
            {
                c.Visibility = Visibility.Visible;
                c.IsEnabled = false;
            }

            foreach (Control c in addressControlsList)
            {
                c.Visibility = Visibility.Visible;
                c.IsEnabled = false;
            }

            EditModeButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;

            tempAdmin = UsersORM.GetAdminByGuid(admin);

            NameTextBox.Text = admin.Name;
            SurnameTextBox.Text = admin.SurName;
            PhoneTextBox.Text = admin.Phone;
            EmailTextBox.Text = admin.Mail;

            Address address = UsersORM.SelectAddressById(admin.Address.Id);
            admin.Address = address;

            StreetTextBox.Text = admin.Address.Street;
            StreetNumberTextBox.Text = admin.Address.StreetNumber;
            CityTextBox.Text = admin.Address.City;
            PostalCodeTextBox.Text = admin.Address.PostalCode;
            CountryTextBox.Text = admin.Address.Country;
            LoginTextBox.Text = admin.Login;
            UserTypeComboBox.Text = "Admin";

            switch (admin.AdminType)
            {
                case AdminType.Admin:
                    UserSubTypeComboBox.SelectedItem = UserSubTypeComboBox_Admin;
                    break;
                case AdminType.SuperAdmin:
                    UserSubTypeComboBox.SelectedItem = UserSubTypeComboBox_Superadmin;
                    break;            
            }

            switch (admin.Valid)
            {
                case true:
                    ValidTextBox.Text = "Yes";
                    break;
                case false:
                    ValidTextBox.Text = "No";
                    break;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UserEditModeButton(object sender, RoutedEventArgs e)
        {                       
            foreach (Control c in userControlsList)
            {
                c.IsEnabled = true;
            }
            ValidTextBox.IsEnabled = false;
            foreach (Control c in addressControlsList)
            {
                c.IsEnabled = true;
            }
            CountryTextBox.IsEnabled = false;

            EditModeButton.Visibility = Visibility.Hidden;
            UpdateUserButton.Visibility = Visibility.Visible;
        }

        private void UpdateUserInDatabaseButton(object sender, RoutedEventArgs e)
        {
            Admin updatedAdmin = new Admin();
            
            updatedAdmin.Guid = tempAdmin.Guid;
            updatedAdmin.Name = NameTextBox.Text;
            updatedAdmin.SurName = SurnameTextBox.Text;
            updatedAdmin.Mail = EmailTextBox.Text;
            updatedAdmin.Phone = PhoneTextBox.Text;
            updatedAdmin.Login = LoginTextBox.Text;

            if (tempAdmin.Address.Street == StreetTextBox.Text &&
                tempAdmin.Address.StreetNumber == StreetNumberTextBox.Text &&
                tempAdmin.Address.City == CityTextBox.Text &&
                tempAdmin.Address.PostalCode == PostalCodeTextBox.Text &&
                tempAdmin.Address.Country == CountryTextBox.Text)
            {
                updatedAdmin.Address = new Address();
                updatedAdmin.Address.Id = tempAdmin.Address.Id;
            }
            else
            {
                MessageBox.Show("Nová Adresa, zatím neumím změnit.");
                return;            
            }       

            if (UserSubTypeComboBox.SelectedItem == UserSubTypeComboBox_Superadmin)
                updatedAdmin.AdminType = AdminType.SuperAdmin;

            else if (UserSubTypeComboBox.SelectedItem == UserSubTypeComboBox_Admin)
                updatedAdmin.AdminType = AdminType.Admin;

            if (!allUserFieldsAreNotEmpty())
            {
                MessageBox.Show("Musíš vyplnit všechna pole");
                return;
            }

            bool result = UsersORM.UpdateAdmin(updatedAdmin);
            if (result)
                MessageBox.Show("Update of user successful.");
            else
                MessageBox.Show("Update of user NOTsuccessful.");

        }
    }
}
