using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pointofsale_application
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    /// 
    
    public partial class Reports : Window
    {
        public static double tillCount = 5000.00;
        DatabaseAccess db = new DatabaseAccess();

        List<Transaction> Transactions = new List<Transaction>();
        List<DateTime> Dates = new List<DateTime>();
        List<Transaction> transFromDate = new List<Transaction>();
        List<Transaction> newTransactions = new List<Transaction>();


        public Reports()
        {
            InitializeComponent();
            InitializeTransactionList();
            FillDateColumn();

            UpdateDateTime();
            ChangeOrderNum();
            PrintTill();
        }

        private void ChangeOrderNum()
        {
            SqlCommand retrieveOrderNum = new SqlCommand("SELECT MAX(TxID) FROM Tx", db.AccessDB());
            int orderNum = (int)retrieveOrderNum.ExecuteScalar() + 1;

            OrderNumberBlock.Text = orderNum.ToString();
        }

        private void UpdateDateTime()
        {
            DateTimeTransactionField.Text = DateTime.Now.ToString();
        }

        public void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            tillCount = 10000;
            Till_Count.Text = "$10,000.00";
        }

        public void InitializeTransactionList()
        {
            SqlCommand transactions = new SqlCommand("SELECT TxID, SKU, Price, Qty, DateTime, UserID, Subtotal, Total, Tender FROM Tx ORDER by TxID DESC", db.AccessDB());
            SqlDataReader rd = transactions.ExecuteReader();
            while (rd.Read())
            {
                Transactions.Add(new Transaction()
                {
                    TxID = rd.GetInt32(rd.GetOrdinal("TxID")),
                    SKU = rd.GetInt32(rd.GetOrdinal("SKU")),
                    Price = (double)rd.GetDecimal(rd.GetOrdinal("Price")),
                    Qty = rd.GetInt32(rd.GetOrdinal("Qty")),
                    DateTime = rd.GetDateTime(rd.GetOrdinal("DateTime")),
                    UserID = rd.GetInt32(rd.GetOrdinal("UserID")),
                    Subtotal = (double)rd.GetDecimal(rd.GetOrdinal("Subtotal")),
                    Total = (double)rd.GetDecimal(rd.GetOrdinal("Total")),
                    Tender = rd.GetString(rd.GetOrdinal("Tender"))
                });
            }
        }

        public void RetrieveDates()
        {
            SqlCommand dates = new SqlCommand("SELECT DateTime FROM Tx ORDER BY DateTime DESC", db.AccessDB());
            SqlDataReader rd = dates.ExecuteReader();

            List<DateTime> tempDates = new List<DateTime>();

            while (rd.Read())
            {
                tempDates.Add(Convert.ToDateTime(rd["DateTime"]));
            }

            for(int i = 0; i < tempDates.Count; i++)
            {
                DateTime dateOnly = tempDates[i].Date;
                Dates.Add(dateOnly);
            }
        }

        private void RemoveDuplicateDates()
        {
            RetrieveDates();
            List<DateTime> noDupes = Dates.Distinct().ToList();
            Dates.Clear();
            foreach(DateTime val in noDupes)
            {
                Dates.Add(val);
            }
        }

        public void FillDateColumn() 
        {
            RemoveDuplicateDates();
            for(int i = 0; i < Dates.Count; i++)
            {
                DateTime date = Dates[i];
                Button newBtn = new Button();
                newBtn.Content = Dates[i];
                newBtn.Name = "Button" + i;
                newBtn.Click += (s, e) => { PopulateGrid(date); };
                DateColumn.Children.Add(newBtn);
            }
        }

        private void PopulateGrid(DateTime date)
        {
            TransactionGrid.Children.Clear();
            FillTransactionGrid(date);
        }

        public void noDupes()
        {
            newTransactions = Transactions;


            for(int i = 0; i < Transactions.Count; i++)
            {
                for(int j = 0; j < Transactions.Count; j++)
                {
                    if(Transactions[i].TxID == Transactions[j].TxID)
                    {
                        newTransactions.Remove(newTransactions[i]);
                    }
                }
            }
        }

        public void FillTransactionGrid(DateTime dateClicked)
        {
            int result;
            transFromDate.Clear();
            noDupes();
            foreach (Transaction t in newTransactions)
            {
                result = DateTime.Compare(t.DateTime.Date, dateClicked.Date);
                if(result == 0)
                {
                        transFromDate.Add(t);
                }
            }
           
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button newBtn = new Button();
                    if(transFromDate.Count > count)
                    {
                        newBtn.Content = transFromDate[count].TxID.ToString();
                        newBtn.Name = "Transaction" + transFromDate[count].TxID;
                        newBtn.Click += (s, e) => { AddTransactionToReceipt(Convert.ToInt32(newBtn.Content)); };
                        Grid.SetColumn(newBtn, j);
                        Grid.SetRow(newBtn, i);
                        TransactionGrid.Children.Add(newBtn);
                        count++;
                    }
                }
            }

        }

        /*TO DO
        1. Select Transactions where txID is the same as t.TxID
        2. Attach SKUs to item names
        3. Attach UserID to employee name
        4. Add necessary info to receipt*/

        private void AddTransactionToReceipt(int txID)
        {
            TransactionBlock.Text = "";
            int userID;
            string name;
            OrderNumberBlock.Text = txID.ToString();
            List<Transaction> sameTrans = new List<Transaction>();

            SqlCommand transactions = new SqlCommand("SELECT TxID, SKU, Price, Qty, DateTime, UserID, Subtotal, Total, Tender FROM Tx ORDER by TxID DESC", db.AccessDB());
            SqlDataReader rd = transactions.ExecuteReader();
            while (rd.Read())
            {
                sameTrans.Add(new Transaction()
                {
                    TxID = rd.GetInt32(rd.GetOrdinal("TxID")),
                    SKU = rd.GetInt32(rd.GetOrdinal("SKU")),
                    Price = (double)rd.GetDecimal(rd.GetOrdinal("Price")),
                    Qty = rd.GetInt32(rd.GetOrdinal("Qty")),
                    DateTime = rd.GetDateTime(rd.GetOrdinal("DateTime")),
                    UserID = rd.GetInt32(rd.GetOrdinal("UserID")),
                    Subtotal = (double)rd.GetDecimal(rd.GetOrdinal("Subtotal")),
                    Total = (double)rd.GetDecimal(rd.GetOrdinal("Total")),
                    Tender = rd.GetString(rd.GetOrdinal("Tender"))
                });
            }


            foreach (Transaction t in sameTrans)
            {
                if(txID == t.TxID)
                {
                    userID = t.UserID;
                    DateTimeTransactionField.Text = t.DateTime.ToString();
                    SubtotalTransactionField.Text = "$ " + String.Format("{0:0.00}", t.Subtotal);
                    TotalTransactionField.Text = "$ " + String.Format("{0:0.00}", t.Total);
                    PaymentTypeTransactionField.Text = t.Tender;
                    CashierTransactionField.Text = t.UserID.ToString();

                    SqlCommand itemName = new SqlCommand("SELECT Name FROM Inventory WHERE SKU = " + t.SKU, db.AccessDB());
                    name = itemName.ExecuteScalar().ToString();
                    
                    TransactionBlock.Text += name + " $ " + String.Format("{0:0.00}", t.Price) + " x " + t.Qty + "\n";
                }
            }
        }

        public double PrintTill()
        {
            try
            {
                SqlCommand retrieveOrderNum = new SqlCommand("SELECT MAX(Till) FROM TillCount", db.AccessDB());
                double newTill = Convert.ToDouble((decimal)retrieveOrderNum.ExecuteScalar());
                tillCount = newTill;
            }
            catch
            {
                tillCount = 5000;
            }
            Till_Count.Text = "$"  + tillCount.ToString();
            return tillCount;
        }
    }
}
