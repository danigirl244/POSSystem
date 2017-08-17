using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestBackend
    {
        [TestMethod]
        public void ItemFieldsTest()
        {
            pointofsale_application.Item item = new pointofsale_application.Item();
            item.Name = "Tequila";
            item.SKU = 123456;
            item.Category = "Liquor";
            item.Price = 4.50;
            item.NumPurchased = 24;

            Console.Write(item.Name, item.SKU, item.Category, item.Price, item.NumPurchased);
        }
        [TestMethod]
        public void EditUserTest()
        {
            pointofsale_application.EditUser user = new pointofsale_application.EditUser();
            user.addEmp("Evan", "Admin", "Active");
            user.updateEmpAct(12345, "inActive");
            user.updateEmpRank(12345, "Basic");
        }
        [TestMethod]
        public void EditInventoryTest()
        {
            pointofsale_application.EditInventory inv = new pointofsale_application.EditInventory();
            inv.createItem(24, 10.25, "Jack", "Jack Bottle", "Whiskey");
            inv.deleteItem(111127);
            inv.editItem(111127, 20, 5.55, "Patron", "Tequila", "Tequila");

        }
        [TestMethod]
        public void HomePageTest()
        {
            pointofsale_application.HomePage home = new pointofsale_application.HomePage();
            home.SubTotal = 73.92;
            home.TaxTotal = 5.93;
            home.Total = 79.85;
            home.addItem();
            home.printSubTotal();
            home.printTotal();
            home.removeItem();


        }
        [TestMethod]
        public void AdminPageTest()
        {
            pointofsale_application.AdminPage admin = new pointofsale_application.AdminPage();
            admin.printSubTotal();
            admin.printTotal();
            admin.addItem();
            admin.removeItem();
            admin.fillCategoryColumn();
            admin.fillItemColumn();
            }
    }
}
