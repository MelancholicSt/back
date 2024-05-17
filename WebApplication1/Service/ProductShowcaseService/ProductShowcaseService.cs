using DuoVia.FuzzyStrings;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Product;
using WebApplication1.Service.ImageService;

namespace WebApplication1.ProductShowcaseService;

public class ProductShowcaseService : IProductShowcaseService
{
    private readonly DbContext _context;
    private ICloudImageService _imageService;

    public ProductShowcaseService(DbContext context, ICloudImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    public async Task AddProduct(Product? product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemoveProduct(ulong id)
    {
        Product? product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EditProductName(ulong id, string name)
    {
        Product? product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        product.Name = name;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EditProductDescription(ulong id, string description)
    {
        Product? product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> SetMainProductImage(ulong id, IFormFile file)
    {
        Product? product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        await _imageService.UploadFileAsync(file, "products");
        Image image = await _context.Images.FirstOrDefaultAsync(x => x.Name == file.Name);
        product.MainImage = image;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteProductImage(ulong id, string imageGuid)
    {
        Product? product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        Image? image = await _context.Images.FindAsync(imageGuid);
        if (image == null)
            return false;

        product.Info.Images.Remove(image);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<Product?> GetProductById(ulong id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> GetProductByName(string name)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<Product>> GetMostLikedProducts()
    {
        List<Product> allLikedProducts = new();
        
        await _context.FavouritesBuckets
            .Where(x => x.FavouriteProducts.Any()).Include(favouritesBucket => favouritesBucket.FavouriteProducts)
            .ForEachAsync(bucket => allLikedProducts.AddRange(bucket.FavouriteProducts));
        
        List<Product> mostLikedProducts = allLikedProducts
            .GroupBy(x => x)
            .OrderByDescending(group => group.Count())
            .Where(x => x.Count() > 6)
            .Select(x => x.Key)
            .ToList();
        
        return mostLikedProducts;
    }

    public async Task<List<Product>> GetAllProductsWhereNameMatches(string value)
    {
        return _context.Products.Where(product => product.Name.FuzzyMatch(value) > 60d).ToList();
    }
    
}