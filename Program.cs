using System;
using NLog.Web;
using System.IO;
using System.Linq;
using NorthwindConsole.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinalProject
{
    class Program
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string choose;
            logger.Info("Program Started");
            do {
                Console.WriteLine("1) Add to products table");
                Console.WriteLine("2) Edit record from products table");
                Console.WriteLine("3) Display all products");
                Console.WriteLine("4) Search for a product");
                Console.WriteLine("5) Add to categories table");
                Console.WriteLine("6) Edit record from categories table");
                Console.WriteLine("7) Display all categories");
                Console.WriteLine("8) Display all active products in each category");
                Console.WriteLine("9) Display all active products for specified category");
                Console.WriteLine("10) Delete record from products table");
                Console.WriteLine("11) Delete record from categories table");
                Console.WriteLine("\"q\" to quit");
                choose = Console.ReadLine();
                Console.Clear();
                logger.Info($"Option {choose} selected");
                if (choose == "1") 
                {
                    var db = new NWConsole_48_DABContext();
                    Product product = InputProduct(db, 0);
                    if (product != null) {
                       db.AddProduct(product);
                       //Console.WriteLine(product.SupplierId);
                        logger.Info($"Product Added - {product.ProductName}");
                    }
                }
                else if (choose == "2")
                {
                    var db = new NWConsole_48_DABContext();
                    bool loop = false;
                    Product product = null;
                    do {
                        Console.WriteLine("Choose a product for editing:");
                        ShowProducts(db);
                        try{
                            int productIdWrite = int.Parse(Console.ReadLine());
                            if (db.Products.Any(p => p.ProductId == productIdWrite)) {
                                product = db.Products.FirstOrDefault(p => p.ProductId == productIdWrite);
                                loop = false;
                            }
                            else {
                                Console.ForegroundColor = ConsoleColor.Red;
                                logger.Error("ProductId does not exist");
                                Console.ForegroundColor = ConsoleColor.White;
                                loop = true;
                            }
                        } catch (Exception) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            logger.Error("Whole Number was not entered");
                            Console.ForegroundColor = ConsoleColor.White;
                            loop = true;
                        }
                    } while (loop == true);
                    if (product != null) {
                        int productId = product.ProductId;
                        Product updatedProduct = InputProduct(db, productId);
                        if (product != null) {
                            updatedProduct.ProductId = product.ProductId;
                            db.EditProduct(updatedProduct);
                            logger.Info($"Product (id: {product.ProductId}) updated");
                        }
                    }
                }
                else if (choose == "3")
                {
                    
                }
                else if (choose == "4")
                {
                    
                }
                else if (choose == "5")
                {
                    
                }
                else if (choose == "6")
                {
                    
                }
                else if (choose == "7")
                {
                    
                }
                else if (choose == "8")
                {
                    
                }
                else if (choose == "9")
                {
                    
                }
                else if (choose == "10")
                {
                    
                }
                else if (choose == "11")
                {
                    
                }
            } while (choose.ToLower() != "q");
            logger.Info("Program Ended");
        }
        public static void ShowProducts(NWConsole_48_DABContext db){
            var products = db.Products.OrderBy(p => p.ProductId);
            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach(Product p in products) {
                Console.WriteLine($"{p.ProductId}: {p.ProductName}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ShowCategories(NWConsole_48_DABContext db){
            var categories = db.Categories.OrderBy(p => p.CategoryId);
            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach(Category c in categories) {
                Console.WriteLine($"{c.CategoryId}: {c.CategoryName}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ShowSuppliers(NWConsole_48_DABContext db){
            var supplier = db.Suppliers.OrderBy(p => p.SupplierId);
            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach(Supplier s in supplier) {
                Console.WriteLine($"{s.SupplierId}: {s.CompanyName}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static Product InputProduct(NWConsole_48_DABContext db, int productId){
            Product product = new Product();
            Product currentProduct = null;
            bool loop = false;
            if (productId != 0) {
                currentProduct = db.Products.FirstOrDefault(p => p.ProductId == productId);
                Console.WriteLine("Press enter to skip any fields");
            }
            Console.WriteLine("Enter Product name:");
            if (currentProduct != null) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Current Product Name: {currentProduct.ProductName} [press \"Enter\" to use]");
                Console.ForegroundColor = ConsoleColor.White;
            }
            string productName = Console.ReadLine();
            if (product.ProductName != "" || productId != 0) {
                if (currentProduct != null && productName == "") {
                    product.ProductName = currentProduct.ProductName;
                }
                else {
                    product.ProductName = productName;
                }
                do{
                    Console.WriteLine("Enter Supplier ID:");
                    ShowSuppliers(db);
                    if (currentProduct != null) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Current Product Supplier ID: {currentProduct.SupplierId} [press \"Enter\" to use]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    string supplierId = Console.ReadLine();
                    if (currentProduct != null && supplierId == "") {
                        product.SupplierId = currentProduct.SupplierId;
                    }
                    else {
                        try {
                            product.SupplierId = int.Parse(supplierId);
                            loop = false;
                        } catch (Exception) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            logger.Error("Whole Number was not entered");
                            Console.ForegroundColor = ConsoleColor.White;
                            loop = true;
                        }
                    }
                } while(loop == true);
                do{
                    Console.WriteLine("Enter Category ID:");
                    ShowCategories(db);
                    if (currentProduct != null) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Current Product Category ID: {currentProduct.CategoryId} [press \"Enter\" to use]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    string categoryId = Console.ReadLine();
                    if (currentProduct != null && categoryId == "") {
                        product.CategoryId = currentProduct.CategoryId;
                    }
                    else{
                        try {
                            product.CategoryId = int.Parse(categoryId);
                            loop = false;
                        } catch (Exception) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            logger.Error("Whole Number was not entered");
                            Console.ForegroundColor = ConsoleColor.White;
                            loop = true;
                        }
                    }
                } while(loop == true);
                Console.WriteLine("Enter Quantity per Unit:");
                if (currentProduct != null) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Current Product Quantity Per Unit: {currentProduct.QuantityPerUnit} [press \"Enter\" to use]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                string quantityPerUnit = Console.ReadLine();
                if (currentProduct != null && quantityPerUnit == "") {
                    product.QuantityPerUnit = currentProduct.QuantityPerUnit;
                }
                else {
                    product.QuantityPerUnit = quantityPerUnit;
                }
                do{
                    Console.WriteLine("Enter Unit Price:");
                    if (currentProduct != null) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Current Product Unit Price: ${currentProduct.UnitPrice} [press \"Enter\" to use]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    string unitPrice = Console.ReadLine();
                    if (currentProduct != null && unitPrice == "") {
                        product.UnitPrice = currentProduct.UnitPrice;
                    }
                    else {
                        try {
                            product.UnitPrice = decimal.Parse(unitPrice);
                            loop = false;
                        } catch (Exception) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            logger.Error("Number was not entered");
                            Console.ForegroundColor = ConsoleColor.White;
                            loop = true;
                        }
                    }
                } while(loop == true);
                do{
                    Console.WriteLine("Enter Units in Stock:");
                    if (currentProduct != null) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Current Product Units in Stock: {currentProduct.UnitsInStock} [press \"Enter\" to use]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    string unitsInStock = Console.ReadLine();
                    if (currentProduct != null && unitsInStock == "") {
                        product.UnitsInStock = currentProduct.UnitsInStock;
                    }
                    else {
                        try {
                            product.UnitsInStock = short.Parse(unitsInStock);
                            loop = false;
                        } catch (Exception) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            logger.Error("Whole Number was not entered");
                            Console.ForegroundColor = ConsoleColor.White;
                            loop = true;
                        }
                    }
                } while(loop == true);
                do{
                    Console.WriteLine("Enter Units on Order:");
                    if (currentProduct != null) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Current Product Units on Order: {currentProduct.UnitsOnOrder} [press \"Enter\" to use]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    string unitsOnOrder = Console.ReadLine();
                    if (currentProduct != null && unitsOnOrder == "") {
                        product.UnitsOnOrder = currentProduct.UnitsOnOrder;
                    }
                    else {
                        try {
                            product.UnitsOnOrder = short.Parse(unitsOnOrder);
                            loop = false;
                        } catch (Exception) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            logger.Error("Whole Number was not entered");
                            Console.ForegroundColor = ConsoleColor.White;
                            loop = true;
                        }
                    }
                } while(loop == true);
                do{
                    Console.WriteLine("Enter Recorder Level:");
                    if (currentProduct != null) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Current Product Recorder Level: {currentProduct.ReorderLevel} [press \"Enter\" to use]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    string recorderLevel = Console.ReadLine();
                    if (currentProduct != null && recorderLevel == "") {
                        product.ReorderLevel = currentProduct.ReorderLevel;
                    }
                    else {
                        try {
                            product.ReorderLevel = short.Parse(recorderLevel);
                            loop = false;
                        } catch (Exception) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            logger.Error("Whole Number was not entered");
                            Console.ForegroundColor = ConsoleColor.White;
                            loop = true;
                        }
                    }
                } while(loop == true);
                Console.WriteLine("Enter (1) if product is discontinued, or Enter (2) if product is not discontinued");
                if (currentProduct != null) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Current Product Discontinued Status: {currentProduct.Discontinued} [press \"Enter\" to use]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                string discontinued = Console.ReadLine();
                if (discontinued == "1") {
                    product.Discontinued = true;
                }
                else if (discontinued == "2") {
                    product.Discontinued = false;
                }
                else if (discontinued == "" && currentProduct != null) {
                    product.Discontinued = currentProduct.Discontinued;
                }
                else {
                    product.Discontinued = false;
                    logger.Info("Product Discontinued Defaulted to False");
                }

                ValidationContext context = new ValidationContext(product,null,null);
                List<ValidationResult> results = new List<ValidationResult>();

                var isValid = Validator.TryValidateObject(product, context, results, true);
                if (isValid)
                {
                    bool validForeign = db.Suppliers.Any(s => s.SupplierId == product.SupplierId) && db.Categories.Any(c => c.CategoryId == product.CategoryId);
                    bool supplierValid = db.Suppliers.Any(s => s.SupplierId == product.SupplierId);
                    bool categoryValid = db.Categories.Any(c => c.CategoryId == product.CategoryId);
                    if (db.Products.Any(p => p.ProductName == product.ProductName) && product.ProductName != currentProduct.ProductName)
                    {
                        isValid = false;
                        results.Add(new ValidationResult("Name exists", new string[] { "ProductName" }));
                    }
                    else if (validForeign == false)
                    {
                        isValid = false;
                        if (supplierValid == false) {
                            results.Add(new ValidationResult("SupplierID does not exists", new string[] { "SupplierId" }));}
                        if (categoryValid == false) {
                            results.Add(new ValidationResult("CategoryID does not exists", new string[] { "CategoryId" }));}
                    }
                    else
                    {
                        logger.Info("Validation passed");
                    }
                }
                if (!isValid)
                {
                    foreach (var result in results)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    return null;
                }
                return product;
            }
            else {
                logger.Info("Add to products have been canceled");
                return null;
            }
        }
    }
}
