using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class OrderItem
{
    public int ItemId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal ProductPrice { get; set; }

    public int? Quantity { get; set; }

    public int SubscriptionTypeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal? SizeOfProduct { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual SubscriptionType SubscriptionType { get; set; } = null!;
}
