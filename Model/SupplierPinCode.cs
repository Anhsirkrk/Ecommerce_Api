using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class SupplierPinCode
{
    public int Id { get; set; }

    public int? SupplierId { get; set; }

    public string? PinCodeOfSupply { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
