using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Product;

namespace WebApplication1.ProductShowcaseService;

public interface IProductShowcaseService
{
    public Task AddProduct(Product? product);
    public Task<bool> RemoveProduct(ulong id);
    
    public Task<bool> EditProductName(ulong id, string name);
    public Task<bool> EditProductDescription(ulong id, string description);
    
    public Task<bool> AddProductImage(ulong id, IFormFile file);
    public Task<bool> DeleteProductImage(ulong id, string imageGuid);

    public Task<Product?> GetProductById(ulong id);
    public Task<Product> GetProductByName(string name);

    public Task<List<Product>> GetMostLikedProducts();

    public Task<List<Product>> GetAllProductsWhereNameMatches(string value);

    
}