using ZestWeb.Models;


namespace ZestWeb.IService
{
    public interface IBillService
    {
        public bool add(Bill b);
        public bool update(Bill b);
        public bool delete(Guid id);
        public Bill getBillById(Guid id);
        public List<Bill> getBillByName(string name);
        public List<Bill> GetAll();
    }
}
