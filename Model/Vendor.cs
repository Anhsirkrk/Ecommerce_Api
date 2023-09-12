using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string? NameofVendor { get; set; }

    public string? Address { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? Description { get; set; }

    public string? LogoUrl { get; set; }
}
