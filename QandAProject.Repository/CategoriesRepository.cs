using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QandADomainModels;

namespace QandAProjectRepository
{
    public interface ICategoriesRepository
    {
        void InsertCategory(Category cat);
        void UpdateCategory(Category cat);
        void DeleteCategory(int id);
        List<Category> GetCategories();
        List<Category> GetCategoriesbyId(int id);
    }
    public class CategoriesRepository : ICategoriesRepository
    {
        QandADatabaseDBContext db;
        public CategoriesRepository()
        {
            db = new QandADatabaseDBContext();
        }

        public void DeleteCategory(int id)
        {
            var category = db.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }

        }

        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        public List<Category> GetCategoriesbyId(int id)
        {
            return db.Categories.Where(x => x.CategoryId == id).ToList() ?? null;
        }

        public void InsertCategory(Category cat)
        {
            db.Categories.Add(cat);
            db.SaveChanges();
        }

        public void UpdateCategory(Category cat)
        {
            var category = db.Categories.Where(x => x.CategoryId == cat.CategoryId).FirstOrDefault();
            if (category != null)
            {
                category.CategoryName = cat.CategoryName;
                db.SaveChanges();
            }
        }
    }
}