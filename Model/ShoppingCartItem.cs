using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class ShoppingCartItem
{
    public int ItemId { get; set; }

    public int? CartId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal? SizeOfItem { get; set; }

    public virtual ShoppingCart? Cart { get; set; }

    public virtual Product? Product { get; set; }
}
