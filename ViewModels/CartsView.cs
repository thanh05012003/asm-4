namespace ZestWeb.ViewModels
{
    public class CartsView
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public Guid IdCart { get; set; }
        public Guid IdProductDetails { get; set; }
        public int Quantity { get; set; }
        public string NameProduct { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public int Status { get; set; }
      
      
    }
}
