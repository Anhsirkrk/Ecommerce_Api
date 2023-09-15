namespace Ecommerce_Api.ViewModels
{
    public class CartViewModel
    {

        public int UserId { get; set; }
        public int ItemId { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public List<int> ProductsList { get; set; }
        public List<int> Quantitiesofeachproduct { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Resultmessage { get;set; }

        public bool IsItemAdded { get; set; }
        public bool IsCartCreated{ get; set; }
        public bool IsItemDeleted { get; set; }

        public bool IsQuantityUpdated { get; set; } 
        public int CreatedCartID { get; set; } 
       
    }
}
