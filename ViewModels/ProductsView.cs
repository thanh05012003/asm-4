namespace ZestWeb.ViewModels
{
    public class ProductsView
    {
        public Guid Id { get; set; }
        public Guid  IdProduct { get; set; }
        public Guid IdProductDetails { get; set; }
        public Guid IdTypeProduct { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public string TypeProduct { get; set; }
        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string Supplier { get; set; }
      
    }
}
