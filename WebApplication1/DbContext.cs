using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Client;
using WebApplication1.Data.dao.Identity;
using WebApplication1.Data.dao.Order;
using WebApplication1.Data.dao.Product;
using WebApplication1.Data.dao.Product.Chars;
using WebApplication1.Data.dao.Product.Details;
using WebApplication1.Data.dao.Supplier;

namespace WebApplication1;

public sealed class DbContext : IdentityDbContext<Account>
{
    public DbSet<Offer> Offers { get; set; }
    public DbSet<FavouritesBucket> FavouritesBuckets { get; set; }
    public DbSet<ClientBucket> ClientBuckets { get; set; }
    public DbSet<BucketCredentials> BucketCredentials { get; set; }
    public DbSet<Characteristics> Chars { get; set; }
    public DbSet<CharAttributeValue> CharKeys { get; set; }
    public DbSet<CharAttributeName> CharValues { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<DeliveryInfo> DeliveryInfos { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<AccountGeolocation> AccountGeolocations { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSellInfo> SellInfos { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Measure> Measures { get; set; }
    public DbSet<Image> Images { get; set; }


    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Client>().HasOne(x => x.ClientBucket).WithOne(x => x.Client)
            .HasForeignKey<ClientBucket>(x => x.ClientId);
        builder.Entity<Client>().HasOne(x => x.FavouritesBucket).WithOne(x => x.Client)
            .HasForeignKey<FavouritesBucket>(x => x.ClientId);


        builder.Entity<ProductInfo>()
            .HasMany(x => x.Images)
            .WithOne(x => x.ProductInfo)
            .HasForeignKey(x => x.ProductInfoId)
            .IsRequired(false);

        builder.Entity<Supplier>()
            .HasMany(x => x.AvailableMaterials)
            .WithMany(x => x.Suppliers);
        builder.Entity<Supplier>()
            .HasMany(x => x.PerformingOrders)
            .WithOne(x => x.Supplier)
            .HasForeignKey(x => x.SupplierId);


        builder.Entity<FavouritesBucket>()
            .HasMany(x => x.FavouriteProducts)
            .WithMany(x => x.FavouritesBuckets);
        builder.Entity<ClientBucket>()
            .HasMany(x => x.Products)
            .WithMany(x => x.ClientBuckets);
    }
}