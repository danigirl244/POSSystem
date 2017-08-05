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

        //Add Item to 'Cart'
        public void addItem()
        {
           //cartList.add(item)  'item' will be the oject of items. 
        }

        //Delete Item From 'Cart'
        public void removeItem()
        {
            //cartList.Remove() Removes item from list.
        }

        //subtotal
        public double printSubTotal()
        {
            
            foreach (object item in cartList)
            {
                //subtotal = subtotal + item.price; 'item.price' is a placeholder for whatever the price section is called
            }

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
