
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;

namespace AppDatabase
{
    /// <summary>
    /// category data app
    /// </summary>
    public class CategoryApp : ICategory
    {
        ApplicationContext _context = new ApplicationContext();
        public void addCategory(Category category)
        {
            _context.categories.Add(category);
             _context.SaveChanges();
        }

        public void deleteCategory(int? id)
        {
            if(id!=null)
            {
                Category ct = _context.categories.Find(id);
                 _context.categories.Remove(ct);
                 _context.SaveChanges();
            }
            
        }
        public Category getCategoryById(int id)
        {
            return _context.categories.Find(id);
        }

        public List<Product> getProductForCategory(int categoryId)
        {
            return _context.products.Where(x => x.CategoryId == categoryId).ToList();
        }

        public List<Category> listCategory(string searchString)
        {
            if (searchString != null)
            {
                    return _context.categories.Where(x => x.Name.Contains(searchString)).ToList();
            }
            return _context.categories.ToList();
        }

        public async Task updateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();  
        }

        public IEnumerable<Category> GetAllCategories(string name)
        {
            IEnumerable<Category> categories = (_context.categories.Where(x=>x.Name.Contains(name)||name==null));
            return categories;
        }

        void ICategory.updateCategory(Category category)
        {
            _context.Set<Category>().AddOrUpdate(category);
            _context.SaveChanges();
        }
        public bool categoryExist(string name)
        {
            Category c = _context.categories.Where(x => x.Name == name).FirstOrDefault();
            if (c == null)
            { return false; }
            return true;
        }
 
    }
}
