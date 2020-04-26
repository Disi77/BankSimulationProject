using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Bank.Objects;
using Bank.ORM;
using MahApps.Metro.Controls;

namespace Bank
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : MetroWindow
    {
        private Official activeOfficial;
        private Bill activeBill;
        private Customer customer;
        private Address address;

        public ProductWindow(Official official, Bill bill)
        {
            activeOfficial = official;
            activeBill = bill;
            InitializeComponent();
            SetDefaultSettings();
        }

        private void SetDefaultSettings()
        {
            customer = UsersORM.GetCustomerByGuid(activeBill.CustomerId);
            address = UsersORM.SelectAddressById(customer.Address.Id);
            BillNumberLabel.Content = activeBill.BillNumber;

            string partialSSN = customer.SSN.Substring(0,6) + "XXXX";

            UserInfo1.Content = String.Format("Name: {0} {1}, SSN: {2}", customer.SurName, customer.Name, partialSSN);
            UserInfo2.Content = String.Format("Address: {0}", address.ToString());
            UserInfo3.Content = String.Format("Contact: {0}, {1}", customer.Phone, customer.Mail);
            BillInfo1.Content = String.Format("Bill Number: {0}", activeBill.BillNumber);
            BillInfo2.Content = String.Format("Balance: {0}", activeBill.Balance);

            GetAllTransactionByKey("default");


        }

        private void GetAllTransactionByKey(string key)
        {
            List<Transaction> all = TransactionORM.GetTransactionByBillId(activeBill);


            switch (key)
            {
                case "all":
                    TransactionListBox.ItemsSource = all;
                    break;
                case "default":
                    DateTime before30days = DateTime.Today.AddDays(-30);
                    var result = all.Where(X => X.DateTransaction > before30days);
                    TransactionListBox.ItemsSource = result;
                    break;

            }
            SwitchOutputLabel.Content = key;

        }

        private void BackToCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            OfficialWindow officialWindow = new OfficialWindow(activeOfficial);
            officialWindow.Show();
            officialWindow.OpenDetailViewOfUser(customer);                        
            Close();
        }

        private void FilterButton_All_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TypePaymentsSelection_Click(object sender, RoutedEventArgs e)
        {
            AllPaymentsRadioButton.IsChecked = false;
            OnlyIncomingRadioButton.IsChecked = false;
            OnlyOutgoingRadioButton.IsChecked = false;

            RadioButton b = sender as RadioButton;
            b.IsChecked = true;

            if (AllPaymentsRadioButton.IsChecked == true)
            {

            }
        }
    }
}
