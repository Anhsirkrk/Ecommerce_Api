using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class SubscriptionType
{
    public int SubscriptionId { get; set; }

    public string SubscriptionType1 { get; set; } = null!;

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
