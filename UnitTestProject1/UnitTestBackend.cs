using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pointofsale_application;
using System.Data.SqlClient;
using static pointofsale_application.Login;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestBackend
    {
        [TestInitialize]
        public void CahierNameTest()
        {
            StaticVars.CashierName = "Evan Test";
        }
       

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
            //adduser.AddEmp("Evan The Great", "admin", 1);
            //user.UpdateEmpAct("11119", 1);
            //user.UpdateEmpRank("11119", "Admin");
            adduser.ShowID("Evan");
            adduser.SortEmpData();
            euser.GetEmpInfo();
            //euser.FillEmpColumn(employee);
        }
        [TestMethod]
        public void EditInventoryTest()
        {
            Item product = new Item();
            product.Category = "Beer";
            product.Name = "Blue Moon";
            List<Item> inventory = new List<Item>();
            inventory.Add(product);
            pointofsale_application.EditProduct inv = new pointofsale_application.EditProduct(product);
            pointofsale_application.AddProduct create = new pointofsale_application.AddProduct();
            pointofsale_application.EditInventory einv = new pointofsale_application.EditInventory(inventory);

            //create.CreateItem("24", "10.25", "Jack", "Jack Bottle", "Whiskey");
            //inv.EditItem("31", "21000", "5.55", "Patron", "Tequila", "Tequila");
            //inv.DeleteItem(24);

        }
        [TestMethod]
        public void HomePageTest()
        {
            List<Item> list = new List<Item>();
            pointofsale_application.HomePage home = new pointofsale_application.HomePage("basic");
            home.SubTotal = 73.92;
            home.TaxTotal = 5.93;
            home.Total = 79.85;

            home.AddItem("Jim Beam");
            home.PrintSubTotal();
            home.PrintTotal();
            home.RemoveItem("African Children");


            home.AddItem("Jim Beam");
            home.PrintSubTotal();
            home.PrintTax();
            home.PrintTotal();
            home.RemoveItem("African Children");
            home.CreateReceipt();
            home.InitializeBestSellersList();
            home.InitializeItemList();
            home.InitializeDrinkList("Wine", list);
            home.FillCategoryColumn();

        }
        [TestMethod]
        public void AdminPageTest()
        {
            List<Item> list = new List<Item>();
            pointofsale_application.AdminPage admin = new pointofsale_application.AdminPage("admin");
            admin.PrintSubTotal();
            admin.PrintTax();
            admin.PrintTotal();
            admin.AddItem("Jim Beam");
            admin.RemoveItem("African Children");
            admin.CreateReceipt();
            admin.FillCategoryColumn();
            admin.InitializeBestSellersList();
            admin.InitializeDrinkList("Wine", list);
            admin.InitializeItemList();
        }
        [TestMethod]
        public void CashOutTest()
        {
            Item test = new Item();
            test.Category = "Wine";
            List<Item> cart = new List<Item>();
            List<Item> inv = new List<Item>();
            cart.Add(test);
            pointofsale_application.CashOut cash = new pointofsale_application.CashOut(100.00, 8.10, 108.10, "admin", cart, inv);
            cash.PrintChange();
            cash.Permission = "admin";
            cash.SubTotal = 100;
            cash.TaxTotal = 8.10;
            cash.Total = 108.10;
            cash.TxID = 1000;
            cash.SubmitTx();
        }
        [TestMethod]
        public void ReportsTest()
        {
            pointofsale_application.Reports report = new pointofsale_application.Reports();
            report.PrintTill();
        }
        [TestMethod] //DB Test Case 003
        public void UserIDQquery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "11119";
            SqlCommand findUserID = new SqlCommand("Select UserID From Users Where EmployeeName = 'Evan'", db.AccessDB());
            Assert.AreEqual(findUserID.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 004
        public void employeeNameQuery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "Evan";
            SqlCommand findEmpName = new SqlCommand("Select EmployeeName From Users Where UserID = '11119'", db.AccessDB());
            Assert.AreEqual(findEmpName.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 005
        public void permissionsQuery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "basic";
            SqlCommand findPermissions = new SqlCommand("Select Permissions From Users Where UserID = '11119'", db.AccessDB());
            Assert.AreEqual(findPermissions.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 006
        public void statusQuery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "True";
            SqlCommand findStatus = new SqlCommand("Select isActive From Users Where UserID = '11119'", db.AccessDB());
            Assert.AreEqual(findStatus.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 010
        public void cashTenderQuery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "3.9900";
            SqlCommand findPrice = new SqlCommand("Select Price From Tx Where Tender = 'Cash' AND TxID = '5'", db.AccessDB());
            Assert.AreEqual(findPrice.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 017
        public void sessionInsert()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "11119";
            SqlCommand insert = new SqlCommand("INSERT INTO SessionLoginHistory(UserID, LastLogin, LastLogout) VALUES('11119', '20170804 1:40:10 AM', '20170805 1:45:10 AM')", db.AccessDB());
            SqlCommand findSessionID = new SqlCommand("Select UserID from SessionLoginHistory where UserID = '11119'", db.AccessDB());
            Assert.AreEqual(findSessionID.ExecuteScalar().ToString(), actual);
            SqlCommand delete = new SqlCommand("DELETE FROM SessionLoginHistory WHERE UserID = '11119'", db.AccessDB());

        }
        [TestMethod] //DB Test Case 018 & 19
        public void sessionUpdate()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "20170805 9:50:10 AM";
            SqlCommand insert = new SqlCommand("INSERT INTO SessionLoginHistory(UserID, LastLogin, LastLogout) VALUES('11119', '20170804 1:40:10 AM', '20170805 1:45:10 AM')", db.AccessDB());
            SqlCommand update = new SqlCommand("UPDATE SessionLoginHistory SET LastLogout = '20170805 9:50:10 AM' WHERE UserID = '11119'", db.AccessDB());
            SqlCommand delete = new SqlCommand("DELETE FROM SessionLoginHistory WHERE UserID = '11119'", db.AccessDB());
        }
        [TestMethod] //DB Test Case 021
        public void skuQuery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "3";
            SqlCommand findSKU = new SqlCommand("Select SKU FROM Inventory WHERE Name = 'H3'", db.AccessDB());
            Assert.AreEqual(findSKU.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 022
        public void qtyQuery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "-29";
            SqlCommand findQty = new SqlCommand("Select QtyOnHand FROM Inventory WHERE Name = 'H3'", db.AccessDB());
            Assert.AreEqual(findQty.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 023
        public void priceQuery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "35.0000";
            SqlCommand findPrice = new SqlCommand("Select Price FROM Inventory WHERE Name = 'H3'", db.AccessDB());
            Assert.AreEqual(findPrice.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 024
        public void itemNameQuery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "H3";
            SqlCommand findItemName = new SqlCommand("Select Name FROM Inventory WHERE Price = '35.00'", db.AccessDB());
            Assert.AreEqual(findItemName.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 025
        public void itemDescQuery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "a sophisticated fellow or gals wine. ";
            SqlCommand findItemDesc = new SqlCommand("Select [Desc] FROM Inventory WHERE Name = 'H3'", db.AccessDB());
            Assert.AreEqual(findItemDesc.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 026
        public void itemCatQuery()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "Wine";
            SqlCommand findItemDesc = new SqlCommand("Select Category FROM Inventory WHERE Name = 'H3'", db.AccessDB());
            Assert.AreEqual(findItemDesc.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 027
        public void insertInventory()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "NewItem";
            SqlCommand insert = new SqlCommand("INSERT INTO Inventory(Name, Price, [Desc], Category, QtyOnHand, NumPurchased) VALUES('NewItem', '2.20', 'A new item', 'Vodka', '25', '80')", db.AccessDB());
            SqlCommand findItem = new SqlCommand("Select Name FROM Inventory WHERE Name = 'NewItem'", db.AccessDB());
            Assert.AreEqual(findItem.ExecuteScalar().ToString(), actual);
            SqlCommand delete = new SqlCommand("DELETE FROM Inventory WHERE Name = 'NewItem'", db.AccessDB());
        }
        [TestMethod] //DB Test Case 028 & 029
        public void updateInventory()
        {
            DatabaseAccess db = new DatabaseAccess();
            SqlCommand insert = new SqlCommand("INSERT INTO Inventory(Name, Price, [Desc], Category, QtyOnHand, NumPurchased) VALUES('NewItem', '2.20', 'A new item', 'Vodka', '25', '80')", db.AccessDB());
            SqlCommand update = new SqlCommand("UPDATE Inventory SET Price = '2.50' WHERE Name = 'NewItem'", db.AccessDB());
            SqlCommand delete = new SqlCommand("DELETE FROM Inventory WHERE Name = 'NewItem'", db.AccessDB());
        }
        [TestMethod] //DB Test Case 030
        public void insertUsers()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "Homeboy";
            SqlCommand insert = new SqlCommand("INSERT INTO Users(EmployeeName, Permissions, isActive) VALUES('Homeboy', 'basic', '0')", db.AccessDB());
            SqlCommand findUser = new SqlCommand("Select EmployeeName FROM Users WHERE EmployeeName = 'Homeboy'", db.AccessDB());
            Assert.AreEqual(findUser.ExecuteScalar().ToString(), actual);
            SqlCommand delete = new SqlCommand("DELETE FROM Users WHERE EmployeeName = 'Homeboy'", db.AccessDB());
        }
        [TestMethod] //DB Test Case 031 & 032
        public void updateUsers()
        {
            DatabaseAccess db = new DatabaseAccess();
            SqlCommand insert = new SqlCommand("INSERT INTO Users(EmployeeName, Permissions, isActive) VALUES('Homeboy', 'basic', '0')", db.AccessDB());
            SqlCommand update = new SqlCommand("Update Users Set EmployeeName = 'notHomeboy' WHERE EmployeeName = 'Homeboy'", db.AccessDB());
            SqlCommand delete = new SqlCommand("DELETE FROM Users WHERE EmployeeName = 'Homeboy'", db.AccessDB());
        }
        [TestMethod] //DB Test Case 033
        public void insertTx()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "2.66";
            SqlCommand insert = new SqlCommand("INSERT INTO Tx(Price, Qty, [DateTime], UserID, Subtotal, Total, Tender) VALUES('2.66','1','2017-08-22 19:27:36.890','11112','10.48','11.21','cash')", db.AccessDB());
            SqlCommand findTx = new SqlCommand("Select Price FROM Tx WHERE Price = '2.66'", db.AccessDB());
            Assert.AreEqual(findTx.ExecuteScalar().ToString(), actual);
            SqlCommand delete = new SqlCommand("DELETE FROM Tx WHERE Price = '2.66'", db.AccessDB());
        }
        //TC 027
        [TestMethod] //DB Test Case 034 & 35
        public void txUpdate()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "1000";
            SqlCommand insert = new SqlCommand("INSERT INTO Tx(TxID, SKU, Price, Qty, [DateTime], UserID, Subtotal, Total, Tender) VALUES(29, 13, 2.00, 10, '20170805 1:50:10 AM', 11119, 20, 22, 'cash')", db.AccessDB());
            SqlCommand update = new SqlCommand("UPDATE Tx SET Qty = '1000' WHERE TxID = '5'", db.AccessDB());
            SqlCommand delete = new SqlCommand("DELETE FROM Tx WHERE TxID = '29'", db.AccessDB());
        }
        [TestMethod] //Test Case 36
        public void txPrimaryKey()
        {
            DatabaseAccess db = new DatabaseAccess();
            SqlCommand insert = new SqlCommand("INSERT INTO Tx(TxID, SKU, Price, Qty, [DateTime], UserID, Subtotal, Total, Tender) VALUES('1', '5', '2.00', '10', '20170805 1:50:10 AM', '11119', '20', '22', 'cash')", db.AccessDB());
        }
        [TestMethod] //Test Case 37
        public void UsersPrimaryKey()
        {
            DatabaseAccess db = new DatabaseAccess();
            SqlCommand insert = new SqlCommand("Insert Into Users(UserID) Values(11119)", db.AccessDB());
        }
        [TestMethod] //Test case 38 & 39
        public void SessionPrimaryKey()
        {
            DatabaseAccess db = new DatabaseAccess();
            SqlCommand insert = new SqlCommand("INSERT INTO SessionLoginHistory(UserID, LastLogin, LastLogout) VALUES('11119', '20170804 1:40:10 AM', '20170805 1:45:10 AM')", db.AccessDB());
            SqlCommand insert2 = new SqlCommand("INSERT INTO SessionLoginHistory(UserID, LastLogin, LastLogout) VALUES('11119', '20170804 1:40:10 AM', '20170805 1:45:10 AM')", db.AccessDB());

        }
        [TestMethod] //DB Test Case 040
        public void ValidateSessionLogin()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "SessionID";
            SqlCommand sessionID = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('SessionLoginHistory') AND name = 'SessionID'", db.AccessDB());
            Assert.AreEqual(sessionID.ExecuteScalar().ToString(), actual);

            actual = "UserID";
            SqlCommand userID = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('SessionLoginHistory') AND name = 'UserID'", db.AccessDB());
            Assert.AreEqual(userID.ExecuteScalar().ToString(), actual);

            actual = "LastLogin";
            SqlCommand lastLogin = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('SessionLoginHistory') AND name = 'LastLogin'", db.AccessDB());
            Assert.AreEqual(lastLogin.ExecuteScalar().ToString(), actual);

            actual = "LastLogout";
            SqlCommand lastLogout = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('SessionLoginHistory') AND name = 'LastLogout'", db.AccessDB());
            Assert.AreEqual(lastLogout.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 041
        public void ValidateUsers()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "UserID";
            SqlCommand userID = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'UserID'", db.AccessDB());
            Assert.AreEqual(userID.ExecuteScalar().ToString(), actual);

            actual = "EmployeeName";
            SqlCommand employeeName = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'EmployeeName'", db.AccessDB());
            Assert.AreEqual(employeeName.ExecuteScalar().ToString(), actual);

            actual = "Permissions";
            SqlCommand permissions = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'Permissions'", db.AccessDB());
            Assert.AreEqual(permissions.ExecuteScalar().ToString(), actual);

            actual = "isActive";
            SqlCommand isActive = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'isActive'", db.AccessDB());
            Assert.AreEqual(isActive.ExecuteScalar().ToString(), actual);
        }
        [TestMethod] //DB Test Case 042
        public void ValidateTX()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "TxID";
            SqlCommand txID = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Tx') AND name = 'TxID'", db.AccessDB());
            Assert.AreEqual(txID.ExecuteScalar().ToString(), actual);

            actual = "SKU";
            SqlCommand sku = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Tx') AND name = 'SKU'", db.AccessDB());
            Assert.AreEqual(sku.ExecuteScalar().ToString(), actual);

            actual = "Price";
            SqlCommand price = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Tx') AND name = 'Price'", db.AccessDB());
            Assert.AreEqual(price.ExecuteScalar().ToString(), actual);

            actual = "Qty";
            SqlCommand qty = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Tx') AND name = 'Qty'", db.AccessDB());
            Assert.AreEqual(qty.ExecuteScalar().ToString(), actual);

            actual = "DateTime";
            SqlCommand dateTime = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Tx') AND name = 'DateTime'", db.AccessDB());
            Assert.AreEqual(dateTime.ExecuteScalar().ToString(), actual);

            actual = "UserID";
            SqlCommand userID = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Tx') AND name = 'UserID'", db.AccessDB());
            Assert.AreEqual(userID.ExecuteScalar().ToString(), actual);

            actual = "Subtotal";
            SqlCommand subtotal = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Tx') AND name = 'Subtotal'", db.AccessDB());
            Assert.AreEqual(subtotal.ExecuteScalar().ToString(), actual);

            actual = "Total";
            SqlCommand total = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Tx') AND name = 'Total'", db.AccessDB());
            Assert.AreEqual(total.ExecuteScalar().ToString(), actual);

            actual = "Tender";
            SqlCommand tender = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Tx') AND name = 'Tender'", db.AccessDB());
            Assert.AreEqual(tender.ExecuteScalar().ToString(), actual);

        }
        [TestMethod] //DB Test Case 043
        public void ValidateInventory()
        {
            DatabaseAccess db = new DatabaseAccess();
            String actual = "SKU";
            SqlCommand sku = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Inventory') AND name = 'SKU'", db.AccessDB());
            Assert.AreEqual(sku.ExecuteScalar().ToString(), actual);

            actual = "Name";
            SqlCommand name = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Inventory') AND name = 'Name'", db.AccessDB());
            Assert.AreEqual(name.ExecuteScalar().ToString(), actual);

            actual = "Price";
            SqlCommand price = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Inventory') AND name = 'Price'", db.AccessDB());
            Assert.AreEqual(price.ExecuteScalar().ToString(), actual);

            actual = "Desc";
            SqlCommand desc = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Inventory') AND name = 'Desc'", db.AccessDB());
            Assert.AreEqual(desc.ExecuteScalar().ToString(), actual);

            actual = "Category";
            SqlCommand cat = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Inventory') AND name = 'Category'", db.AccessDB());
            Assert.AreEqual(cat.ExecuteScalar().ToString(), actual);

            actual = "QtyOnHand";
            SqlCommand onHand = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Inventory') AND name = 'QtyOnHand'", db.AccessDB());
            Assert.AreEqual(onHand.ExecuteScalar().ToString(), actual);

            actual = "NumPurchased";
            SqlCommand purchased = new SqlCommand("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('Inventory') AND name = 'NumPurchased'", db.AccessDB());
            Assert.AreEqual(purchased.ExecuteScalar().ToString(), actual);
        }

    }
}
