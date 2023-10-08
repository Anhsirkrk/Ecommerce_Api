using Ecommerce_Api.Model;

namespace Ecommerce_Api.ViewModels
{
    public class UserAdressViewModel
    {
        public int AddressId { get; set; }

        public int UserId { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public string Pincode { get; set; }

        public string HouseNo { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }


    }
}
