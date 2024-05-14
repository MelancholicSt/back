using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Client;
using WebApplication1.Data.dao.Order;
using WebApplication1.Data.dao.Product;
using WebApplication1.Data.dao.Product.Chars;
using WebApplication1.Data.dao.Product.Details;
using WebApplication1.Data.dao.Storage;

namespace WebApplication1;

public sealed class DbContext : IdentityDbContext<Account>
{

    public DbSet<FavouritesBucket> FavouritesBuckets { get; set; }
    public DbSet<ClientBucket> ClientBuckets { get; set; }
    public DbSet<BucketCredentials> BucketCredentials { get; set; }
    public DbSet<Characteristics> Chars { get; set; }
    public DbSet<CharKey> CharKeys { get; set; }
    public DbSet<CharValue> CharValues { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<AccountGeolocation> AccountGeolocations { get; set; }
    public DbSet<Organization> Organizations { get; set; }
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
    }
}