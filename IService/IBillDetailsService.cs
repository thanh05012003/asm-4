
using ZestWeb.Models;


namespace ZestWeb.IService
{
    public interface IBillDetailsService
    {
        public bool add(BillDetails b);
        public bool update(BillDetails b);
        public bool delete(Guid id);
        public BillDetails getBillDetailsById(Guid id);
        
        public List<BillDetails> GetAll();
    }
}
