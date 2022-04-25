using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NorthwindConsole.Model;

namespace FinalProject 
{
    public class NWContext : DbContext
    {
        public DbSet<Category> Categories {get; set;}
        public DbSet<Product> Products {get; set;}

        public void AddCategory(Category category) {
            this.Categories.Add(category);
            this.SaveChanges();
        }

        public void AddProduct(Product product) {
            this.Products.Add(product);
            this.SaveChanges();
        }
    }
}