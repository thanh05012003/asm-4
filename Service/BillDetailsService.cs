using ZestWeb.Context;
using ZestWeb.IService;
using ZestWeb.Models;

namespace ZestWeb.Service
{
    public class BillDetailsService:IBillDetailsService
    {
        private AsmDbContext _context;

        public BillDetailsService()
        {
            _context = new AsmDbContext();
        }
        public bool add(BillDetails b)
        {
            if (b == null) return false;
            _context.BillDetails.Add(b);
            _context.SaveChanges();
            return true;
        }

        public bool update(BillDetails b)
        {
            throw new NotImplementedException();
        }

        public bool delete(Guid id)
        {
            if (id == null) return false;
           var billDetails = _context.BillDetails.Find(id);
           _context.BillDetails.Remove(billDetails);
           _context.SaveChanges();
           return true;
        }

        public BillDetails getBillDetailsById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<BillDetails> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
