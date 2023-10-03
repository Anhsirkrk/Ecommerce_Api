namespace Ecommerce_Api.ViewModels
{
    public class BrandViewModel
    {
        public int BrandId { get; set; }

        public int Category_id { get; set; }    

        public string BrandName { get; set; } = null!;

        public string BrandDescription { get; set; }

        public IFormFile image { get; set; }

        public string Base64Image { get; set; }

        public string resultMessage { get; set; }
        public bool IsBrandCreated { get; set; }    
    }
}
