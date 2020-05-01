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
using System.Windows.Shapes;
using Bank.Logger;
using Bank.Objects;
using Bank.ORM;
using Bank.Types;
using MahApps.Metro.Controls;

namespace Bank
{
    /// <summary>
    /// Interaction logic for OfficialWindow.xaml
    /// </summary>
    public partial class OfficialWindow : MetroWindow
    {
        private Official activeOfficial;
        private Official tempOfficial;
        private Customer tempCustomer;
        private Log log;
        private List<Control> changePasswordControlsList = new List<Control>();
        private List<Control> userControlsList = new List<Control>();
        private List<Control> addressControlsList = new List<Control>();
        private bool oldPasswordCheck = false;

        public OfficialWindow(Official official)
        {
            InitializeComponent();
            activeOfficial = official;
            log = new Log();
            CreateControlsLists();
            SetDefaultSettings();
            if (activeOfficial.CompanyNumber == "123")
            {
                MenuItem_SecretPage.Visibility = Visibility.Visible;
            }
        }

        private void CreateControlsLists()
        {
            changePasswordControlsList.Add(MainPageLabel);
            changePasswordControlsList.Add(OldPassword);
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
            userControlsList.Add(OfficialSubTypeComboBox);
            userControlsList.Add(CustomerSubTypeComboBox);
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

        private void SetDefaultSettings()
        {
            // Fields for password change
            foreach (Control c in changePasswordControlsList)
            {
                c.Visibility = Visibility.Hidden;
                c.IsEnabled = true;
            }
            OldPassword.Text = "Old password ...";
            var bc = new BrushConverter();
            OldPassword.Foreground = (Brush)bc.ConvertFrom("#CC119EDA");
            OldPassword.FontStyle = FontStyles.Italic;

            NewPassword1.Text = "New password ...";
            NewPassword1.Foreground = (Brush)bc.ConvertFrom("#CC119EDA");
            NewPassword1.FontStyle = FontStyles.Italic;

            NewPassword2.Text = "New password ...";
            NewPassword2.Foreground = (Brush)bc.ConvertFrom("#CC119EDA");
            NewPassword2.FontStyle = FontStyles.Italic;

            OldPassword_NoIcon.Visibility = Visibility.Hidden;
            OldPassword_YesIcon.Visibility = Visibility.Hidden;
            NewPassword1_NoIcon.Visibility = Visibility.Hidden;
            NewPassword1_YesIcon.Visibility = Visibility.Hidden;
            NewPassword2_NoIcon.Visibility = Visibility.Hidden;
            NewPassword2_YesIcon.Visibility = Visibility.Hidden;

            // Confirm and Storno Button
            ConfirmButton.Visibility = Visibility.Hidden;
            ConfirmButton.IsEnabled = true;
            StornoButton.Visibility = Visibility.Hidden;
            StornoButton.IsEnabled = true;

            // Fields for Add, Update 
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
            UserTypeComboBox.SelectedItem = UserTypeComboBox_Official;
            OfficialSubTypeComboBox.SelectedItem = OfficialSubTypeComboBox_Junior;
            CustomerSubTypeComboBox.SelectedItem = CustomerSubTypeComboBox_Person;
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

            EditModeButton.Visibility = Visibility.Hidden;
            EditModeButton.IsEnabled = true;

            UpdateUserButton.Visibility = Visibility.Hidden;
            UpdateUserButton.IsEnabled = true;

            CreateCustomerButton.Visibility = Visibility.Hidden;
            CreateCustomerButton.IsEnabled = true;

            LoginLabel.Content = "Login";


            // All Users view
            AllCustomersListView.Visibility = Visibility.Hidden;
            AllCustomersListView.IsEnabled = true;
            ViewDetails.Visibility = Visibility.Hidden;
            ViewDetails.IsEnabled = true;

            // Search 
            SearchTextBox.Visibility = Visibility.Hidden;
            SearchTextBox.Text = "Enter user name ...";
            AllCustomersListView.ItemsSource = null;
            SearchButton.Visibility = Visibility.Hidden;

            // Products
            AllProductsListView.Visibility = Visibility.Hidden;
            ListOfProductsLabel.Visibility = Visibility.Hidden;
            AllProductsListView.ItemsSource = null;

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            log.Info(string.Format("Official logout: {0} {1} {2}", activeOfficial.Name, activeOfficial.SurName, activeOfficial.Guid));
            mainWindow.Show();
            Close();
        }


        // Pasword change
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();

            MainPageLabel.Visibility = Visibility.Visible;
            MainPageLabel.Content = "PASSWORD CHANGE:";

            OldPassword.Visibility = Visibility.Visible;
            OldPassword.Text = "Old password ...";
            OldPassword.IsEnabled = true;

            NewPassword1.Visibility = Visibility.Visible;
            NewPassword1.IsEnabled = false;
            NewPassword1.Text = "New password ...";

            NewPassword2.Visibility = Visibility.Visible;
            NewPassword2.IsEnabled = false;
            NewPassword2.Text = "New password ...";

            ConfirmButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;

            OldPassword.Focus();
        }

        private void OldPasswordOnKeyDown_Handler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (OldPassword.Text == activeOfficial.Password)
                {
                    OldPasswordCorrect();
                }
                else
                {
                    OldPassword.Text = "";
                    OldPassword_YesIcon.Visibility = Visibility.Hidden;
                    OldPassword_NoIcon.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (OldPassword.Text == "Old password ...")
                {
                    OldPassword.Text = "";
                    OldPassword.Foreground = Brushes.Black;
                    OldPassword.FontStyle = FontStyles.Normal;
                }
            }
        }

        private void OldPasswordOnKeyUp_Handler(object sender, KeyEventArgs e)
        {
            if (OldPassword.Text == activeOfficial.Password)
            {
                OldPasswordCorrect();
            }
            else
            {
                OldPassword_NoIcon.Visibility = Visibility.Visible;
                OldPassword_YesIcon.Visibility = Visibility.Hidden;
                oldPasswordCheck = false;
            }
        }

        private void NewPasswordOnKeyDown_Handler(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text is "New password ...")
            {
                textbox.Text = "";                
            }
            textbox.Foreground = Brushes.Black;
            textbox.FontStyle = FontStyles.Normal;

        }

        private void NewPasswordOnKeyUp_Handler(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            switch (textbox.Name)
            {
                case "NewPassword1":
                    if (textbox.Text.Length >= User.passwordMinLength &&
                        Validator.Validator.ValidatePassword(textbox.Text))
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

        private void OldPasswordCorrect()
        {
            oldPasswordCheck = true;
            NewPassword1.IsEnabled = true;
            NewPassword2.IsEnabled = true;
            OldPassword_YesIcon.Visibility = Visibility.Visible;
            OldPassword_NoIcon.Visibility = Visibility.Hidden;
        }

        private void PasswordOnMouseClick(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text is "New password ..." || textbox.Text is "Old password ...")
            {
                textbox.Text = "";
            }
        }

        private bool PasswordChangingProcess()
        {
            if (oldPasswordCheck)
            {
                if (NewPassword1.Text == NewPassword2.Text && !String.IsNullOrEmpty(NewPassword1.Text))
                {
                    //Validace hesla
                    if (!Validator.Validator.ValidatePassword(NewPassword1.Text))
                    {
                        MessageBox.Show(String.Format("Weak password."),
                                        "",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                        return false;
                    }

                    activeOfficial.Password = NewPassword1.Text;

                    bool result = UsersORM.ChangePassword(activeOfficial);
                    if (result)
                    {
                        SetDefaultSettings();
                        return true;
                    }
                }
                else if (String.IsNullOrEmpty(NewPassword1.Text) ||
                         NewPassword1.Text == "New password ..." ||
                         String.IsNullOrEmpty(NewPassword2.Text) ||
                        NewPassword2.Text == "New password ...")
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
                MessageBox.Show("Wrong old password.",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            return false;
        }


        // Confirm Storno button
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordChangingProcess())
            {
                MessageBox.Show("Password was changed.");
            }
        }

        private void StornoButton_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
        }


        // Edit mode, update user
        private void UpdateMyAccount_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            OpenDetailViewOfUser(activeOfficial);
        }

        private void OpenDetailViewOfUser(Official official)
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

            MainPageLabel.Content = "UPDATE MY ACCOUNT:";
            MainPageLabel.IsEnabled = true;
            EditModeButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;

            tempOfficial = UsersORM.GetOfficialById(official.CompanyNumber);

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

        public void OpenDetailViewOfUser(Customer customer)
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
            LoginLabel.Content = "SSN";
            

            tempCustomer = UsersORM.GetCustomerBySSN(customer.SSN);

            NameTextBox.Text = customer.Name;
            SurnameTextBox.Text = customer.SurName;
            PhoneTextBox.Text = customer.Phone;
            EmailTextBox.Text = customer.Mail;

            Address address = UsersORM.SelectAddressById(customer.Address.Id);
            customer.Address = address;

            StreetTextBox.Text = customer.Address.Street;
            StreetNumberTextBox.Text = customer.Address.StreetNumber;
            CityTextBox.Text = customer.Address.City;
            PostalCodeTextBox.Text = customer.Address.PostalCode;
            CountryTextBox.Text = customer.Address.Country;
            LoginTextBox.Text = customer.SSN;
            UserTypeComboBox.Text = "Customer";

            switch (customer.CustomerType)
            {
                case CustomerType.Person:
                    CustomerSubTypeComboBox.SelectedItem = CustomerSubTypeComboBox_Person;
                    break;
                case CustomerType.Company:
                    CustomerSubTypeComboBox.SelectedItem = CustomerSubTypeComboBox_Company;
                    break;
            }

            switch (customer.Valid)
            {
                case true:
                    ValidTextBox.Text = "Yes";
                    break;
                case false:
                    ValidTextBox.Text = "No";
                    break;
            }


            AllProductsListView.Visibility = Visibility.Visible;
            ListOfProductsLabel.Visibility = Visibility.Visible;
            AllProductsListView.ItemsSource = BillORM.GetBillsByCustomerId(customer);


        }

        private void UserEditModeButton(object sender, RoutedEventArgs e)
        {
            foreach (Control c in userControlsList)
            {
                c.IsEnabled = true;
            }
            ValidTextBox.IsEnabled = false;
            UserTypeComboBox.IsEnabled = false;

            foreach (Control c in addressControlsList)
            {
                c.IsEnabled = true;
            }
            CountryTextBox.IsEnabled = false;
            LoginTextBox.IsEnabled = false;

            MainPageLabel.Content = "USER UPDATE:";
            EditModeButton.Visibility = Visibility.Hidden;
            UpdateUserButton.Visibility = Visibility.Visible;

            if (UserTypeComboBox.SelectedItem == UserTypeComboBox_Customer)
            {
                LoginTextBox.IsEnabled = false;
                CustomerSubTypeComboBox.IsEnabled = false;
            }
        }

        private void UpdateUserInDatabaseButton(object sender, RoutedEventArgs e)
        {
            if (!allUserFieldsAreNotEmpty())
            {
                MessageBox.Show("Musíš vyplnit všechna pole");
                return;
            }
            if (UserTypeComboBox.SelectedItem == UserTypeComboBox_Customer)
            {
                UpdateUserProcess(tempCustomer);
            }
            else if (UserTypeComboBox.SelectedItem == UserTypeComboBox_Official)
            {
                UpdateUserProcess(tempOfficial);
            }

        }

        private void UpdateUserProcess(Customer tempCustomer)
        {
            Customer updatedCustomer = new Customer();
            Address updatedAddress = new Address();


            bool userWasChanged = false;
            bool addressWasChanged = false;
            bool addressExistsInDb = false;

            updatedCustomer.Guid = tempCustomer.Guid;
            updatedCustomer.Name = NameTextBox.Text;
            updatedCustomer.SurName = SurnameTextBox.Text;
            updatedCustomer.Mail = EmailTextBox.Text;
            updatedCustomer.Phone = PhoneTextBox.Text;
            updatedCustomer.SSN = LoginTextBox.Text;

            if (CustomerSubTypeComboBox.SelectedItem == CustomerSubTypeComboBox_Person)
                updatedCustomer.CustomerType = CustomerType.Person;
            else if (CustomerSubTypeComboBox.SelectedItem == CustomerSubTypeComboBox_Company)
                updatedCustomer.CustomerType = CustomerType.Company;

            updatedCustomer.Address = tempCustomer.Address;

            updatedAddress.Id = tempCustomer.Address.Id;
            updatedAddress.Street = StreetTextBox.Text;
            updatedAddress.StreetNumber = StreetNumberTextBox.Text;
            updatedAddress.City = CityTextBox.Text;
            updatedAddress.PostalCode = PostalCodeTextBox.Text;
            updatedAddress.Country = CountryTextBox.Text;
            Address addressFromDb = UsersORM.IsAddressInDatabase(updatedAddress);

            if (tempCustomer.Name == updatedCustomer.Name &&
                tempCustomer.SurName == updatedCustomer.SurName &&
                tempCustomer.Phone == updatedCustomer.Phone &&
                tempCustomer.Mail == updatedCustomer.Mail &&
                tempCustomer.SSN == updatedCustomer.SSN &&
                tempCustomer.CustomerType == updatedCustomer.CustomerType)
            {
                userWasChanged = false;
            }
            else
            {
                userWasChanged = true;
            }

            if (updatedAddress.Street == tempCustomer.Address.Street &&
                updatedAddress.StreetNumber == tempCustomer.Address.StreetNumber &&
                updatedAddress.City == tempCustomer.Address.City &&
                updatedAddress.PostalCode == tempCustomer.Address.PostalCode &&
                updatedAddress.Country == tempCustomer.Address.Country)
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
                    updatedCustomer.Address = addressFromDb;
                }
                else if (!addressExistsInDb)
                {

                    updatedAddress.Id = UsersORM.GetNewAddressId();
                    bool addressIsCreated = UsersORM.CreateAddress(updatedAddress);
                    if (addressIsCreated)
                    {
                        updatedCustomer.Address = updatedAddress;
                    }
                    else
                    {
                        MessageBox.Show("Address was not created. Contact administrator.",
                                        "",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                        return;
                    }
                }
            }

            bool result = UsersORM.UpdateCustomer(updatedCustomer);
            if (result)
            {
                MessageBox.Show("Update of user successful.",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                
                //try
                //{
                //    if (tempCustomer.Guid == activeCustomer.Guid)
                //        activeCustomer = updatedCustomer;
                //}
                //catch (Exception ex)
                //{

                //}

                tempCustomer = updatedCustomer;
            }

            else
                MessageBox.Show("Update of customer not successful. Contact administrator.",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
        }

        private void UpdateUserProcess(Official tempOfficial)
        {
            Official updatedOfficial = new Official();
            Address updatedAddress = new Address();


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
                        MessageBox.Show("Address was not created. Contact administrator.",
                                        "",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                        return;
                    }
                }
            }

            bool result = UsersORM.UpdateOfficial(updatedOfficial);
            if (result)
            {
                MessageBox.Show("Update of user successful.",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                if (tempOfficial.Guid == activeOfficial.Guid)
                    activeOfficial = updatedOfficial;
                tempOfficial = updatedOfficial;

            }

            else
                MessageBox.Show("Update of customer not successful. Contact administrator.",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
        }

        // Create new customer
        private void CreateNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            MainPageLabel.Content = "CREATE NEW CUSTOMER:";
            foreach (Control c in userControlsList)
                c.Visibility = Visibility.Visible;
            foreach (Control c in addressControlsList)
                c.Visibility = Visibility.Visible;

            OfficialSubTypeComboBox.Visibility = Visibility.Hidden;
            CustomerSubTypeComboBox.Visibility = Visibility.Visible;

            StornoButton.Visibility = Visibility.Visible;
            CreateCustomerButton.Visibility = Visibility.Visible;

            ValidTextBox.Text = "Yes";
            ValidTextBox.IsEnabled = false;

            UserTypeComboBox.Text = "Customer";
            UserTypeComboBox.IsEnabled = false;

            CountryTextBox.Text = "CZE";
            CountryTextBox.IsEnabled = false;

            LoginLabel.Content = "SSN";
            NameTextBox.Focus();

        }

        private void CreateNewCustomerInDB_Click(object sender, RoutedEventArgs e)
        {
            Customer newCustomer = new Customer();
            Address newAddress = new Address();


            if (!allUserFieldsAreNotEmpty())
            {
                MessageBox.Show("Please fill in all user information",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            if (!allAddressFieldsAreNotEmpty())
            {
                MessageBox.Show("Please fill in all address fields",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            if (!Validator.Validator.NameValidator(NameTextBox.Text) ||
                !Validator.Validator.NameValidator(SurnameTextBox.Text) ||
                !Validator.Validator.EmailValidator(EmailTextBox.Text) ||
                !Validator.Validator.PhoneValidator(PhoneTextBox.Text) ||
                !Validator.Validator.StreetValidator(StreetTextBox.Text) ||
                !Validator.Validator.StreetNumberValidator(StreetNumberTextBox.Text) ||
                !Validator.Validator.CityValidator(CityTextBox.Text) ||
                !Validator.Validator.PostalCodeValidator(PostalCodeTextBox.Text) ||
                !Validator.Validator.SSNValidator(LoginTextBox.Text)
    )
            {
                MessageBox.Show("Incorect inputs. Please check user information.",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);

                if (!Validator.Validator.NameValidator(NameTextBox.Text))
                {
                    NameTextBox_NoIcon.Visibility = Visibility.Visible;
                }
                if (!Validator.Validator.NameValidator(SurnameTextBox.Text))
                {
                    SurnameTextBox_NoIcon.Visibility = Visibility.Visible;
                }
                if (!Validator.Validator.EmailValidator(EmailTextBox.Text))
                {
                    EmailTextBox_NoIcon.Visibility = Visibility.Visible;
                }
                if (!Validator.Validator.PhoneValidator(PhoneTextBox.Text))
                {
                    PhoneTextBox_NoIcon.Visibility = Visibility.Visible;
                }
                if (!Validator.Validator.StreetValidator(StreetTextBox.Text))
                {
                    StreetTextBox_NoIcon.Visibility = Visibility.Visible;
                }
                if (!Validator.Validator.StreetNumberValidator(StreetNumberTextBox.Text))
                {
                    StreetNumberTextBox_NoIcon.Visibility = Visibility.Visible;
                }
                if (!Validator.Validator.CityValidator(CityTextBox.Text))
                {
                    CityTextBox_NoIcon.Visibility = Visibility.Visible;
                }
                if (!Validator.Validator.PostalCodeValidator(PostalCodeTextBox.Text))
                {
                    PostalCodeTextBox_NoIcon.Visibility = Visibility.Visible;
                }
                if (!Validator.Validator.SSNValidator(LoginTextBox.Text))
                {
                    LoginTextBox_NoIcon.Visibility = Visibility.Visible;
                }

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
                MessageBox.Show("New address was not created. Contact administrator",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

            newCustomer.Guid = Guid.NewGuid();
            newCustomer.Name = NameTextBox.Text;
            newCustomer.SurName = SurnameTextBox.Text;
            newCustomer.Address = newAddress;
            newCustomer.Mail = EmailTextBox.Text;
            newCustomer.Phone = PhoneTextBox.Text;
            newCustomer.Valid = true;
            newCustomer.Password = "heslo";
            newCustomer.SSN = LoginTextBox.Text;

            if (CustomerSubTypeComboBox.SelectedItem == CustomerSubTypeComboBox_Person)
                newCustomer.CustomerType = CustomerType.Person;

            else if (CustomerSubTypeComboBox.SelectedItem == CustomerSubTypeComboBox_Company)
                newCustomer.CustomerType = CustomerType.Company;



            bool userIsCreated = UsersORM.CreateNewCustomer(newCustomer);
            if (userIsCreated)
            {
                MessageBox.Show(String.Format("New User was created: {0} {1}", newCustomer.Name, newCustomer.SurName),
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                OpenDetailViewOfUser(newCustomer);
            }
            else
            {
                MessageBox.Show("New user was not created. Contact administrator",
                                "",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

        }

        // Search
        private void SearchTextBox_KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SelectDataForListView();
            }
        }

        private void SearchTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text is "Enter user name ...")
            {
                textbox.Text = "";
            }
        }

        private void ClickOnButtonViewDetails(object sender, RoutedEventArgs e)
        {
            Customer customer = (Customer)AllCustomersListView.SelectedItems[0];
            OpenDetailViewOfUser(customer);
        }

        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            MainPageLabel.Content = "CUSTOMER SEARCH:";
            MainPageLabel.Visibility = Visibility.Visible;
            SearchTextBox.Visibility = Visibility.Visible;
            AllCustomersListView.Visibility = Visibility.Visible;
            ViewDetails.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;
            SearchButton.Visibility = Visibility.Visible;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SelectDataForListView();
        }

        private void SelectDataForListView()
        {
            List<Customer> customers = UsersORM.GetAllCustomers();
            var result = customers.Where(X => X.Name.ToLower().Contains(SearchTextBox.Text.ToLower())
                                    || X.SurName.ToLower().Contains(SearchTextBox.Text.ToLower())
                                    || String.Format("{0} {1}", X.Name, X.SurName).ToLower().Contains(SearchTextBox.Text.ToLower())
                                    || String.Format("{1} {0}", X.Name, X.SurName).ToLower().Contains(SearchTextBox.Text.ToLower())
                                    );
            AllCustomersListView.ItemsSource = result;

        }

        // Secret page
        private void OpenSecretPage(object sender, RoutedEventArgs e)
        {
            SecretWindow secretWindow = new SecretWindow(activeOfficial);
            secretWindow.Show();
            Close();
        }

        // Products overview
        private void ViewProductDetails_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Bill bill = BillORM.GetBillbyBillNumber((int)b.CommandParameter);
            ProductWindow productWindow = new ProductWindow(activeOfficial, bill);
            productWindow.Show();
            Close();
        }

        // Other
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
            if (Validator.Validator.CityValidator(t.Text) || String.IsNullOrEmpty(t.Text))
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

            if (UserTypeComboBox.SelectedItem == UserTypeComboBox_Customer)
                userType = "customer";
            else if (UserTypeComboBox.SelectedItem == UserTypeComboBox_Official)
                userType = "official";

            switch (userType)
            {
                case "customer":
                    if (Validator.Validator.SSNValidator(t.Text) || String.IsNullOrEmpty(t.Text))
                    {
                        LoginTextBox_NoIcon.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        LoginTextBox_NoIcon.Visibility = Visibility.Visible;
                    }
                   break;
                case "official":
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
