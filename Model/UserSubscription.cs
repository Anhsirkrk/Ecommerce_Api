using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class UserSubscription
{
    public int UserSubscriptionId { get; set; }

    public int UserId { get; set; }

    public int SubscriptionTypeId { get; set; }

    public int OrderId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal SubscriptionPrice { get; set; }

    public bool? IsActive { get; set; }

    public virtual SubscriptionType SubscriptionType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
