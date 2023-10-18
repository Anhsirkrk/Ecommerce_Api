using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class SupplierBrand
{
    public int Id { get; set; }

    public int? SupplierId { get; set; }

    public int? BrandIdOfSupply { get; set; }

    public virtual Brand? BrandIdOfSupplyNavigation { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
