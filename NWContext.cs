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

        public void EditCategory(Category updatedCategory) {
            Category category = this.Categories.Find(updatedCategory.CategoryId);
            category.CategoryName = updatedCategory.CategoryName;
            category.Description = updatedCategory.Description;
            this.SaveChanges();
        }

        public void EditProduct(Product updatedProduct) {
            Product product = this.Products.Find(updatedProduct.ProductId);
            product.ProductName = updatedProduct.ProductName;
            product.SupplierId = updatedProduct.SupplierId;
            product.CategoryId = updatedProduct.CategoryId;
            product.QuantityPerUnit = updatedProduct.QuantityPerUnit;
            product.UnitPrice = updatedProduct.UnitPrice;
            product.UnitsInStock = updatedProduct.UnitsInStock;
            product.UnitsOnOrder = updatedProduct.UnitsOnOrder;
            product.ReorderLevel = updatedProduct.ReorderLevel;
            product.Discontinued = updatedProduct.Discontinued;
            this.SaveChanges();
        }

        public void DeleteCategory(Category category) {
            this.Categories.Remove(category);
            this.SaveChanges();
        }

        public void DeleteProduct(Product product) {
            this.Products.Remove(product);
            this.SaveChanges();
        }
    }
}