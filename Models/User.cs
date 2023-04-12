namespace ZestWeb.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Guid IdRole { get; set; }
        public int Status { get; set; }
        public string Address { get; set; }
        public virtual Role Role { get; set; }
    }
}
