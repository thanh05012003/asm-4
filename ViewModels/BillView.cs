namespace ZestWeb.ViewModels
{
    public class BillView
    {
        public Guid IdBill { get; set; }
        public Guid IdUser { get; set; }
        public int Status { get; set; }
        public string Images { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
    }
}
