using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public string? Imageurl { get; set; }

    public string? BrandDescription { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
