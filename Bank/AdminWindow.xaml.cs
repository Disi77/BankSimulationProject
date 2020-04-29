using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Bank.Logger;
using Bank.Objects;
using Bank.ORM;
using Bank.Types;
using MahApps.Metro.Controls;

namespace Bank
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : MetroWindow
    {
        private Admin activeAdmin;
        private Admin tempAdmin;
        private Official tempOfficial;
         
        private bool currentPasswordCheck = false;
        private string process = "";
        private List<Control> changePasswordControlsList = new List<Control>();
        private List<Control> userControlsList = new List<Control>();
        private List<Control> addressControlsList = new List<Control>();
        private Log log = new Log();

        public AdminWindow(Admin admin)
        {
            InitializeComponent();
            activeAdmin = admin;
            tempAdmin = new Admin();
            tempOfficial = new Official();
            CreateControlsLists();
            SetDefaultSettings();
        }
        // Logout
        private void Logout_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            log.Info(string.Format("Admin logout: {0} {1} {2}", activeAdmin.Name, activeAdmin.SurName, activeAdmin.Guid));
            mainWindow.Show();
            Close();
        }


        // Default page settings
        private void CreateControlsLists()
        {
            changePasswordControlsList.Add(MainPageLabel); 
            changePasswordControlsList.Add(CurrentPassword);
            changePasswordControlsList.Add(NewPassword1);
            changePasswordControlsList.Add(NewPassword2);

            userControlsList.Add(MainPageLabel);
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
            userControlsList.Add(AdminSubTypeComboBox);
            userControlsList.Add(OfficialSubTypeComboBox);
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
            tempOfficial = new Official();

            // Fields for password change
            foreach (Control c in changePasswordControlsList)
            {
                c.Visibility = Visibility.Hidden;
                c.IsEnabled = true;
            }
            CurrentPassword.Text = "Current password ...";
            var bc = new BrushConverter();
            CurrentPassword.Foreground = (Brush)bc.ConvertFrom("#CC119EDA");
            CurrentPassword.FontStyle = FontStyles.Italic;

            NewPassword1.Text = "New password ...";            
            NewPassword1.Foreground = (Brush)bc.ConvertFrom("#CC119EDA");
            NewPassword1.FontStyle = FontStyles.Italic;

            NewPassword2.Text = "New password ...";
            NewPassword2.Foreground = (Brush)bc.ConvertFrom("#CC119EDA");
            NewPassword2.FontStyle = FontStyles.Italic;

            CurrentPassword_NoIcon.Visibility = Visibility.Hidden;
            CurrentPassword_YesIcon.Visibility = Visibility.Hidden;
            NewPassword1_NoIcon.Visibility = Visibility.Hidden;
            NewPassword1_YesIcon.Visibility = Visibility.Hidden;
            NewPassword2_NoIcon.Visibility = Visibility.Hidden;
            NewPassword2_YesIcon.Visibility = Visibility.Hidden;

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
            EmailTextBox.Text = "@";
            PhoneTextBox.Text = "+420";
            ValidTextBox.Text = "Yes";
            LoginTextBox.Text = "";
            AdminSubTypeComboBox.SelectedItem = AdminSubTypeComboBox_Admin;
            UserTypeComboBox.SelectedItem = UserTypeComboBox_Admin;
            OfficialSubTypeComboBox.SelectedItem = OfficialSubTypeComboBox_Junior;
            NameTextBox_NoIcon.Visibility = Visibility.Hidden;
            SurnameTextBox_NoIcon.Visibility = Visibility.Hidden;
            EmailTextBox_NoIcon.Visibility = Visibility.Hidden;
            PhoneTextBox_NoIcon.Visibility = Visibility.Hidden;
            StreetTextBox_NoIcon.Visibility = Visibility.Hidden;
            StreetNumberTextBox_NoIcon.Visibility = Visibility.Hidden;
            CityTextBox_NoIcon.Visibility = Visibility.Hidden;
            PostalCodeTextBox_NoIcon.Visibility = Visibility.Hidden;
            LoginTextBox_NoIcon.Visibility = Visibility.Hidden;

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
            AllAdminsListView.Visibility = Visibility.Hidden;
            AllAdminsListView.IsEnabled = true;
            AllOfficialsListView.Visibility = Visibility.Hidden;
            AllOfficialsListView.IsEnabled = true;
            ViewDetails.Visibility = Visibility.Hidden;
            ViewDetails.IsEnabled = true;
            EditModeButton.Visibility = Visibility.Hidden;
            EditModeButton.IsEnabled = true;

            UpdateUserButton.Visibility = Visibility.Hidden;
            UpdateUserButton.IsEnabled = true;

            // Search 
            SearchTextBox.Visibility = Visibility.Hidden;
            SearchTextBox.Text = "Enter user name ...";
            MainPageLabel.Visibility = Visibility.Hidden;
            AllAdminsListView.ItemsSource = null;
            AllOfficialsListView.ItemsSource = null;

        }


        // Pasword change
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            
            process = "Password change";

            MainPageLabel.Visibility = Visibility.Visible;
            MainPageLabel.Content = "PASSWORD CHANGE:";

            CurrentPassword.Visibility = Visibility.Visible;
            CurrentPassword.Text = "Current password ...";
            CurrentPassword.IsEnabled = true;

            NewPassword1.Visibility = Visibility.Visible;
            NewPassword1.IsEnabled = false;
            NewPassword1.Text = "New password ...";

            NewPassword2.Visibility = Visibility.Visible;
            NewPassword2.IsEnabled = false;
            NewPassword2.Text = "New password ...";

            ConfirmButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;

            CurrentPassword.Focus();

        }

        private void CurrentPasswordOnKeyDown_Handler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (CurrentPassword.Text == activeAdmin.Password)
                {
                    CurrentPasswordCorrect();                    
                }
                else
                {
                    CurrentPassword.Text = "";
                    CurrentPassword_YesIcon.Visibility = Visibility.Hidden;
                    CurrentPassword_NoIcon.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (CurrentPassword.Text == "Current password ...")
                {
                    CurrentPassword.Text = "";
                    CurrentPassword.Foreground = Brushes.Black;
                    CurrentPassword.FontStyle = FontStyles.Normal;
                }
            }
        }

        private void CurrentPasswordOnKeyUp_Handler(object sender, KeyEventArgs e)
        {
            if (CurrentPassword.Text == activeAdmin.Password)
            {
                CurrentPasswordCorrect();
            }
            else
            {
                CurrentPassword_NoIcon.Visibility = Visibility.Visible;
            }
        }

        private void NewPasswordOnKeyDown_Handler(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text is "New password ...")
            {
                textbox.Text = "";
                textbox.Foreground = Brushes.Black;
                textbox.FontStyle = FontStyles.Normal;
            }

        }

        private void NewPasswordOnKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            switch (textbox.Name)
            {
                case "NewPassword1":
                    if (textbox.Text.Length >= User.passwordMinLength &&
                        Validator.Validator.IsAlphaNumeric(textbox.Text))
                    {
                        NewPassword1_YesIcon.Visibility = Visibility.Visible;
                        NewPassword1_NoIcon.Visibility = Visibility.Hidden;
                    }
                    else if (textbox.Text.Length >= 1 && textbox.Text != "New password ...")
                    {
                        NewPassword1_YesIcon.Visibility = Visibility.Hidden;
                        NewPassword1_NoIcon.Visibility = Visibility.Visible;
                    }
                    break;
                case "NewPassword2":
                    if (textbox.Text == NewPassword1.Text)
                    {
                        NewPassword2_YesIcon.Visibility = Visibility.Visible;
                        NewPassword2_NoIcon.Visibility = Visibility.Hidden;
                    }
                    else if (textbox.Text.Length >= 1 && textbox.Text != "New password ...")
                    {
                        NewPassword2_YesIcon.Visibility = Visibility.Hidden;
                        NewPassword2_NoIcon.Visibility = Visibility.Visible;
                    }
                    break;
                default:
                    break;
            }
        }

        private void CurrentPasswordCorrect()
        {
            currentPasswordCheck = true;
            NewPassword1.IsEnabled = true;
            NewPassword2.IsEnabled = true;
            CurrentPassword_YesIcon.Visibility = Visibility.Visible;
            CurrentPassword_NoIcon.Visibility = Visibility.Hidden;
        }

        private void PasswordOnMouseClick(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text is "New password ..." || textbox.Text is "Current password ...")
            { 
                textbox.Text = ""; 
            }
        }

        private bool PasswordChangingProcess()
        {
            if (currentPasswordCheck)
            {
                if (NewPassword1.Text == NewPassword2.Text && !String.IsNullOrEmpty(NewPassword1.Text))
                {
                    //Validace hesla
                    if (User.passwordMinLength > NewPassword1.Text.Length)
                    {
                        MessageBox.Show(String.Format("Password too short. Minimal lenght is {0} characters", User.passwordMinLength),
                                         "",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Warning);
                        return false;
                    }

                    activeAdmin.Password = NewPassword1.Text;
                    bool result = UsersORM.ChangePassword(activeAdmin);
                    if (result)
                    {
                        SetDefaultSettings();
                        return true;
                    }
                }
                else if (String.IsNullOrEmpty(NewPassword1.Text) || String.IsNullOrEmpty(NewPassword2.Text))
                {
                    MessageBox.Show("You have to fill in new password.",
                                    "",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Enter twice same new password.",
                                    "",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                }
            }
            else
            {
                process = "";
            }
            return false;
        }


        // Create new user
        private bool CreateNewOfficialProcess()
        {
            Official newOfficial = new Official();
            Address newAddress = new Address();

            if (!allUserFieldsAreNotEmpty())
            {
                MessageBox.Show("Please fill in all user information",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return false;
            }

            if (!allAddressFieldsAreNotEmpty())
            {
                MessageBox.Show("Please fill in all address fields",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return false;
            }

            if (!Validator.Validator.NameValidator(NameTextBox.Text) ||
                !Validator.Validator.NameValidator(SurnameTextBox.Text) ||
                !Validator.Validator.EmailValidator(EmailTextBox.Text) ||
                !Validator.Validator.PhoneValidator(PhoneTextBox.Text) ||
                !Validator.Validator.StreetValidator(StreetTextBox.Text) ||
                !Validator.Validator.StreetNumberValidator(StreetNumberTextBox.Text) ||
                !Validator.Validator.NameValidator(CityTextBox.Text) ||
                !Validator.Validator.PostalCodeValidator(PostalCodeTextBox.Text) ||
                !Validator.Validator.OfficialUserNameValidator(LoginTextBox.Text)
                )
            {
                MessageBox.Show("Incorect inputs. Please check user information.",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                                
                if (!Validator.Validator.EmailValidator(EmailTextBox.Text))
                {
                    EmailTextBox_NoIcon.Visibility = Visibility.Visible;
                }

                if (!Validator.Validator.PhoneValidator(PhoneTextBox.Text))
                {
                    PhoneTextBox_NoIcon.Visibility = Visibility.Visible;
                }

                return false;
            }

            newAddress.Id = UsersORM.GetNewAddressId();
            newAddress.Street = StreetTextBox.Text;
            newAddress.StreetNumber = StreetNumberTextBox.Text;
            newAddress.City = CityTextBox.Text;
            newAddress.PostalCode = PostalCodeTextBox.Text;
            newAddress.Country = CountryTextBox.Text;

            Address addressFromDB = UsersORM.IsAddressInDatabase(newAddress);
            if (addressFromDB is null)
            {
                bool addressIsCreated = UsersORM.CreateAddress(newAddress);
                if (!addressIsCreated)
                {
                    MessageBox.Show("New address was not created. Contact administrator",
                                    "",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                newAddress.Id = addressFromDB.Id;
            }            

            newOfficial.Guid = Guid.NewGuid();
            newOfficial.Name = NameTextBox.Text;
            newOfficial.SurName = SurnameTextBox.Text;
            newOfficial.Address = newAddress;
            newOfficial.Mail = EmailTextBox.Text;
            newOfficial.Phone = PhoneTextBox.Text;
            newOfficial.Valid = true;
            newOfficial.Password = "heslo";
            newOfficial.CompanyNumber = LoginTextBox.Text;

             if (OfficialSubTypeComboBox.SelectedItem == OfficialSubTypeComboBox_Junior)
                newOfficial.OfficialType = OfficialType.Junior;

            else if (OfficialSubTypeComboBox.SelectedItem == OfficialSubTypeComboBox_Normal)
                newOfficial.OfficialType = OfficialType.Normal;

            if (OfficialSubTypeComboBox.SelectedItem == OfficialSubTypeComboBox_Senior)
                newOfficial.OfficialType = OfficialType.Senior;


            bool userIsCreated = UsersORM.CreateNewOfficial(newOfficial);
            if (userIsCreated)
            {
                MessageBox.Show(String.Format("New User was created: {0} {1}", newOfficial.Name, newOfficial.SurName),
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                OpenDetailViewOfUser(newOfficial);
                return true;
            }
            else
            {
                MessageBox.Show("New user was not created. Contact administrator",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            process = "";
            return false;

        }

        private bool CreateNewAdminProcess()
        {
            Admin newAdmin = new Admin();
            Address newAddress = new Address();


            if (!allUserFieldsAreNotEmpty())
            {
                MessageBox.Show("Please fill in all user information",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return false;
            }

            if (!allAddressFieldsAreNotEmpty())
            {
                MessageBox.Show("Please fill in all address fields",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return false;
            }

            if (!Validator.Validator.NameValidator(NameTextBox.Text) ||
                !Validator.Validator.NameValidator(SurnameTextBox.Text) ||
                !Validator.Validator.EmailValidator(EmailTextBox.Text) ||
                !Validator.Validator.PhoneValidator(PhoneTextBox.Text) ||
                !Validator.Validator.StreetValidator(StreetTextBox.Text) ||
                !Validator.Validator.StreetNumberValidator(StreetNumberTextBox.Text) ||
                !Validator.Validator.NameValidator(CityTextBox.Text) ||
                !Validator.Validator.PostalCodeValidator(PostalCodeTextBox.Text) ||
                !Validator.Validator.StringAllLetter(LoginTextBox.Text)
                )
            {
                MessageBox.Show("Incorect inputs. Please check user information.",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);

                if (!Validator.Validator.EmailValidator(EmailTextBox.Text))
                {
                    EmailTextBox_NoIcon.Visibility = Visibility.Visible;
                }

                if (!Validator.Validator.PhoneValidator(PhoneTextBox.Text))
                {
                    PhoneTextBox_NoIcon.Visibility = Visibility.Visible;
                }

                return false;
            }

            newAddress.Id = UsersORM.GetNewAddressId();
            newAddress.Street = StreetTextBox.Text;
            newAddress.StreetNumber = StreetNumberTextBox.Text;
            newAddress.City = CityTextBox.Text;
            newAddress.PostalCode = PostalCodeTextBox.Text;
            newAddress.Country = CountryTextBox.Text;

            Address addressFromDB = UsersORM.IsAddressInDatabase(newAddress);
            if (addressFromDB is null)
            {
                bool addressIsCreated = UsersORM.CreateAddress(newAddress);
                if (!addressIsCreated)
                {
                    MessageBox.Show("New address was not created. Contact administrator",
                                    "",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                newAddress.Id = addressFromDB.Id;
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

            if (AdminSubTypeComboBox.SelectedItem == AdminSubTypeComboBox_Superadmin)
                newAdmin.AdminType = AdminType.SuperAdmin;

            else if (AdminSubTypeComboBox.SelectedItem == AdminSubTypeComboBox_Admin)
                newAdmin.AdminType = AdminType.Admin;


            bool userIsCreated = UsersORM.CreateAdmin(newAdmin);
            if (userIsCreated)
            {
                MessageBox.Show(String.Format("New User was created: {0} {1}", newAdmin.Name, newAdmin.SurName),
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                OpenDetailViewOfUser(newAdmin);
                return true;
            }
            else
            {
                MessageBox.Show("New user was not created. Contact administrator",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            process = "";
            return false;

        }

        private void CreateNewAdmin_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            process = "Add new admin";
            MainPageLabel.Content = "CREATE NEW ADMIN:";
            foreach (Control c in userControlsList)
                c.Visibility = Visibility.Visible;
            foreach (Control c in addressControlsList)
                c.Visibility = Visibility.Visible;

            OfficialSubTypeComboBox.Visibility = Visibility.Hidden;

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
                    AdminSubTypeComboBox_Superadmin.Visibility = Visibility.Hidden;
                    AdminSubTypeComboBox.IsEnabled = false;
                    break;
                case AdminType.SuperAdmin:
                    break;
            }
            NameTextBox.Focus();

        }

        private void CreateNewOfficial_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            process = "Add new official";
            MainPageLabel.Content = "CREATE NEW OFFICIAL:";
            foreach (Control c in userControlsList)
                c.Visibility = Visibility.Visible;
            foreach (Control c in addressControlsList)
                c.Visibility = Visibility.Visible;

            AdminSubTypeComboBox.Visibility = Visibility.Hidden;

            ConfirmButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;

            ValidTextBox.Text = "Yes";
            ValidTextBox.IsEnabled = false;

            UserTypeComboBox.Text = "Official";
            UserTypeComboBox.IsEnabled = false;

            CountryTextBox.Text = "CZE";
            CountryTextBox.IsEnabled = false;

            LoginTextBox.IsEnabled = false;
            LoginTextBox.Text = GetNewCompanyNumber();

            NameTextBox.Focus();

        }


        // Confirm Storno button
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            switch (process)
            {
                case "Password change":
                    if (PasswordChangingProcess())
                    {
                        MessageBox.Show("Password was changed.", 
                                        "",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                        process = "";
                    }  
                    break;
                case "Add new admin":
                    if (CreateNewAdminProcess())
                    {
                        process = "";
                    }            
                    break;
                case "Add new official":
                    if (CreateNewOfficialProcess())
                    {
                        process = "";
                    }                        
                    break;
                case "":
                    break;
            }
        }

        private void StornoButton_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            process = "";
        }


        // Select all users
        private void SelectAllAdmins_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            MainPageLabel.Visibility = Visibility.Visible;
            MainPageLabel.Content = "ALL ADMINS:";

            List<Admin> allAdmins = UsersORM.GetAdmins();

            var result = from admin in allAdmins
                         orderby admin.SurName
                         select admin;

            AllAdminsListView.ItemsSource = result;

            AllAdminsListView.Visibility = Visibility.Visible;
            ViewDetails.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;
        }

        private void SelectAllOfficials_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            MainPageLabel.Visibility = Visibility.Visible;
            MainPageLabel.Content = "ALL OFFICIALS:";

            List<Official> allOfficials = UsersORM.GetAllOfficials();

            var result = from official in allOfficials
                         orderby official.SurName
                         select official;

            AllOfficialsListView.ItemsSource = result;

            AllOfficialsListView.Visibility = Visibility.Visible;
            ViewDetails.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;
        }


        // User detail view
        private void ClickOnButtonViewDetails(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AllAdminsListView.Visibility == Visibility.Visible)
                {
                    Admin admin = (Admin)AllAdminsListView.SelectedItems[0];
                    OpenDetailViewOfUser(admin);
                }
                else if (AllOfficialsListView.Visibility == Visibility.Visible)
                {
                    Official official = (Official)AllOfficialsListView.SelectedItems[0];
                    OpenDetailViewOfUser(official);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select some user.",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning
                                );
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
            OfficialSubTypeComboBox.Visibility = Visibility.Hidden;

            foreach (Control c in addressControlsList)
            {
                c.Visibility = Visibility.Visible;
                c.IsEnabled = false;
            }

            MainPageLabel.Content = "USER DETAILS:";
            MainPageLabel.IsEnabled = true;
            EditModeButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;

            tempAdmin = UsersORM.GetAdminByGuid(admin);
            tempOfficial = new Official();

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
                    AdminSubTypeComboBox.SelectedItem = AdminSubTypeComboBox_Admin;
                    break;
                case AdminType.SuperAdmin:
                    AdminSubTypeComboBox.SelectedItem = AdminSubTypeComboBox_Superadmin;
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

        private void OpenDetailViewOfUser(Official official)
        {
            SetDefaultSettings();
            foreach (Control c in userControlsList)
            {
                c.Visibility = Visibility.Visible;
                c.IsEnabled = false;
            }
            AdminSubTypeComboBox.Visibility = Visibility.Hidden;

            foreach (Control c in addressControlsList)
            {
                c.Visibility = Visibility.Visible;
                c.IsEnabled = false;
            }

            MainPageLabel.Content = "USER DETAILS:";
            MainPageLabel.IsEnabled = true;
            EditModeButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;

            tempOfficial = UsersORM.GetOfficialById(official.CompanyNumber);
            tempAdmin = new Admin();

            NameTextBox.Text = official.Name;
            SurnameTextBox.Text = official.SurName;
            PhoneTextBox.Text = official.Phone;
            EmailTextBox.Text = official.Mail;

            Address address = UsersORM.SelectAddressById(official.Address.Id);
            official.Address = address;

            StreetTextBox.Text = official.Address.Street;
            StreetNumberTextBox.Text = official.Address.StreetNumber;
            CityTextBox.Text = official.Address.City;
            PostalCodeTextBox.Text = official.Address.PostalCode;
            CountryTextBox.Text = official.Address.Country;
            LoginTextBox.Text = official.CompanyNumber;
            LoginTextBox.IsEnabled = false;
            UserTypeComboBox.Text = "Official";


            switch (official.OfficialType)
            {
                case OfficialType.Junior:
                    OfficialSubTypeComboBox.SelectedItem = OfficialSubTypeComboBox_Junior;
                    break;
                case OfficialType.Normal:
                    OfficialSubTypeComboBox.SelectedItem = OfficialSubTypeComboBox_Normal;
                    break;
                case OfficialType.Senior:
                    OfficialSubTypeComboBox.SelectedItem = OfficialSubTypeComboBox_Senior;
                    break;
            }

            switch (official.Valid)
            {
                case true:
                    ValidTextBox.Text = "Yes";
                    break;
                case false:
                    ValidTextBox.Text = "No";
                    break;
            }
        }


        // Edit mode, update user
        private void UserEditModeButton(object sender, RoutedEventArgs e)
        {                       
            foreach (Control c in userControlsList)
            {
                c.IsEnabled = true;
            }
            ValidTextBox.IsEnabled = false;
            UserTypeComboBox.IsEnabled = false;

            switch (activeAdmin.AdminType)
            {
                case AdminType.Admin:
                    AdminSubTypeComboBox_Superadmin.Visibility = Visibility.Hidden;
                    AdminSubTypeComboBox.IsEnabled = false;
                    break;
                case AdminType.SuperAdmin:
                    break;
            }

            foreach (Control c in addressControlsList)
            {
                c.IsEnabled = true;
            }
            CountryTextBox.IsEnabled = false;

            MainPageLabel.Content = "USER UPDATE:";
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
        private void UpdateUserInDatabaseButton(object sender, RoutedEventArgs e)
        {
            if (!allUserFieldsAreNotEmpty())
            {
                MessageBox.Show("Musíš vyplnit všechna pole");
                return;
            }

            if (UserTypeComboBox.SelectedItem == UserTypeComboBox_Admin)
            {
                UpdateUserProcess(tempAdmin);
            }
            else if (UserTypeComboBox.SelectedItem == UserTypeComboBox_Official)
            {
                UpdateUserProcess(tempOfficial);
            }
        }

        private void UpdateUserProcess(Official tempOfficial)
        {
            Official updatedOfficial = new Official();
            Address updatedAddress = new Address();

            LoginTextBox.IsEnabled = false;

            bool userWasChanged = false;
            bool addressWasChanged = false;
            bool addressExistsInDb = false;

            updatedOfficial.Guid = tempOfficial.Guid;
            updatedOfficial.Name = NameTextBox.Text;
            updatedOfficial.SurName = SurnameTextBox.Text;
            updatedOfficial.Mail = EmailTextBox.Text;
            updatedOfficial.Phone = PhoneTextBox.Text;
            updatedOfficial.CompanyNumber = LoginTextBox.Text;

            if (OfficialSubTypeComboBox.SelectedItem == OfficialSubTypeComboBox_Junior)
                updatedOfficial.OfficialType = OfficialType.Junior;
            else if (OfficialSubTypeComboBox.SelectedItem == OfficialSubTypeComboBox_Normal)
                updatedOfficial.OfficialType = OfficialType.Normal;
            else if (OfficialSubTypeComboBox.SelectedItem == OfficialSubTypeComboBox_Senior)
                updatedOfficial.OfficialType = OfficialType.Senior;

            updatedOfficial.Address = tempOfficial.Address;

            updatedAddress.Id = tempOfficial.Address.Id;
            updatedAddress.Street = StreetTextBox.Text;
            updatedAddress.StreetNumber = StreetNumberTextBox.Text;
            updatedAddress.City = CityTextBox.Text;
            updatedAddress.PostalCode = PostalCodeTextBox.Text;
            updatedAddress.Country = CountryTextBox.Text;
            Address addressFromDb = UsersORM.IsAddressInDatabase(updatedAddress);

            if (tempOfficial.Name == updatedOfficial.Name &&
                tempOfficial.SurName == updatedOfficial.SurName &&
                tempOfficial.Phone == updatedOfficial.Phone &&
                tempOfficial.Mail == updatedOfficial.Mail &&
                tempOfficial.CompanyNumber == updatedOfficial.CompanyNumber &&
                tempOfficial.OfficialType == updatedOfficial.OfficialType)
            {
                userWasChanged = false;
            }
            else
            {
                userWasChanged = true;
            }

            if (updatedAddress.Street == tempOfficial.Address.Street &&
                updatedAddress.StreetNumber == tempOfficial.Address.StreetNumber &&
                updatedAddress.City == tempOfficial.Address.City &&
                updatedAddress.PostalCode == tempOfficial.Address.PostalCode &&
                updatedAddress.Country == tempOfficial.Address.Country)
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
                return;
            }

            if (addressWasChanged)
            {
                if (addressExistsInDb)
                {
                    updatedOfficial.Address = addressFromDb;
                }
                else if (!addressExistsInDb)
                {

                    updatedAddress.Id = UsersORM.GetNewAddressId();
                    bool addressIsCreated = UsersORM.CreateAddress(updatedAddress);
                    if (addressIsCreated)
                    {
                        updatedOfficial.Address = updatedAddress;
                    }
                    else
                    {
                        MessageBox.Show("We have troubles with new Address creating.");
                        return;
                    }
                }
            }

            bool result = UsersORM.UpdateOfficial(updatedOfficial);
            if (result)
            {
                MessageBox.Show("Update of user successful.");
                tempOfficial = updatedOfficial;
            }

            else
                MessageBox.Show("Update of user NOT successful.");
        }

        private void UpdateUserProcess(Admin tempAdmin)
        {
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
            if (AdminSubTypeComboBox.SelectedItem == AdminSubTypeComboBox_Superadmin)
                updatedAdmin.AdminType = AdminType.SuperAdmin;
            else if (AdminSubTypeComboBox.SelectedItem == AdminSubTypeComboBox_Admin)
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
                return;            
            }

            if (addressWasChanged)
            {
                if (addressExistsInDb)
                {
                    updatedAdmin.Address = addressFromDb;
                }
                else if (!addressExistsInDb)
                {

                    updatedAddress.Id = UsersORM.GetNewAddressId();
                    bool addressIsCreated = UsersORM.CreateAddress(updatedAddress);
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
                if (tempAdmin == activeAdmin)
                    activeAdmin = updatedAdmin;
                tempAdmin = updatedAdmin;
            }
                
            else
                MessageBox.Show("Update of user NOT successful.");
        }

        private void UpdateMyAccount_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            OpenDetailViewOfUser(activeAdmin);
        }

        private string GetNewCompanyNumber()
        {
            List<Official> officialList = UsersORM.GetAllOfficials();
            Random random = new Random();
            string newCompanyNumber;

            while (true)
            {
                newCompanyNumber = random.Next(100, 1000).ToString();
                if (officialList.Where(X => X.CompanyNumber == newCompanyNumber).Count() == 0)
                {
                    break;
                }
            }
            return newCompanyNumber;
        }


        // Search user
        private void SearchAdmin_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            MainPageLabel.Content = "ADMIN SEARCH:";
            SearchTextBox.Text = "Enter user name ...";
            MainPageLabel.Visibility = Visibility.Visible;
            SearchTextBox.Visibility = Visibility.Visible;
            AllAdminsListView.Visibility = Visibility.Visible;
            ViewDetails.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;
            SearchTextBox.Focus();
        }

        private void SearchTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text is "Enter user name ...")
            {
                textbox.Text = "";
            }
        }

        private void SearchTextBox_KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AllAdminsListView.Visibility == Visibility.Visible)
                {
                    List<Admin> admins = UsersORM.GetAdmins();
                    var result = admins.Where(X => X.Name.ToLower().Contains(SearchTextBox.Text.ToLower())
                                            || X.SurName.ToLower().Contains(SearchTextBox.Text.ToLower())
                                            || String.Format("{0} {1}", X.Name, X.SurName).ToLower().Contains(SearchTextBox.Text.ToLower())
                                            || String.Format("{1} {0}", X.Name, X.SurName).ToLower().Contains(SearchTextBox.Text.ToLower())
                                          );
                    AllAdminsListView.ItemsSource = result;
                }
                else if (AllOfficialsListView.Visibility == Visibility.Visible)
                {
                    List<Official> officials = UsersORM.GetAllOfficials();
                    var result2 = officials.Where(X => X.Name.ToLower().Contains(SearchTextBox.Text.ToLower())
                                                || X.SurName.ToLower().Contains(SearchTextBox.Text.ToLower())
                                                || String.Format("{0} {1}", X.Name, X.SurName).ToLower().Contains(SearchTextBox.Text.ToLower())
                                                || String.Format("{1} {0}", X.Name, X.SurName).ToLower().Contains(SearchTextBox.Text.ToLower())
                                              );
                    AllOfficialsListView.ItemsSource = result2;
                }
            }
            else
            {
                if (SearchTextBox.Text == "Enter user name ...")
                {
                    SearchTextBox.Text = "";
                    SearchTextBox.FontSize = 14;
                }
            }
        }

        private void SearchOfficial_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            MainPageLabel.Content = "OFFICIAL SEARCH:";
            MainPageLabel.Visibility = Visibility.Visible;
            SearchTextBox.Visibility = Visibility.Visible;
            AllOfficialsListView.Visibility = Visibility.Visible;
            ViewDetails.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;
        }


        // Validation
        private bool allAddressFieldsAreNotEmpty()
        {
            if (String.IsNullOrEmpty(StreetTextBox.Text))
                return false;
            if (String.IsNullOrEmpty(StreetNumberTextBox.Text))
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

        private void NameTextBoxKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (Validator.Validator.NameValidator(t.Text) || String.IsNullOrEmpty(t.Text))
            {
                NameTextBox_NoIcon.Visibility = Visibility.Hidden;
            }    
            else
            {
                NameTextBox_NoIcon.Visibility = Visibility.Visible;
            }
        }

        private void SurnameTextBoxKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (Validator.Validator.NameValidator(t.Text) || String.IsNullOrEmpty(t.Text))
            {
                SurnameTextBox_NoIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                SurnameTextBox_NoIcon.Visibility = Visibility.Visible;
            }
        }

        private void EmailTextBoxKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (Validator.Validator.EmailValidator(t.Text) || t.Text == "@" || t.Text == "")
            {
                EmailTextBox_NoIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                EmailTextBox_NoIcon.Visibility = Visibility.Visible;
            }
        }

        private void PhoneTextBoxKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (Validator.Validator.PhoneValidator(t.Text) || t.Text == "" || t.Text == "+420")
            {
                PhoneTextBox_NoIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                PhoneTextBox_NoIcon.Visibility = Visibility.Visible;
            }
        }

        private void PhoneTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhoneTextBox.Select(PhoneTextBox.Text.Length, 0);
        }

        private void EmailTextBoxKeyDown_Handler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                PhoneTextBox.Select(PhoneTextBox.Text.Length, 0);
            }
        }

        private void StreetTextBoxKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (Validator.Validator.StreetValidator(t.Text) || String.IsNullOrEmpty(t.Text))
            {
                StreetTextBox_NoIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                StreetTextBox_NoIcon.Visibility = Visibility.Visible;
            }
        }

        private void StreetNumberTextBoxKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (Validator.Validator.StreetNumberValidator(t.Text) || String.IsNullOrEmpty(t.Text))
            {
                StreetNumberTextBox_NoIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                StreetNumberTextBox_NoIcon.Visibility = Visibility.Visible;
            }
        }

        private void CityTextBoxKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (Validator.Validator.NameValidator(t.Text) || String.IsNullOrEmpty(t.Text))
            {
                CityTextBox_NoIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                CityTextBox_NoIcon.Visibility = Visibility.Visible;
            }
        }

        private void PostalCodeTextBoxKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (Validator.Validator.PostalCodeValidator(t.Text) || String.IsNullOrEmpty(t.Text))
            {
                PostalCodeTextBox_NoIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                PostalCodeTextBox_NoIcon.Visibility = Visibility.Visible;
            }
        }

        private void LoginTextBoxKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            string userType = "";

            if (UserTypeComboBox.SelectedItem == UserTypeComboBox_Admin)
                userType = "admin";
            else if (UserTypeComboBox.SelectedItem == UserTypeComboBox_Official)
                userType = "official";

            switch (userType)
            {
                case "admin":
                    if (Validator.Validator.StringAllLetter(t.Text) || String.IsNullOrEmpty(t.Text))
                    {
                        LoginTextBox_NoIcon.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        LoginTextBox_NoIcon.Visibility = Visibility.Visible;
                    }
                    break;
                case "offiial":
                    if (Validator.Validator.OfficialUserNameValidator(t.Text) || String.IsNullOrEmpty(t.Text))
                    {
                        LoginTextBox_NoIcon.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        LoginTextBox_NoIcon.Visibility = Visibility.Visible;
                    }
                    break;
                default:
                    break;
            }

            
        }
    }
}
