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
                        MessageBox.Show("Some fields are empty.");
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

        /// <summary>
        /// Origin User and Origin Address is saved in tempAdmin
        /// From TextBoxes I get new User and New Address
        /// Then I check if data of User or Address was changed => Origin data != New data
        /// There can be several scenarios:
        ///         1. Origin User == New User     Origin Address == New Address
        ///                    = no need to update Database
        ///         2. Origin User == New User     Origin Address != New Address
        ///                    2.1 This new Address is new in DB
        ///                                = add new Address in DB
        ///                   2.2 This new Address exists in DB
        ///                                = find in DB this Address
        ///                    = update User in DB (use correct Address ID)
        ///          3. Origin User != New User     Origin Address == New Address
        ///                    = update User in DB with origin Address ID
        ///                    = no need to create new Address in DB
        ///          4. Origin User != New User     Origin Address != New Address
        ///                    4.1 This new Address is new in DB
        ///                                = add new Address in DB
        ///                    4.2 This new Address exists in DB
        ///                                = find in DB this Address
        ///                    = update User in DB (use correct Address ID)
        /// Because in 2. and 4. there is need to update User in DB (at least Address DB), I merge this two scenarious to one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateUserInDatabaseButton(object sender, RoutedEventArgs e)
        {
            if (!allUserFieldsAreNotEmpty())
            {
                MessageBox.Show("Musíš vyplnit všechna pole");
                return;
            }

            Admin updatedAdmin = new Admin();
            Address updatedAddress = new Address();
            

            bool userWasChanged = false;
            bool addressWasChanged = false;
            bool addressExistsInDb = false;

            updatedAdmin.Guid = tempAdmin.Guid;
            updatedAdmin.Name = NameTextBox.Text;
            updatedAdmin.SurName = SurnameTextBox.Text;
            updatedAdmin.Mail = EmailTextBox.Text;
            updatedAdmin.Phone = PhoneTextBox.Text;
            updatedAdmin.Login = LoginTextBox.Text;
            if (UserSubTypeComboBox.SelectedItem == UserSubTypeComboBox_Superadmin)
                updatedAdmin.AdminType = AdminType.SuperAdmin;
            else if (UserSubTypeComboBox.SelectedItem == UserSubTypeComboBox_Admin)
                updatedAdmin.AdminType = AdminType.Admin;
            updatedAdmin.Address = tempAdmin.Address;

            updatedAddress.Id = tempAdmin.Address.Id;
            updatedAddress.Street = StreetTextBox.Text;
            updatedAddress.StreetNumber = StreetNumberTextBox.Text;
            updatedAddress.City = CityTextBox.Text;
            updatedAddress.PostalCode = PostalCodeTextBox.Text;
            updatedAddress.Country = CountryTextBox.Text;
            Address addressFromDb = UsersORM.IsAddressInDatabase(updatedAddress);

            if (tempAdmin.Name == updatedAdmin.Name &&
                tempAdmin.SurName == updatedAdmin.SurName &&
                tempAdmin.Phone == updatedAdmin.Phone &&
                tempAdmin.Mail == updatedAdmin.Mail &&
                tempAdmin.Login == updatedAdmin.Login &&
                tempAdmin.AdminType == updatedAdmin.AdminType)
            {
                userWasChanged = false;
            }
            else
            {
                userWasChanged = true;
            }

            if (updatedAddress.Street == tempAdmin.Address.Street &&
                updatedAddress.StreetNumber == tempAdmin.Address.StreetNumber &&
                updatedAddress.City == tempAdmin.Address.City &&
                updatedAddress.PostalCode == tempAdmin.Address.PostalCode &&
                updatedAddress.Country == tempAdmin.Address.Country)
            {
                addressWasChanged = false;
            }
            else
            {
                addressWasChanged = true;
                if (addressFromDb is null)
                {
                    addressExistsInDb = false;
                }
                else
                {
                    addressExistsInDb = true;
                }
            }

            if (!userWasChanged && !addressWasChanged)
            {
                MessageBox.Show("Nic ke změně");
                return;            
            }

            if (addressWasChanged)
            {
                if (addressExistsInDb)
                {
                    updatedAdmin.Address = addressFromDb;
                    MessageBox.Show("Adresa existuje v DB, přiřazuji její ID userovi");
                }
                else if (!addressExistsInDb)
                {

                    updatedAddress.Id = UsersORM.GetNewAddressId();
                    MessageBox.Show("Žádám si o nové ID, adresa neexistuje v DB");
                    bool addressIsCreated = UsersORM.CreateAddress(updatedAddress);
                    MessageBox.Show("Vytvářím novou adresu v DB");
                    if (addressIsCreated)
                    {
                        updatedAdmin.Address = updatedAddress;
                    }
                    else
                    {
                        MessageBox.Show("We have troubles with new Address creating.");
                        return;
                    }

                }
            }      

            bool result = UsersORM.UpdateAdmin(updatedAdmin);
            if (result)
            {
                MessageBox.Show("Update of user successful.");
                tempAdmin = updatedAdmin;
            }
                
            else
                MessageBox.Show("Update of user NOTsuccessful.");

        }
    }
}
