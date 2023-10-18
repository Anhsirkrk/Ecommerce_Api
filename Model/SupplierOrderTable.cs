using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class SupplierOrderTable
{
    public int SupplierOrderId { get; set; }

    public int? SupplierId { get; set; }

    public int? OrderId { get; set; }

    public decimal? AmountPerOrder { get; set; }

    public string? OrderStatus { get; set; }

    public string? OrderPaymentStatus { get; set; }

    public string? OrderType { get; set; }

    public DateTime? OrderStartdate { get; set; }

    public DateTime? OrderEnddate { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
