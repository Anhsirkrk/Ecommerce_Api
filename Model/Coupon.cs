using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string? Code { get; set; }

    public decimal? Discount { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();
}
