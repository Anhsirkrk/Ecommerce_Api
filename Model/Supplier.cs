using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public DateTime? JoinDate { get; set; }

    public decimal? RegistrationAmountPaid { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? StatusOfRegistration { get; set; }

    public string? PanCard { get; set; }

    public string? Licenceno { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<SupplierBrand> SupplierBrands { get; set; } = new List<SupplierBrand>();

    public virtual ICollection<SupplierOrderTable> SupplierOrderTables { get; set; } = new List<SupplierOrderTable>();

    public virtual ICollection<SupplierPinCode> SupplierPinCodes { get; set; } = new List<SupplierPinCode>();
}
