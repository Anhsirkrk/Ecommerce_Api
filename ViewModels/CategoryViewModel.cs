namespace Ecommerce_Api.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string Description { get; set; }
         
        public IFormFile image { get; set; }    

    }
}
