using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class UserCoupon
{
    public int UserCouponId { get; set; }

    public int? UserId { get; set; }

    public int? CouponId { get; set; }

    public DateTime? UsageDate { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual User? User { get; set; }
}
