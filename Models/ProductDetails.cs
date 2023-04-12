namespace ZestWeb.Models
{
    public class ProductDetails
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public int Status { get; set; }
        public string ImageUrl { get; set; }
       
        public Guid IdProduct { get; set; }
        public string Color { get; set; }
       
        public virtual Products Products { get; set; }
       
       
    }
}
