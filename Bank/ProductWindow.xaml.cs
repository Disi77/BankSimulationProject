using System;
using System.Windows;
using Bank.Objects;
using Bank.ORM;

namespace Bank
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
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

            TransactionListBox.ItemsSource = TransactionORM.GetTransactionByBillId(activeBill);
            
        }

        private void BackToCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            OfficialWindow officialWindow = new OfficialWindow(activeOfficial);
            officialWindow.Show();
            officialWindow.OpenDetailViewOfUser(customer);                        
            Close();
        }
    }
}
