using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Model;

public partial class EcommerceDailyPickContext : DbContext
{
    public EcommerceDailyPickContext()
    {
    }

    public EcommerceDailyPickContext(DbContextOptions<EcommerceDailyPickContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductItemDetail> ProductItemDetails { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierBrand> SupplierBrands { get; set; }

    public virtual DbSet<SupplierOrderTable> SupplierOrderTables { get; set; }

    public virtual DbSet<SupplierPinCode> SupplierPinCodes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCoupon> UserCoupons { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=VVSGHP\\SQLEXPRESS;Database=Ecommerce_dailyPick;Integrated Security=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__091C2A1BACF09119");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HouseNo)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Pincode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Supplier_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Address__UserID__6383C8BA");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brand__AABC2567AE756B73");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("Brand_id");
            entity.Property(e => e.BrandDescription).HasColumnType("text");
            entity.Property(e => e.BrandName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Brand_Name");
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Brands)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Brand__Category___6477ECF3");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__6DB38D6EA5C818EB");

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
            entity.HasKey(e => e.CouponId).HasName("PK__Coupons__384AF1DA633E34C4");

            entity.HasIndex(e => e.Code, "UQ__Coupons__A25C5AA732B10DC6").IsUnique();

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
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__6C137204B3FCB37F");

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
            entity.HasKey(e => e.LogId).HasName("PK__Log__5E54864837C8023D");

            entity.ToTable("Log");

            entity.Property(e => e.EventDescription)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Log__User_Id__656C112C");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__F1E4607BBD08AAA1");

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdat");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("Order_Date");
            entity.Property(e => e.OrderPaymentStatus)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");
            entity.Property(e => e.SubscriptionTypeId).HasColumnName("Subscription_Type_Id");
            entity.Property(e => e.TimeSlot)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Total_Amount");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_order_Address_Id");

            entity.HasOne(d => d.SubscriptionType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_SubscriptionId_Subscription");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_order_Supplier_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_UserId_User");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__OrderIte__727E838B4174F347");

            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("End_Date");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.ProductPrice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Product_Price");
            entity.Property(e => e.SizeOfProduct).HasColumnType("decimal(10, 2)");
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
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58766F4354");

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
                .HasConstraintName("FK__Payments__OrderI__6B24EA82");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__9834FBBAED342D48");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Brand_Id_Brand");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_Id_Category");
        });

        modelBuilder.Entity<ProductItemDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3213E83F6213FE77");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AvailableQuantity)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Available_Quantity");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.DiscountId).HasColumnName("Discount_id");
            entity.Property(e => e.ExpiryDate).HasColumnType("date");
            entity.Property(e => e.ManufactureDate).HasColumnType("date");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("PRICE");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.SizeOfEachUnit).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.StockOfEachUnit).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.WeightOfEachUnit).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Discount).WithMany(p => p.ProductItemDetails)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK__ProductIt__Disco__6E01572D");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductItemDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductIt__Produ__6EF57B66");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE011848E0");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Reviews__Product__6FE99F9F");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserID__70DDC3D8");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Shopping__51BCD797B87331FD");

            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ShoppingC__UserI__73BA3083");
        });

        modelBuilder.Entity<ShoppingCartItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Shopping__727E83EBBFC3F786");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SizeOfItem).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Cart).WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__ShoppingC__CartI__71D1E811");

            entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ShoppingC__Produ__72C60C4A");
        });

        modelBuilder.Entity<SubscriptionType>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__subscrip__51805A51D457A960");

            entity.ToTable("subscription_type");

            entity.Property(e => e.SubscriptionId).HasColumnName("Subscription_Id");
            entity.Property(e => e.SubscriptionType1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Subscription_Type");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__83918DB89CDAB562");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("datetime")
                .HasColumnName("Expiry_Date");
            entity.Property(e => e.JoinDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Licenceno)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PanCard)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationAmountPaid).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StatusOfRegistration)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SupplierBrand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3213E83FC99BE8E0");

            entity.ToTable("SupplierBrand");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

            entity.HasOne(d => d.BrandIdOfSupplyNavigation).WithMany(p => p.SupplierBrands)
                .HasForeignKey(d => d.BrandIdOfSupply)
                .HasConstraintName("FK__SupplierB__Brand__1EA48E88");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierBrands)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__SupplierB__Suppl__1DB06A4F");
        });

        modelBuilder.Entity<SupplierOrderTable>(entity =>
        {
            entity.HasKey(e => e.SupplierOrderId).HasName("PK__Supplier__BB5F7DD65AAAE1C1");

            entity.ToTable("Supplier_order_Table");

            entity.Property(e => e.SupplierOrderId).HasColumnName("Supplier_order_ID");
            entity.Property(e => e.AmountPerOrder)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Amount_per_Order");
            entity.Property(e => e.OrderEnddate)
                .HasColumnType("datetime")
                .HasColumnName("Order_enddate");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.OrderPaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Order_Payment_status");
            entity.Property(e => e.OrderStartdate)
                .HasColumnType("datetime")
                .HasColumnName("Order_startdate");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Order_status");
            entity.Property(e => e.OrderType).HasColumnName("Order_type");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.SupplierOrderTables)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Supplier___Order__22751F6C");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierOrderTables)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Supplier___Suppl__2180FB33");
        });

        modelBuilder.Entity<SupplierPinCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3213E83FCFA6682C");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PinCodeOfSupply)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierPinCodes)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__SupplierP__Suppl__1AD3FDA4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__206D91701A565368");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E401826BAD").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053407E15804").IsUnique();

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
            entity.HasKey(e => e.UserCouponId).HasName("PK__UserCoup__22994B733EECD668");

            entity.Property(e => e.UserCouponId).HasColumnName("UserCouponID");
            entity.Property(e => e.CouponId).HasColumnName("CouponID");
            entity.Property(e => e.UsageDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Coupon).WithMany(p => p.UserCoupons)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("FK__UserCoupo__Coupo__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.UserCoupons)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserCoupo__UserI__75A278F5");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.UserSubscriptionId).HasName("PK__UserSubs__D1FD775CCC7F1797");

            entity.Property(e => e.UserSubscriptionId).HasColumnName("UserSubscriptionID");
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
                .HasConstraintName("FK__UserSubsc__Subsc__778AC167");

            entity.HasOne(d => d.User).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubsc__User___787EE5A0");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__User_typ__FE91E1E6A67F8923");

            entity.ToTable("User_types");

            entity.Property(e => e.TypeId).HasColumnName("Type_id");
            entity.Property(e => e.UserType1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("User_Type");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendors__FC8618D313FFBCB1");

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
                .HasConstraintName("FK__Vendors__Brand_I__797309D9");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.WishlistId).HasName("PK__Wishlist__233189CB2F38CF01");

            entity.ToTable("Wishlist");

            entity.Property(e => e.WishlistId).HasColumnName("WishlistID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wishlist__Produc__03F0984C");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wishlist__UserID__02FC7413");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
