﻿using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
