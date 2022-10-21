using Best_Buy_Database.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace Best_Buy_Database
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public ProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM PRODUCTS");
        }

        public Product GetProductById(int id)
        {
            return _conn.QuerySingle<Product>("SELECT * FROM PRODUCTS WHERE PRODUCTID = @id", new { id = id });
        }

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE products SET Name = @Name, Price = @Price, StockLevel = @Stock WHERE ProductID = @id", new { Name = product.Name, Price = product.Price, Stock = product.StockLevel, id = product.ProductID });
        }
        public void InsertProduct(Product productToInsert)
        {
            _conn.Execute("INSERT INTO products (NAME, PRICE, CATEGORYID, ONSALE, STOCKLEVEL) VALUES (@name, @price, @categoryID, @onsale, @stock);", new { name = productToInsert.Name, price = productToInsert.Price, categoryID = productToInsert.CategoryID, onsale = productToInsert.OnSale, stock = productToInsert.StockLevel });
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _conn.Query<Category>("SELECT * FROM categories");
        }

        public Product AssignCategory()
        {
            var categoryList = GetAllCategories();
            var product = new Product();
            product.Categories = categoryList;
            return product;
        }

        public void DeleteProduct(Product product)
        {
            _conn.Execute("DELETE FROM REVIEWS WHERE ProductID = @id;", new { id = product.ProductID });
            _conn.Execute("DELETE FROM Sales WHERE ProductID = @id;", new { id = product.ProductID });
            _conn.Execute("DELETE FROM Products WHERE ProductID = @id;", new { id = product.ProductID });
        }
  
    }
}
