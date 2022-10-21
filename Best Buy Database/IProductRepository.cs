using Best_Buy_Database.Models;
using System.Collections.Generic;

namespace Best_Buy_Database
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public Product GetProductById(int id);
        public void UpdateProduct(Product product);  
        public void InsertProduct(Product productToInsert);
        public IEnumerable<Category> GetAllCategories();
        public Product AssignCategory();
        public void DeleteProduct(Product product);
    }
}
