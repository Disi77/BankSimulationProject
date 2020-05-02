using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Bank.Objects;
using Bank.ORM;
using Bank.Types;
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

            GetAllTransactionBySelectedCriterias();
        }

        private void GetAllTransactionBySelectedCriterias()
        {
            List<Transaction> transactions = TransactionORM.GetTransactionByBillId(activeBill);

            if (OnlyIncomingRadioButton.IsChecked == true)
            {
                transactions = transactions.Where(X => X.TransactionType == TransactionType.Incoming).ToList();
            }
            else if (OnlyOutgoingRadioButton.IsChecked == true)
            {
                transactions = transactions.Where(X => X.TransactionType == TransactionType.Outgoing).ToList();
            }

            if (TransactionDateFrom.SelectedDate != null)
            {
                transactions = transactions.Where(X => X.DateTransaction >= (DateTime)TransactionDateFrom.SelectedDate).ToList();
            }
            if (TransactionDateTo.SelectedDate != null)
            {
                transactions = transactions.Where(X => X.DateTransaction <= (DateTime)TransactionDateTo.SelectedDate).ToList();
            }
            
            if (AmountFromTextBox.Text != "")
            {
                int amountFrom = int.Parse(AmountFromTextBox.Text);
                transactions = transactions.Where(X => X.Amount >= amountFrom).ToList();
            }
            if (AmountToTextBox.Text != "")
            {
                int amountTo = int.Parse(AmountToTextBox.Text);
                transactions = transactions.Where(X => X.Amount <= amountTo).ToList();
            }

            
            transactions = transactions.OrderByDescending(X => X.DateTransaction).ToList();

            TransactionListBox.ItemsSource = transactions;
            TransactionListBox.Items.Refresh();
        }

        private void BackToCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            OfficialWindow officialWindow = new OfficialWindow(activeOfficial);
            officialWindow.Show();
            officialWindow.OpenDetailViewOfUser(customer);                        
            Close();
        }

        private void TypePaymentsSelection_Click(object sender, RoutedEventArgs e)
        {
            AllPaymentsRadioButton.IsChecked = false;
            OnlyIncomingRadioButton.IsChecked = false;
            OnlyOutgoingRadioButton.IsChecked = false;

            RadioButton b = sender as RadioButton;
            b.IsChecked = true;

            GetAllTransactionBySelectedCriterias();
        }

        private void TransactionDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllTransactionBySelectedCriterias();
        }

        private void AmountTextBox_KeyUp(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AmountTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                GetAllTransactionBySelectedCriterias();
            }
        }
    }
}
