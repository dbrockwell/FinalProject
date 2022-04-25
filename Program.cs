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
        }
    }
}
