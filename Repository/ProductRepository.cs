using coursework.DbContexts;
using coursework.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace coursework.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _dbContext;
        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteProduct(int productId)
        {
            var product = _dbContext.Products.Find(productId);
            _dbContext.Products.Remove(product);
            Save();
        }
        public Product GetProductById(int productId)
        {
            var prod = _dbContext.Products.Find(productId);
            _dbContext.Entry(prod).Reference(s => s.ProductCategory).Load();
            return prod;
        }
        public IEnumerable<Product> GetProducts()
        {
            //return _dbContext.Products.ToList();
            return _dbContext.Products.Include(s => s.ProductCategory).ToList();
            
        }
        public void InsertProduct(Product product)
        {
            var c = _dbContext.Categories.Local.SingleOrDefault(c => c.Id == product.ProductCategory.Id);
            if (c == null)
            {
                c = new Category { Id = product.ProductCategory.Id };
                _dbContext.Categories.Attach(c);
            }
            var p = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price=product.Price,
                ProductCategory=c

            };

            _dbContext.Add(p);



            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            var c = _dbContext.Categories.Local.SingleOrDefault(c => c.Id == product.ProductCategory.Id);
            if (c == null)
            {
                c = new Category { Id = product.ProductCategory.Id };
                _dbContext.Categories.Attach(c);
            }
            var p = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductCategory = c

            };

            _dbContext.Entry(p).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }

        //Hello
    }
}
