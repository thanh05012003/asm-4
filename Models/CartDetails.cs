namespace ZestWeb.Models
{
    public class CartDetails
    {
  
        public Guid IdCart { get; set; }
        public Guid IdProductDetails { get; set; }
        public int Quantity { get; set; }
        public virtual ProductDetails ProductsdDetails { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
