namespace ZestWeb.Models
{
    public class BillDetails
    {
       
        public Guid IdBill { get; set; }
        public Guid IdProductDetails { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public virtual ProductDetails ProductsdDetails { get; set; }
        public virtual Bill Bill { get; set; }
    }
}
