using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApplication1.Data;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Clients;
using WebApplication1.Data.dao.Other;
using WebApplication1.Data.dao.Products;
using WebApplication1.Data.dao.Suppliers;
using WebApplication1.Data.Identity;

namespace WebApplication1;

public class DbContext : IdentityDbContext<Account>
{


    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    
    // Сущность, которая будет предоставляться в качестве предложенных товаров клиенту
    public DbSet<OfferedProduct> OfferedProducts { get; set; }
    public DbSet<ProductGallery> Galleries { get; set; }
    public DbSet<Bucket> Buckets { get; set; }
    public DbSet<Favourites> UserFavourites { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Measure> Measures { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Status> Statuses { get; set; }
    

    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
    }
}