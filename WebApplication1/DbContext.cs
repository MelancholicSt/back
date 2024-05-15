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

namespace WebApplication1;

public sealed class DbContext : IdentityDbContext<Account>
{
    public DbSet<FavouritesBucket> FavouritesBuckets { get; set; }
    public DbSet<ClientBucket> ClientBuckets { get; set; }
    public DbSet<BucketCredentials> BucketCredentials { get; set; }
    public DbSet<Characteristics> Chars { get; set; }
    public DbSet<CharAttributeValue> CharKeys { get; set; }
    public DbSet<CharAttributeName> CharValues { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<AccountGeolocation> AccountGeolocations { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Category?> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> UserOrders { get; set; }
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
        builder.Entity<Product>()
            .HasMany(x => x.Images)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .IsRequired(false);
    }
}