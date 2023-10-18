using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int SubscriptionTypeId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? OrderPaymentStatus { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual SubscriptionType SubscriptionType { get; set; } = null!;

    public virtual ICollection<SupplierOrderTable> SupplierOrderTables { get; set; } = new List<SupplierOrderTable>();

    public virtual User User { get; set; } = null!;
}
