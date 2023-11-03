namespace Ecommerce_Api.ViewModels
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal RegistrationAmountPaid { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string StatusOfRegistration { get; set; }
        public string PanCard { get; set; }
        public string Licenceno { get; set; }
        public string Password { get; set; }
    }
}
