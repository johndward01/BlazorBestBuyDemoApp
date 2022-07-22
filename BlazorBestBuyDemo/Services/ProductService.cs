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
        _connection.Execute("UPDATE products SET Name = @name, Price = @price WHERE ProductID = @id",
            new { name = product.Name, price = product.Price, id = product.ProductID });
    }

    public void InsertProduct(Product productToInsert)
    {
        _connection.Execute("INSERT INTO products (NAME, PRICE, CATEGORYID) VALUES (@name, @price, @categoryID);",
            new { name = productToInsert.Name, price = productToInsert.Price, categoryID = productToInsert.CategoryID });
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
