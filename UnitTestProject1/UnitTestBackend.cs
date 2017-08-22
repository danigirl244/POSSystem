using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pointofsale_application;

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
            List<String> employee = new List<string>();
            pointofsale_application.EditUserPopUp user = new pointofsale_application.EditUserPopUp();
            pointofsale_application.AddUser adduser = new pointofsale_application.AddUser();
            pointofsale_application.EditUser euser = new pointofsale_application.EditUser();
            //adduser.AddEmp("Evan", "admin", 1);
            //user.UpdateEmpAct(11119, 0);
            //user.UpdateEmpRank(11119, "basic");
            adduser.showID("Evan");
            adduser.SortEmpData();
            euser.getEmpInfo();
            euser.fillEmpColumn(employee);
        }
        [TestMethod]
        public void EditInventoryTest()
        {
            pointofsale_application.EditProduct inv = new pointofsale_application.EditProduct();
            pointofsale_application.AddProduct create = new pointofsale_application.AddProduct();
            pointofsale_application.EditInventory einv = new pointofsale_application.EditInventory();

            //create.CreateItem(24, 10.25, "Jack", "Jack Bottle", "Whiskey");
            //inv.EditItem(111137, 20, 5.55, "Patron", "Tequila", "Tequila");
            //inv.DeleteItem(111139);
            
        }
        [TestMethod]
        public void HomePageTest()
        {
            pointofsale_application.HomePage home = new pointofsale_application.HomePage("basic");
            home.SubTotal = 73.92;
            home.TaxTotal = 5.93;
            home.Total = 79.85;
            home.addItem("Jim Beam");
            home.printSubTotal();
            home.printTax();
            home.printTotal();
            home.removeItem("African Children");
            home.CreateReceipt();
            home.InitializeBeerList();
            home.InitializeBestSellersList();
            home.InitializeBourbonList();
            home.InitializeItemList();
            home.InitializeTequilaList();
            home.InitializeVodkaList();
            home.InitializeWhiskeyList();
            home.InitializeWineList();
            home.fillCategoryColumn();

        }
        [TestMethod]
        public void AdminPageTest()
        {
            pointofsale_application.AdminPage admin = new pointofsale_application.AdminPage("admin");
            admin.printSubTotal();
            admin.printTax();
            admin.printTotal();
            admin.addItem("Jim Beam");
            admin.removeItem("African Children");
            admin.CreateReceipt();
            admin.fillCategoryColumn();
            admin.InitializeBeerList();
            admin.InitializeBestSellersList();
            admin.InitializeBourbonList();
            admin.InitializeTequilaList();
            admin.InitializeVodkaList();
            admin.InitializeWhiskeyList();
            admin.InitializeWineList();
            admin.InitializeItemList();
        }
        [TestMethod]
        public void CashOutTest()
        {
            List<Item> cart = new List<Item>();
            pointofsale_application.CashOut cash = new pointofsale_application.CashOut(100, 8.10, 108.10, "admin", cart);
            cash.PrintChange();
            cash.Permission = "admin";
            cash.SubTotal = 100;
            cash.TaxTotal = 8.10;
            cash.Total = 108.10;
            cash.tranID = 1000;
        }
    }
}
