using System.Collections;

namespace ZestWeb.Models
{
    public class TypeProduct 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
