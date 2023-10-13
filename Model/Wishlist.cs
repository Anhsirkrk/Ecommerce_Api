using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Wishlist
{
    public int WishlistId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public bool? IsInWishlist { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
