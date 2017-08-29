using System;

namespace pointofsale_application
{
    public class Transaction
    {
        private int txID;
        public int TxID
        {
            get { return txID; }
            set { txID = value; }
        }

        private int sku;
        public int SKU
        {
            get { return sku; }
            set { sku = value; }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private int qty;
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        private DateTime dateTime;
        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        private int userID;
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private double subtotal;
        public double Subtotal
        {
            get { return subtotal; }
            set { subtotal = value; }
        }

        private double total;
        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        private string tender;
        public string Tender
        {
            get { return tender; }
            set { tender = value; }
        }
    }
}