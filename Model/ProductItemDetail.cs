using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class ProductItemDetail
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? Unit { get; set; }

    public decimal? SizeOfEachUnit { get; set; }

    public decimal? WeightOfEachUnit { get; set; }

    public decimal? StockOfEachUnit { get; set; }

    public decimal? Price { get; set; }

    public bool? IsAvailable { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int? DiscountId { get; set; }

    public decimal? AvailableQuantity { get; set; }

    public string? Description { get; set; }

    public DateTime? AddedDate { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual Product? Product { get; set; }
}
