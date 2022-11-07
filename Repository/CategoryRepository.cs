using coursework.DbContexts;
using coursework.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coursework.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ProductContext _dbContext;
        public CategoryRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteCategory(int categoryId)
        {
            var category = _dbContext.Categories.Find(categoryId);
            _dbContext.Categories.Remove(category);
            Save();
        }
        public Category GetCategoryById(int categoryId)
        {
            var category = _dbContext.Categories.Find(categoryId);
           
            return category;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _dbContext.Categories.ToList();
        }
        public void InsertCategory(Category category)
        {
            _dbContext.Add(category);
            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            _dbContext.Entry(category).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }


    }
}
