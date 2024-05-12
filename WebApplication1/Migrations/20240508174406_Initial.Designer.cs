﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(DbContext))]
    [Migration("20240508174406_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<ulong>("OrganizationId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("OrganizationId");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Account");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WebApplication1.Data.dao.AccountGeolocation", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("LocalAddress")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("AccountGeolocations");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Client.ClientBucket", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("ClientBuckets");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Client.FavouritesBucket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("FavouritesBuckets");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Client.OrderProduct", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<ulong>("BucketId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("ProductId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("Quantity")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("BucketId");

                    b.ToTable("BucketProducts");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.ImageFile", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Guid")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ImageFiles");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Order.Order", b =>
                {
                    b.Property<ulong>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("OrderId"));

                    b.Property<string>("ClientId")
                        .HasColumnType("varchar(255)");

                    b.Property<uint>("StatusId")
                        .HasColumnType("int unsigned");

                    b.Property<string>("SupplierId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("OrderId");

                    b.HasIndex("ClientId");

                    b.HasIndex("StatusId");

                    b.HasIndex("SupplierId");

                    b.ToTable("UserOrders");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Order.Status", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Organization", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<int>("INN")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.CharKey", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CharKeys");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.CharValue", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<ulong>("KeyId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("KeyId");

                    b.ToTable("CharValues");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.Characteristics", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<ulong>("KeyId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("ProductInfoId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("ValueId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("KeyId");

                    b.HasIndex("ProductInfoId")
                        .IsUnique();

                    b.HasIndex("ValueId");

                    b.ToTable("Chars");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.Details.Material", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<uint>("MeasureId")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SupplierId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("MeasureId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.Details.Measure", b =>
                {
                    b.Property<uint>("MeasureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("MeasureId"));

                    b.Property<string>("MeasureName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MeasureId");

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.Product", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<ulong?>("ClientBucketId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("FavouritesBucketId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<ulong?>("OrderId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("SupplierId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ClientBucketId");

                    b.HasIndex("FavouritesBucketId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.ProductInfo", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<ulong?>("MaterialId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("ProductId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductInfo");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Storage.BucketCredentials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("KeyIdentifier")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BucketCredentials");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Client.Client", b =>
                {
                    b.HasBaseType("WebApplication1.Account");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Supplier", b =>
                {
                    b.HasBaseType("WebApplication1.Account");

                    b.Property<float>("Rating")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("Supplier");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebApplication1.Account", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebApplication1.Account", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Account", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebApplication1.Account", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Account", b =>
                {
                    b.HasOne("WebApplication1.Data.dao.Organization", "Organization")
                        .WithMany("Accounts")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.AccountGeolocation", b =>
                {
                    b.HasOne("WebApplication1.Account", "Account")
                        .WithOne("Geolocation")
                        .HasForeignKey("WebApplication1.Data.dao.AccountGeolocation", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Client.ClientBucket", b =>
                {
                    b.HasOne("WebApplication1.Data.dao.Client.Client", "Client")
                        .WithOne("ClientBucket")
                        .HasForeignKey("WebApplication1.Data.dao.Client.ClientBucket", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Client.FavouritesBucket", b =>
                {
                    b.HasOne("WebApplication1.Data.dao.Client.Client", "Client")
                        .WithOne("FavouritesBucket")
                        .HasForeignKey("WebApplication1.Data.dao.Client.FavouritesBucket", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Client.OrderProduct", b =>
                {
                    b.HasOne("WebApplication1.Data.dao.Client.ClientBucket", "Bucket")
                        .WithMany()
                        .HasForeignKey("BucketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bucket");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Order.Order", b =>
                {
                    b.HasOne("WebApplication1.Data.dao.Client.Client", null)
                        .WithMany("Orders")
                        .HasForeignKey("ClientId");

                    b.HasOne("WebApplication1.Data.dao.Order.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Data.dao.Supplier", null)
                        .WithMany("Orders")
                        .HasForeignKey("SupplierId");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.CharValue", b =>
                {
                    b.HasOne("WebApplication1.Data.dao.Product.CharKey", "Key")
                        .WithMany("Values")
                        .HasForeignKey("KeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Key");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.Characteristics", b =>
                {
                    b.HasOne("WebApplication1.Data.dao.Product.CharKey", "Key")
                        .WithMany()
                        .HasForeignKey("KeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Data.dao.Product.ProductInfo", "ProductInfo")
                        .WithOne("Characteristics")
                        .HasForeignKey("WebApplication1.Data.dao.Product.Characteristics", "ProductInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Data.dao.Product.CharValue", "Value")
                        .WithMany()
                        .HasForeignKey("ValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Key");

                    b.Navigation("ProductInfo");

                    b.Navigation("Value");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.Details.Material", b =>
                {
                    b.HasOne("WebApplication1.Data.dao.Product.Details.Measure", "Measure")
                        .WithMany()
                        .HasForeignKey("MeasureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Data.dao.Supplier", null)
                        .WithMany("AvailableMaterials")
                        .HasForeignKey("SupplierId");

                    b.Navigation("Measure");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.Product", b =>
                {
                    b.HasOne("WebApplication1.Data.dao.Client.ClientBucket", null)
                        .WithMany("Products")
                        .HasForeignKey("ClientBucketId");

                    b.HasOne("WebApplication1.Data.dao.Client.FavouritesBucket", null)
                        .WithMany("FavouriteProducts")
                        .HasForeignKey("FavouritesBucketId");

                    b.HasOne("WebApplication1.Data.dao.Order.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("WebApplication1.Data.dao.Supplier", null)
                        .WithMany("SellingProducts")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.ProductInfo", b =>
                {
                    b.HasOne("WebApplication1.Data.dao.Product.Details.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId");

                    b.HasOne("WebApplication1.Data.dao.Product.Product", "Product")
                        .WithOne("Info")
                        .HasForeignKey("WebApplication1.Data.dao.Product.ProductInfo", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApplication1.Account", b =>
                {
                    b.Navigation("Geolocation");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Client.ClientBucket", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Client.FavouritesBucket", b =>
                {
                    b.Navigation("FavouriteProducts");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Order.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Order.Status", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Organization", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.CharKey", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.Product", b =>
                {
                    b.Navigation("Info");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Product.ProductInfo", b =>
                {
                    b.Navigation("Characteristics");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Client.Client", b =>
                {
                    b.Navigation("ClientBucket");

                    b.Navigation("FavouritesBucket");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("WebApplication1.Data.dao.Supplier", b =>
                {
                    b.Navigation("AvailableMaterials");

                    b.Navigation("Orders");

                    b.Navigation("SellingProducts");
                });
#pragma warning restore 612, 618
        }
    }
}