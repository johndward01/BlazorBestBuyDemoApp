using BlazorBestBuyDemo.Data;
using Dapper;
using System.Data;

namespace BlazorBestBuyDemo.Services;

public class ProductService : IProductService
{
    private readonly IDbConnection _connection;

    public ProductService(IDbConnection connection)
    {
        _connection = connection;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _connection.Query<Product>("SELECT * FROM Products;");
    }

    public Product ViewProduct(int id)
    {
        return _connection.QuerySingle<Product>("SELECT * FROM Products WHERE ProductID = @id;",
            new { id });
    }

    public void UpdateProduct(Product product)
    {
        _connection.Execute("UPDATE products SET Name = @name, Price = @price, CategoryID = @categoryID, OnSale = @onSale, StockLevel = @stockLevel  WHERE ProductID = @id",
            new { name = product.Name, price = product.Price, categoryID = product.CategoryID, onSale = product.OnSale, stockLevel = product.StockLevel, id = product.ProductID });
    }

    public void InsertProduct(Product productToInsert)
    {
        _connection.Execute("INSERT INTO products (Name, Price, CategoryID, OnSale, StockLevel) VALUES (@name, @price, @categoryID, @onSale, @stockLevel);",
            new { name = productToInsert.Name, price = productToInsert.Price, categoryID = productToInsert.CategoryID, onSale = productToInsert.OnSale, stockLevel = productToInsert.StockLevel });
    }

    public IEnumerable<Category> GetCategories()
    {
        return _connection.Query<Category>("SELECT * FROM categories;");
    }

    public Product AssignCategory()
    {
        var categoryList = GetCategories();
        var product = new Product();
        product.Categories = categoryList;

        return product;
    }

    public void DeleteProduct(Product product)
    {
        _connection.Execute("DELETE FROM REVIEWS WHERE ProductID = @id;",
            new { id = product.ProductID });
        _connection.Execute("DELETE FROM Sales WHERE ProductID = @id;",
            new { id = product.ProductID });
        _connection.Execute("DELETE FROM Products WHERE ProductID = @id;",
            new { id = product.ProductID });
    }
}
