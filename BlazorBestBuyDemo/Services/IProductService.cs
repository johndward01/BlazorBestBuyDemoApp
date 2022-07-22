using BlazorBestBuyDemo.Data;

namespace BlazorBestBuyDemo.Services;

public interface IProductService
{
    // Create
    public void InsertProduct(Product productToInsert);

    // Read
    public IEnumerable<Product> GetAllProducts();
    public Product ViewProduct(int id);

    // Update
    public void UpdateProduct(Product product);

    // Delete
    public void DeleteProduct(Product product);

    public IEnumerable<Category> GetCategories();
    public Product AssignCategory();
}
