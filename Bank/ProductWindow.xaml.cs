using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

            string partialSSN = customer.SSN.Substring(0, 6) + "XXXX";

            UserInfo1.Content = String.Format("Name: {0} {1}, SSN: {2}", customer.SurName, customer.Name, partialSSN);
            UserInfo2.Content = String.Format("Address: {0}", address.ToString());
            UserInfo3.Content = String.Format("Contact: {0}, {1}", customer.Phone, customer.Mail);
            BillInfo1.Content = String.Format("Bill Number: {0}", activeBill.BillNumber);
            BillInfo2.Content = String.Format("Balance: {0:n} Kč", activeBill.Balance);

            AllPaymentsRadioButton.IsChecked = true;
            NewestToOldest.IsChecked = true;

            GetAllTransactionBySelectedCriterias();
        }

        private void GetAllTransactionBySelectedCriterias()
        {
            List<Transaction> transactions = TransactionORM.GetTransactionByBillId(activeBill);

            if (OnlyIncomingRadioButton.IsChecked == true)
            {
                transactions = transactions.Where(X => X.TransactionType == TransactionType.Incoming)
                                           .ToList();
            }
            else if (OnlyOutgoingRadioButton.IsChecked == true)
            {
                transactions = transactions.Where(X => X.TransactionType == TransactionType.Outgoing)
                                           .ToList();
            }

            if (TransactionDateFrom.SelectedDate != null)
            {
                transactions = transactions.Where(X => X.DateTransaction >= (DateTime)TransactionDateFrom.SelectedDate)
                                           .ToList();
            }
            if (TransactionDateTo.SelectedDate != null)
            {
                transactions = transactions.Where(X => X.DateTransaction <= (DateTime)TransactionDateTo.SelectedDate)
                                           .ToList();
            }

            if (AmountFromTextBox.Text != "")
            {
                int amountFrom = int.Parse(AmountFromTextBox.Text);
                transactions = transactions.Where(X => X.Amount >= amountFrom)
                                           .ToList();
            }
            if (AmountToTextBox.Text != "")
            {
                int amountTo = int.Parse(AmountToTextBox.Text);
                transactions = transactions.Where(X => X.Amount <= amountTo)
                                           .ToList();
            }

            if (VariableSymbolTextBox.Text != "")
            {
                transactions = transactions.Where(X => X.VariableSymbol.ToString().Contains(VariableSymbolTextBox.Text))
                                           .ToList();
            }

            if (BillNumberTextBox.Text != "")
            {
                List<Transaction> transactions1 = new List<Transaction>();

                transactions1 = transactions.Where(X => X.TransactionType == TransactionType.Incoming)
                                            .Where(X => X.PayerBillNum.ToString().Contains(BillNumberTextBox.Text))
                                            .ToList();

                transactions = transactions.Where(X => X.TransactionType == TransactionType.Outgoing)
                                            .Where(X => X.RecipientBillNum.ToString().Contains(BillNumberTextBox.Text))
                                            .ToList();

                transactions.AddRange(transactions1);
            }


            if (NewestToOldest.IsChecked == true)
            {
                transactions = transactions.OrderByDescending(X => X.DateTransaction)
                                           .ToList();
            }
            else if (OldestToNewest.IsChecked == true)
            {
                transactions = transactions.OrderBy(X => X.DateTransaction)
                                           .ToList();
            }
            else if (HighestToLowest.IsChecked == true)
            {
                List<Transaction> transactions1 = new List<Transaction>();

                transactions1 = transactions.Where(X => X.TransactionType == TransactionType.Outgoing)
                                           .OrderBy(X => X.Amount)
                                           .ToList();

                transactions = transactions.Where(X => X.TransactionType == TransactionType.Incoming)
                           .OrderByDescending(X => X.Amount)
                           .ToList();

                transactions.AddRange(transactions1);
            }
            else if (LowestToHighest.IsChecked == true)
            {
                List<Transaction> transactions1 = new List<Transaction>();

                transactions1 = transactions.Where(X => X.TransactionType == TransactionType.Incoming)
                                           .OrderBy(X => X.Amount)
                                           .ToList();

                transactions = transactions.Where(X => X.TransactionType == TransactionType.Outgoing)
                           .OrderByDescending(X => X.Amount)
                           .ToList();

                transactions.AddRange(transactions1);
            }

            TransactionListBox.ItemsSource = transactions;
            TransactionListBox.Items.Refresh();

            int sumOutgoing = 0;
            int sumIncoming = 0;

            if (transactions.Any())
            {
                foreach (Transaction t in transactions)
                {
                    switch (t.TransactionType)
                    {
                        case TransactionType.Incoming:
                            sumIncoming += t.Amount;
                            break;
                        case TransactionType.Outgoing:
                            sumOutgoing += t.Amount;
                            break;
                        default:
                            break;
                    }
                }
            }

            UpdateIncomingOutgoingBalanceLabels(sumOutgoing, sumIncoming, sumIncoming - sumOutgoing);
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

        private void UpdateIncomingOutgoingBalanceLabels(int o, int i, int b)
        {
            SumIncomingPaymentsLabel.Content = String.Format("{0:n} Kč", i);

            if (o == 0)
            {
                SumOutgoingPaymentsLabel.Content = String.Format("{0:n} Kč", o);
                SumOutgoingPaymentsLabel.Foreground = Brushes.Black;
            }
            else
            {
                SumOutgoingPaymentsLabel.Content = String.Format("-{0:n} Kč", o);
                SumOutgoingPaymentsLabel.Foreground = Brushes.Red;
            }

            BalanceBySelectedCriteriaLabel.Content = String.Format("{0:n} Kč", b);
            if (b < 0)
            {
                BalanceBySelectedCriteriaLabel.Foreground = Brushes.Red;
            }
            else
            {
                BalanceBySelectedCriteriaLabel.Foreground = Brushes.Black;
            }

        }

        private void SortingTransactionByDate(object sender, RoutedEventArgs e)
        {
            GetAllTransactionBySelectedCriterias();
        }

        private void DefaultViewButton_Click(object sender, RoutedEventArgs e)
        {
            AllPaymentsRadioButton.IsChecked = true;
            TransactionDateFrom.SelectedDate = null;
            TransactionDateTo.SelectedDate = null;
            AmountFromTextBox.Text = "";
            AmountToTextBox.Text = "";
            VariableSymbolTextBox.Text = "";
            BillNumberTextBox.Text = "";
            NewestToOldest.IsChecked = true;

            GetAllTransactionBySelectedCriterias();
        }
    }
}
