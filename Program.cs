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
                    Product product = InputProduct(db);
                    if (product != null) {
                       db.AddProduct(product);
                       //Console.WriteLine(product.SupplierId);
                        logger.Info($"Product Added - {product.ProductName}");
                    }
                }
                else if (choose == "2")
                {

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

        public static Product InputProduct(NWConsole_48_DABContext db){
            Product product = new Product();
            bool loop = false;
            Console.WriteLine("Enter Product name:");
            product.ProductName = Console.ReadLine();
            if (product.ProductName != "") {
                do{
                Console.WriteLine("Enter Supplier ID:");
                ShowSuppliers(db);
                try {
                product.SupplierId = int.Parse(Console.ReadLine());
                loop = false;
                } catch (Exception) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    logger.Error("Whole Number was not entered");
                    Console.ForegroundColor = ConsoleColor.White;
                    loop = true;
                }
                } while(loop == true);
                do{
                Console.WriteLine("Enter Category ID:");
                ShowCategories(db);
                try {
                product.CategoryId = int.Parse(Console.ReadLine());
                loop = false;
                } catch (Exception) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    logger.Error("Whole Number was not entered");
                    Console.ForegroundColor = ConsoleColor.White;
                    loop = true;
                }
                } while(loop == true);
                Console.WriteLine("Enter Quantity per Unit:");
                product.QuantityPerUnit = Console.ReadLine();
                do{
                Console.WriteLine("Enter Unit Price:");
                try {
                product.UnitPrice = decimal.Parse(Console.ReadLine());
                loop = false;
                } catch (Exception) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    logger.Error("Number was not entered");
                    Console.ForegroundColor = ConsoleColor.White;
                    loop = true;
                }
                } while(loop == true);
                do{
                Console.WriteLine("Enter Units in Stock:");
                try {
                product.UnitsInStock = short.Parse(Console.ReadLine());
                loop = false;
                } catch (Exception) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    logger.Error("Whole Number was not entered");
                    Console.ForegroundColor = ConsoleColor.White;
                    loop = true;
                }
                } while(loop == true);
                do{
                Console.WriteLine("Enter Units on Order:");
                try {
                product.UnitsOnOrder = short.Parse(Console.ReadLine());
                loop = false;
                } catch (Exception) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    logger.Error("Whole Number was not entered");
                    Console.ForegroundColor = ConsoleColor.White;
                    loop = true;
                }
                } while(loop == true);
                do{
                Console.WriteLine("Enter Record Level:");
                try {
                product.ReorderLevel = short.Parse(Console.ReadLine());
                loop = false;
                } catch (Exception) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    logger.Error("Whole Number was not entered");
                    Console.ForegroundColor = ConsoleColor.White;
                    loop = true;
                }
                } while(loop == true);
                Console.WriteLine("Enter (1) if product is discontinued, or Enter (2) if product is not discontinued");
                string discontinued = Console.ReadLine();
                if (discontinued == "1") {
                    product.Discontinued = true;
                }
                else if (discontinued == "2") {
                    product.Discontinued = false;
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
                    if (db.Products.Any(p => p.ProductName == product.ProductName))
                    {
                        isValid = false;
                        results.Add(new ValidationResult("Name exists", new string[] { "ProductName" }));
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
