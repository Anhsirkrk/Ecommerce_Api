using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Address
{
    public int AddressId { get; set; }

    public int? UserId { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public string? Area { get; set; }

    public string? Pincode { get; set; }

    public string? HouseNo { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public string? Username { get; set; }

    public string? MobileNumber { get; set; }

    public int? SupplierId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Supplier? Supplier { get; set; }

    public virtual User? User { get; set; }
}
