using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Model;

public partial class EcommercedemoContext : DbContext
{
    public EcommercedemoContext()
    {
    }

    public EcommercedemoContext(DbContextOptions<EcommercedemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCoupon> UserCoupons { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.in_userreg
     => optionsBuilder.UseSqlServer("Server=KRISHNA\\SQLEXPRESS;Database=Ecommercedemo;Integrated Security=true;TrustServerCertificate=True");
        => optionsBuilder.UseSqlServer("Server=KRISHNA\\SQLEXPRESS;Database=Ecommercedemo;Integrated Security=true;TrustServerCertificate=True")

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {

            entity.HasKey(e => e.BrandId).HasName("PK__Brand__AABC25675F7F7331");


            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("Brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Brand_Name");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__6DB38D6E887FB171");


            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Category_Name");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {

            entity.HasKey(e => e.CouponId).HasName("PK__Coupons__384AF1DA815B89E5");

            entity.HasIndex(e => e.Code, "UQ__Coupons__A25C5AA7BA1CD6C3").IsUnique();


            entity.Property(e => e.CouponId).HasColumnName("CouponID");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ExpiryDate).HasColumnType("date");
        });

        modelBuilder.Entity<Discount>(entity =>
        {

            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__6C1372049015074A");


            entity.ToTable("Discount");

            entity.Property(e => e.DiscountId).HasColumnName("Discount_Id");
            entity.Property(e => e.DiscountPercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Discount_percentage");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
        });

        modelBuilder.Entity<Log>(entity =>
        {

            entity.HasKey(e => e.LogId).HasName("PK__Log__5E5486481AFAA093");


            entity.ToTable("Log");

            entity.Property(e => e.EventDescription)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .HasForeignKey(d => d.UserId)

                .HasConstraintName("FK__Log__User_Id__05D8E0BE");

        });

        modelBuilder.Entity<Order>(entity =>
        {

            entity.HasKey(e => e.OrderId).HasName("PK__Orders__F1E4607B940B4F4E");


            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("End_Date");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("Order_Date");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("Start_Date");
            entity.Property(e => e.SubscriptionTypeId).HasColumnName("Subscription_Type_Id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Total_Amount");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.SubscriptionType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_SubscriptionId_Subscription");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_UserId_User");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {

            entity.HasKey(e => e.ItemId).HasName("PK__OrderIte__727E838BD4CE39A5");

            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("End_Date");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.ProductPrice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Product_Price");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("Start_Date");
            entity.Property(e => e.SubscriptionTypeId).HasColumnName("Subscription_Type_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_OrderId_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_ProductId_Products");

            entity.HasOne(d => d.SubscriptionType).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_SubscriptionId_subscriptions");
        });

        modelBuilder.Entity<Payment>(entity =>
        {

            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58FF4F08F1");


            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Payment_Status");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TransactionID");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)

                .HasConstraintName("FK__Payments__OrderI__76969D2E");

        });

        modelBuilder.Entity<Product>(entity =>
        {

            entity.HasKey(e => e.ProductId).HasName("PK__Product__9834FBBA5066984B");


            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.DiscountId).HasColumnName("Discount_id");
            entity.Property(e => e.ExpiryDate).HasColumnType("date");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.ManufactureDate).HasColumnType("date");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Product_Name");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Weight).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Brand_Id_Brand");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_Id_Category");

            entity.HasOne(d => d.Discount).WithMany(p => p.Products)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discount_Id");
        });

        modelBuilder.Entity<Review>(entity =>
        {

            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEE5FC7E27");


            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)

                .HasConstraintName("FK__Reviews__Product__00200768");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserID__7F2BE32F");

        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {

            entity.HasKey(e => e.CartId).HasName("PK__Shopping__51BCD79753A95F37");


            entity.Property(e => e.CartId)
                .ValueGeneratedNever()
                .HasColumnName("CartID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.UserId)

                .HasConstraintName("FK__ShoppingC__UserI__5DCAEF64");

        });

        modelBuilder.Entity<ShoppingCartItem>(entity =>
        {

            entity.HasKey(e => e.ItemId).HasName("PK__Shopping__727E83EBF5E912CE");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("ItemID");
            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Cart).WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(d => d.CartId)

                .HasConstraintName("FK__ShoppingC__CartI__60A75C0F");

            entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ShoppingC__Produ__619B8048");

        });

        modelBuilder.Entity<SubscriptionType>(entity =>
        {

            entity.HasKey(e => e.SubscriptionId).HasName("PK__subscrip__51805A5110CD04AA");


            entity.ToTable("subscription_type");

            entity.Property(e => e.SubscriptionId).HasColumnName("Subscription_Id");
            entity.Property(e => e.SubscriptionType1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Subscription_Type");
        });

        modelBuilder.Entity<User>(entity =>
        {

            entity.HasKey(e => e.UserId).HasName("PK__Users__206D917049C8403F");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4FF815A07").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534792C6D9C").IsUnique();


            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usertypeid_User_types");
        });

        modelBuilder.Entity<UserCoupon>(entity =>
        {

            entity.HasKey(e => e.UserCouponId).HasName("PK__UserCoup__22994B734843CC75");

            entity.Property(e => e.UserCouponId).HasColumnName("UserCouponID");
            entity.Property(e => e.CouponId).HasColumnName("CouponID");
            entity.Property(e => e.UsageDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Coupon).WithMany(p => p.UserCoupons)
                .HasForeignKey(d => d.CouponId)

                .HasConstraintName("FK__UserCoupo__Coupo__693CA210");

            entity.HasOne(d => d.User).WithMany(p => p.UserCoupons)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserCoupo__UserI__68487DD7");

        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {

            entity.HasKey(e => e.UserSubscriptionId).HasName("PK__UserSubs__D1FD775C41E3F219");


            entity.Property(e => e.UserSubscriptionId)
                .ValueGeneratedNever()
                .HasColumnName("UserSubscriptionID");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.SubscriptionPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Subscription_Price");
            entity.Property(e => e.SubscriptionTypeId).HasColumnName("Subscription_TypeId");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.SubscriptionType).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)

                .HasConstraintName("FK__UserSubsc__Subsc__7B5B524B");


            entity.HasOne(d => d.User).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)

                .HasConstraintName("FK__UserSubsc__User___7A672E12");

        });

        modelBuilder.Entity<UserType>(entity =>
        {

            entity.HasKey(e => e.TypeId).HasName("PK__User_typ__FE91E1E69C225DC6");


            entity.ToTable("User_types");

            entity.Property(e => e.TypeId).HasColumnName("Type_id");
            entity.Property(e => e.UserType1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("User_Type");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {

            entity.HasKey(e => e.VendorId).HasName("PK__Vendors__FC8618D3973404CC");


            entity.Property(e => e.VendorId).HasColumnName("VendorID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BrandId).HasColumnName("Brand_ID");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LogoURL");
            entity.Property(e => e.NameofVendor)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Brand).WithMany(p => p.Vendors)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__Vendors__LogoURL__02FC7413");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
