using System.Collections;

namespace ZestWeb.Models
{
    public class Products 
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public Guid IdTypeProduct { get; set; }
        public virtual TypeProduct TypeProduct { get; set; }


    
    }
}
