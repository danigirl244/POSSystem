using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pointofsale_application
{
    public class Item
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int sku;

        public int SKU
        {
            get { return sku; }
            set { sku = value; }
        }

        private string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private int numPurchased;

        public int NumPurchased
        {
            get { return numPurchased; }
            set { numPurchased = value; }
        }

    }
}
