using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public string ProductName { get; set; } = null!;

    public int StockQuantity { get; set; }

    public decimal Price { get; set; }

    public decimal? Weight { get; set; }

    public string? Unit { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool? IsAvailable { get; set; }

    public DateTime ExpiryDate { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public int? DiscountId { get; set; }

    public string? Description { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
}
