using System;
using System.Collections.Generic;
using LINQTask.Model;
using Microsoft.EntityFrameworkCore;

namespace LINQTask.Data;

public partial class ApplicationDbcontext : DbContext
{
    public ApplicationDbcontext()
    {
    }

    public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options)
    : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerNy> CustomerNies { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPrint> ProductPrints { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer(" Data Source=.;initial catalog =BikeStores528;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Name=\"SQL Server Management Studio\";");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__brands__5E5A8E271C809D1B");

            entity.ToTable("brands", "production");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("brand_name");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__D54EE9B406BFB00A");

            entity.ToTable("categories", "production");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB857B0651D7");

            entity.ToTable("customers", "sales");

            entity.HasIndex(e => e.City, "IX_CitySearch");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<CustomerNy>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("customer_ny");

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("customer_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__46596229B4E312FC");

            entity.ToTable("orders", "sales");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.RequiredDate).HasColumnName("required_date");
            entity.Property(e => e.ShippedDate).HasColumnName("shipped_date");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__orders__customer__5AEE82B9");

            entity.HasOne(d => d.Staff).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__staff_id__5CD6CB2B");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__orders__store_id__5BE2A6F2");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ItemId }).HasName("PK__order_it__837942D43591A625");

            entity.ToTable("order_items", "sales");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Discount)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("discount");
            entity.Property(e => e.ListPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("list_price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__order_ite__order__60A75C0F");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__order_ite__produ__619B8048");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("parts", "production");

            entity.Property(e => e.PartId).HasColumnName("part_id");
            entity.Property(e => e.PartName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("part_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF5D264015C");

            entity.ToTable("products", "production");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ListPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("list_price");
            entity.Property(e => e.ModelYear).HasColumnName("model_year");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_name");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__products__brand___4F7CD00D");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__products__catego__4E88ABD4");
        });

        modelBuilder.Entity<ProductPrint>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("productPrint");

            entity.Property(e => e.Fullname)
                .HasMaxLength(511)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__staffs__1963DD9C335A3A3D");

            entity.ToTable("staffs", "sales");

            entity.HasIndex(e => e.Email, "UQ__staffs__AB6E61646B9DE3F7").IsUnique();

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__staffs__manager___5812160E");

            entity.HasOne(d => d.Store).WithMany(p => p.Staff)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__staffs__store_id__571DF1D5");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.ProductId }).HasName("PK__stocks__E68284D32F7D98BC");

            entity.ToTable("stocks", "production");

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Product).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__stocks__product___656C112C");

            entity.HasOne(d => d.Store).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__stocks__store_id__6477ECF3");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__stores__A2F2A30C668FC402");

            entity.ToTable("stores", "sales");

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("store_name");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("zip_code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
