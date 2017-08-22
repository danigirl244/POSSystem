using System;
using System.Collections.Generic;

namespace pointofsale_application
{
    class Receipt
    {
        private int OrderID /*Pull the highest value from the database and increment from there*/;
        public int Order
        {
            get { return OrderID; }
            set { OrderID = value; }
        }
        private string cashierName;
        public string CashierName
        {
            get { return cashierName; }
            set { cashierName = value; }
        }
        private DateTime dateTime;
        public DateTime DT
        {
            get { return dateTime; }
            set { dateTime = value; }
        }
        private double subTotal;
        public double RecieptSubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }
        private double tax;
        public double RecieptTax
        {
            get { return tax; }
            set { tax = value; }
        }
        private double total;
        public double RecieptTotal
        {
            get { return total; }
            set { total = value; }
        }
        private List<Item> recieptCart = new List<Item>();
        public List<Item> RecieptCart
        {
            get { return recieptCart; }
            set { recieptCart = value; }
        }

        public Receipt(double sub, double tax, double total, List<Item> cart, string cashier, DateTime date)
        {
            RecieptSubTotal = sub;
            RecieptTax = tax;
            RecieptTotal = total;
            RecieptCart = cart;
            CashierName = cashier;
            DT = date;
            Order += 1;
        }


    }
}
