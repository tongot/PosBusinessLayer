

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
namespace AppDatabase
{
    public interface ICategory
    {
        void addCategory(Category category);
        void deleteCategory(int? id);
        void updateCategory(Category category);
        Category getCategoryById(int id);
        IEnumerable<Category> GetAllCategories( string name);
        List<Category> listCategory(string searchString);
        List<Product> getProductForCategory(int CustomerId);
        bool categoryExist(string name);
    }
}
