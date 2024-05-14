namespace WebApplication1.Data.dao.Client;

using Product;

public class FavouritesBucket
{
    public long Id { get; set; }

    /// <summary>
    /// Owner of this bucket
    /// </summary>

    /// <summary>
    /// Liked products
    /// </summary>
    public List<Product>? FavouriteProducts { get; set; } = new();
}