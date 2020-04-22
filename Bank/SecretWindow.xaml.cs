using System;
using System.Windows;
using System.Windows.Controls;
using Bank.Objects;
using Bank.ORM;

namespace Bank
{
    /// <summary>
    /// Interaction logic for SecretPage.xaml
    /// </summary>
    public partial class SecretWindow : Window
    {
        public Random random;

        public SecretWindow()
        {
            InitializeComponent();
            PayerList.ItemsSource = BillORM.GetBills();
            RecipientList.ItemsSource = BillORM.GetBills();
            random = new Random();
            GenerateNewVariableSymbol();

        }

        private void GenerateNewVariableSymbol()
        {
            int result = random.Next(10000000, 99999999);
            VariableSymbolTextBox.Text = result.ToString();
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
            PayerLabel.Content = String.Format("Payer: {0} {1} \n{2}", customer.Name, customer.SurName, updatedBillBalance);
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

            RecipientLabel.Content = String.Format("Recipient: {0} {1} \n{2}", customer.Name, customer.SurName, updatedBillBalance);
        }

        private void CreateTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            Bill payer = (Bill)PayerList.SelectedItem;
            Bill recipient = (Bill)RecipientList.SelectedItem;

            CreateTransactionLabel.Content = "";

            if (PayerList.SelectedItem is null || RecipientList.SelectedItem is null)
            {
                CreateTransactionLabel.Content = "You have to select payer and Recipient";
                return;
            }

            if (payer.BillNumber == recipient.BillNumber)
            {
                CreateTransactionLabel.Content = "Payer = Recipient";
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

            //MessageBox.Show(String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}",
            //    newTransaction.Id,
            //    newTransaction.VariableSymbol,
            //    newTransaction.Amount,
            //    newTransaction.Valid,
            //    newTransaction.PayerBillNum,
            //    newTransaction.RecipientBillNum,
            //    newTransaction.DateTransaction));

            TransactionORM.CreateNewTransaction(newTransaction);
            BillORM.UpdateBillBalance(newTransaction);

            UpdatePayerLabelContent();
            UpdateRecipientLabelContent();

            GenerateNewVariableSymbol();

        }
    }
}
