using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pointofsale_application
{
    class Transaction
    {
        private static double taxPercentage = .0685;
        private double subtotal;
        public double SubTotal
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
        private int ID;

        List<Object> cartList = new List<Object>();
        



        public Transaction(int userID /*, string paymentType */)
        {
            ID = userID;
        }

        //Add Item to Cart
        public void addItem()
        {
           
        }

        //Delete Item From Cart
        public void removeItem()
        {

        }

        //subtotal
        public double printSubTotal()
        {

            return subtotal;
        }

        //Final Price with Tax
        public double printTotal()
        {

            total = subtotal * taxPercentage;

            return total;
        }
        

        
    }
}
