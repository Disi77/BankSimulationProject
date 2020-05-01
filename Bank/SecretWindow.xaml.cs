using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Bank.Objects;
using Bank.ORM;
using MahApps.Metro.Controls;

namespace Bank
{
    /// <summary>
    /// Interaction logic for SecretPage.xaml
    /// </summary>
    public partial class SecretWindow : MetroWindow
    {
        public Random random;
        Official Official { get; set; }
        List<Transaction> LastTransactions { get; set; }

        public SecretWindow(Official official)
        {
            InitializeComponent();
            PayerList.ItemsSource = BillORM.GetBills();
            RecipientList.ItemsSource = BillORM.GetBills();
            random = new Random();
            GenerateNewVariableSymbol();
            GenerateNewAmount();
            Official = official;
            LastTransactions = new List<Transaction>();
            GenerateLastTransactions();
        }

        private void GenerateNewVariableSymbol()
        {
            int result = random.Next(10000000, 99999999);
            VariableSymbolTextBox.Text = result.ToString();
        }

        private void GenerateNewAmount()
        {
            Random random = new Random();
            AmountTextBox.Text = (random.Next(1, 20) * 100).ToString();
        }

        private void GenerateLastTransactions()
        {
            //Bill bill = new Bill();
            //bill.BillNumber = 1230001;
            ////LastTransactionsListBox.ItemsSource = TransactionORM.GetTransactionByBillId(bill);
            //LastTransactions = TransactionORM.GetTransactionByBillId(bill);
            LastTransactionsListBox.ItemsSource = LastTransactions;
            LastTransactionsListBox.Items.Refresh();
        }

        private void UpdateLastTransactionsList(Transaction newTransaction)
        {
            LastTransactions.Insert(0, newTransaction);
            if (LastTransactions.Count > 5)
            {
                LastTransactions.RemoveAt(LastTransactions.Count - 1);
            }

            //Bill bill = new Bill();
            //bill.BillNumber = 1230002;
            //LastTransactionsListBox.ItemsSource = TransactionORM.GetTransactionByBillId(bill);
            //LastTransactions = TransactionORM.GetTransactionByBillId(bill);
            LastTransactionsListBox.ItemsSource = LastTransactions;
            LastTransactionsListBox.Items.Refresh();

            //LastTransactionsListBox.ItemsSource = LastTransactions;

            //foreach (Transaction t in LastTransactions)
            //    MessageBox.Show(t.ToString());

            //MessageBox.Show(newTransaction.ToString());
            //MessageBox.Show(LastTransactions.Count.ToString());
        }

        private void PayerSelection(object sender, SelectionChangedEventArgs e)
        {
            UpdatePayerLabelContent();
        }

        private void UpdatePayerLabelContent()
        {
            Bill bill = (Bill)PayerList.SelectedItem;
            Customer customer = UsersORM.GetCustomerByGuid(bill.CustomerId);
            int updatedBillBalance = BillORM.GetBillbyBillNumber(bill.BillNumber).Balance;
            PayerLabel.Content = String.Format("Payer: {0} {1} \nBalance: {2:n} Kč", customer.Name, customer.SurName, updatedBillBalance);
        }

        private void RecipientSelection(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecipientLabelContent();
        }

        private void UpdateRecipientLabelContent()
        {
            Bill bill = (Bill)RecipientList.SelectedItem;
            Customer customer = UsersORM.GetCustomerByGuid(bill.CustomerId);
            int updatedBillBalance = BillORM.GetBillbyBillNumber(bill.BillNumber).Balance;

            RecipientLabel.Content = String.Format("Recipient: {0} {1} \nBalance: {2:n} Kč", customer.Name, customer.SurName, updatedBillBalance);
        }

        private void CreateTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            Bill payer = (Bill)PayerList.SelectedItem;
            Bill recipient = (Bill)RecipientList.SelectedItem;

            CreateTransactionLabel.Content = "";

            if (PayerList.SelectedItem is null || RecipientList.SelectedItem is null)
            {
                CreateTransactionLabel.Content = "You have to select \none Payer and one Recipient";
                return;
            }

            if (payer.BillNumber == recipient.BillNumber)
            {
                CreateTransactionLabel.Content = "Unable create Transaction \nbecause Payer = Recipient";
                return;
            }

            Transaction newTransaction = new Transaction
            {
                Id = TransactionORM.GetNewTransactionId(),
                VariableSymbol = Int32.Parse(VariableSymbolTextBox.Text),
                Amount = Int32.Parse(AmountTextBox.Text),
                Valid = true,
                PayerBillNum = payer.BillNumber,
                RecipientBillNum = recipient.BillNumber,
                DateTransaction = DateTime.Now
            };

            DateTime? selectedDate = DateSelectionBox.SelectedDate;
            if (selectedDate.HasValue)
            {
                newTransaction.DateTransaction = (DateTime)DateSelectionBox.SelectedDate;
            }

            TransactionORM.CreateNewTransaction(newTransaction);
            BillORM.UpdateBillBalance(newTransaction);                     

            UpdatePayerLabelContent();
            UpdateRecipientLabelContent();
            GenerateNewVariableSymbol();
            GenerateNewAmount();

            UpdateLastTransactionsList(newTransaction);
        }


        private void CloseSecretPageButton_Click(object sender, RoutedEventArgs e)
        {
            OfficialWindow officialWindow = new OfficialWindow(Official);
            officialWindow.Show();
            Close();
        }
    }
}
