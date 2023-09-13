using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Discount
{
    public int DiscountId { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
