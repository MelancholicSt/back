namespace WebApplication1.Data.dao.Client;

using Product;

public class FavouritesBucket
{
    public long Id { get; set; }

    /// <summary>
    /// Owner of this bucket
    /// </summary>
    public Client Client { get; set; } = null!;
    public string? ClientId { get; set; }

    /// <summary>
    /// Liked products
    /// </summary>
    public List<Product>? FavouriteProducts { get; } = new();
}