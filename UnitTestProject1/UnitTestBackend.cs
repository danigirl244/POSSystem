﻿using System;
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
            pointofsale_application.EditUserPopUp user = new pointofsale_application.EditUserPopUp();
            pointofsale_application.AddUser adduser = new pointofsale_application.AddUser();
            //adduser.AddEmp("Evan", "admin", 1);
            //user.UpdateEmpAct(11119, 0);
            //user.UpdateEmpRank(11119, "basic");
            adduser.showID("Evan");
            adduser.SortEmpData();
        }
        [TestMethod]
        public void EditInventoryTest()
        {

            pointofsale_application.EditProduct inv = new pointofsale_application.EditProduct();
            pointofsale_application.AddProduct create = new pointofsale_application.AddProduct();

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
            home.printTotal();
            home.removeItem("African Children");


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

        }
    }
}
