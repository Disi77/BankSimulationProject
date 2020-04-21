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

namespace Bank
{
    /// <summary>
    /// Interaction logic for OfficialWindow.xaml
    /// </summary>
    public partial class OfficialWindow : Window
    {
        private Official activeOfficial;
        private Official tempOfficial;
        private Customer activeCustomer;
        private Customer tempCustomer;
        private Log log;
        private List<Control> changePasswordControlsList = new List<Control>();
        private List<Control> userControlsList = new List<Control>();
        private List<Control> addressControlsList = new List<Control>();
        private bool currentPasswordCheck = false;

        public OfficialWindow(Official official)
        {
            InitializeComponent();
            activeOfficial = official;
            log = new Log();
            CreateControlsLists();
            SetDefaultSettings();
            if (activeOfficial.CompanyNumber == "123")
            {
                SecretButton.Visibility = Visibility.Visible;
            }
        }

        private void CreateControlsLists()
        {
            changePasswordControlsList.Add(MainLabel);
            changePasswordControlsList.Add(CurrentPassword);
            changePasswordControlsList.Add(NewPassword1);
            changePasswordControlsList.Add(NewPassword2);
            changePasswordControlsList.Add(CurrentPasswordLabel);
            changePasswordControlsList.Add(NewPasswordLabel);


            userControlsList.Add(MainLabel);
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
            CurrentPassword.Text = "Enter current password";
            NewPassword1.Text = "Enter new password";
            NewPassword2.Text = "Enter new password";

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
            EmailTextBox.Text = "";
            PhoneTextBox.Text = "";
            ValidTextBox.Text = "Yes";
            LoginTextBox.Text = "";
            UserTypeComboBox.SelectedItem = UserTypeComboBox_Official;
            OfficialSubTypeComboBox.SelectedItem = OfficialSubTypeComboBox_Junior;
            CustomerSubTypeComboBox.SelectedItem = CustomerSubTypeComboBox_Person;

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
            ViewDetailsButton.Visibility = Visibility.Hidden;
            ViewDetailsButton.IsEnabled = true;

            // Search 
            SearchTextBox.Visibility = Visibility.Hidden;
            SearchTextBox.Text = "Enter user name ...";
            SearchLabel.Visibility = Visibility.Hidden;
            AllCustomersListView.ItemsSource = null;

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

            MainLabel.Visibility = Visibility.Visible;

            CurrentPassword.Visibility = Visibility.Visible;
            CurrentPassword.Text = "Enter current password";
            CurrentPassword.IsEnabled = true;

            NewPassword1.Visibility = Visibility.Visible;
            NewPassword1.IsEnabled = false;
            NewPassword1.Text = "Enter new password";

            NewPassword2.Visibility = Visibility.Visible;
            NewPassword2.IsEnabled = false;
            NewPassword2.Text = "Enter new password";

            ConfirmButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;

            CurrentPasswordLabel.Content = "";
            NewPasswordLabel.Content = "";

        }

        private void CurrentPasswordOnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CurrentPassword.IsEnabled = false;
                CurrentPasswordLabel.Visibility = Visibility.Visible;
                if (CurrentPassword.Text == activeOfficial.Password)
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

        private void CurrentPasswordOnMouseClick(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text is "Enter new password" || textbox.Text is "Enter current password")
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
                    if (User.minimalniDelkaHesla > NewPassword1.Text.Length)
                    {
                        MessageBox.Show(String.Format("Password too short. Minimal lenght is {0} characters", User.minimalniDelkaHesla));
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
                else
                {
                    MessageBox.Show("Enter twice same new password.");
                }
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

            MainLabel.Content = "UPDATE MY ACCOUNT:";
            MainLabel.IsEnabled = true;
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

        private void OpenDetailViewOfUser(Customer customer)
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

            MainLabel.Content = "USER DETAILS:";
            MainLabel.IsEnabled = true;
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

            MainLabel.Content = "USER UPDATE:";
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
                        MessageBox.Show("We have troubles with new Address creating.");
                        return;
                    }
                }
            }

            bool result = UsersORM.UpdateCustomer(updatedCustomer);
            if (result)
            {
                MessageBox.Show("Update of user successful.");
                
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
                MessageBox.Show("Update of customer NOT successful.");
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
                        MessageBox.Show("We have troubles with new Address creating.");
                        return;
                    }
                }
            }

            bool result = UsersORM.UpdateOfficial(updatedOfficial);
            if (result)
            {
                MessageBox.Show("Update of user successful.");
                if (tempOfficial.Guid == activeOfficial.Guid)
                    activeOfficial = updatedOfficial;
                tempOfficial = updatedOfficial;

            }

            else
                MessageBox.Show("Update of official NOT successful.");
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

        private void CreateNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultSettings();
            MainLabel.Content = "CREATE NEW CUSTOMER:";
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

        }

        private void CreateNewCustomerInDB_Click(object sender, RoutedEventArgs e)
        {
            Customer newCustomer = new Customer();
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

            if (!allUserFieldsAreNotEmpty())
            {
                MessageBox.Show("Some fields are empty.");
                return;
            }

            bool userIsCreated = UsersORM.CreateNewCustomer(newCustomer);
            if (userIsCreated)
            {
                MessageBox.Show(String.Format("New User was created: {0} {1}", newCustomer.Name, newCustomer.SurName));
                OpenDetailViewOfUser(newCustomer);
            }
            else
            {
                MessageBox.Show("Sometring wrong. Error num: 45678");
            }

        }

        private void SearchTextBox_KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<Customer> customers = UsersORM.GetAllCustomers();
                var result = customers.Where(X => X.Name.ToLower().Contains(SearchTextBox.Text.ToLower())
                                        || X.SurName.ToLower().Contains(SearchTextBox.Text.ToLower())
                                        || String.Format("{0} {1}", X.Name, X.SurName).ToLower().Contains(SearchTextBox.Text.ToLower())
                                        || String.Format("{1} {0}", X.Name, X.SurName).ToLower().Contains(SearchTextBox.Text.ToLower())
                                        );
                AllCustomersListView.ItemsSource = result;
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
            SearchLabel.Content = "CUSTOMER SEARCH:";
            SearchLabel.Visibility = Visibility.Visible;
            SearchTextBox.Visibility = Visibility.Visible;
            AllCustomersListView.Visibility = Visibility.Visible;
            ViewDetailsButton.Visibility = Visibility.Visible;
            StornoButton.Visibility = Visibility.Visible;
        }

        private void OpenSecretPage(object sender, RoutedEventArgs e)
        {
            SecretPage secretPage = new SecretPage();
            secretPage.Show();
            Close();
        }






        //NameBox.Text = official.Name;
        //SurNameBox.Text = official.SurName;
        //PhoneBox.Text = official.Phone;
        //MailBox.Text = official.Mail;

        // bude tam ověření - jméno a přijmení bude string a budou tam jen písmena
        // email bude nějaká maska ... 
        // třída Validator ... metody, které vrátí true nebo false podle toho, co budu kontrolovat, např. že ve stringu jsou jen písmena a nic jiného
        // a pak (po kliknutí na tlačítko) se bude volat Validator s příslušnými metodami a když to bude všechno true, tak se provede update
        // každý box bude mít svůj validátor a bude se tam ukazovat nějaký label
    }
}
