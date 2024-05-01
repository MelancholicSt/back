﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApplication1.Data.dao;

namespace WebApplication1;

public class DbContext : IdentityDbContext<Account>
{

    public DbSet<Offer> Offers { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<ClientOffer> ClientOffers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ClientOrder> UserOrders { get; set; }
    public DbSet<Measure> Measures { get; set; }
    

    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Account>(x =>
        {
            x.HasOne(me => me.Offer).WithOne(c => c.Owner).HasForeignKey<Offer>(c => c.OwnerId).IsRequired(false);
        });
        builder.Entity<ClientOffer>().HasNoKey();
    }
}