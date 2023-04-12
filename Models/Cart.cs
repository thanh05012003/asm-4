namespace ZestWeb.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
    }
}
