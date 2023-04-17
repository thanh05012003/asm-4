using ZestWeb.Context;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.ViewModels;

namespace ZestWeb.Service
{

    public class BillService : IBillService
    {
        private readonly AsmDbContext _context;

        public BillService()
        {
            _context = new AsmDbContext();
        }

        public bool add(Bill b)
        {
            if (b == null) return false;
            _context.Add(b);
            _context.SaveChanges();
            return true;
        }

        public bool update(Bill b)
        {
            if (b==null) return false;
            _context.Update(b);
            _context.SaveChanges();
            return true;

        }

        public bool delete(Guid id)
        {
            if (id == null) return false;
            var bill = _context.Bills.FirstOrDefault(c => c.Id == id);
            _context.Bills.Remove(bill);
            _context.SaveChanges();
            return true;
        }

        public Bill getBillById(Guid id)
        {
            return _context.Bills.FirstOrDefault(c => c.Id == id);
        }

        public List<Bill> getBillByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<BillView> GetAll()
        {
            var bill = from a in _context.Bills
                join b in _context.BillDetails on a.Id equals b.IdBill
                join c in _context.ProductDetails on b.IdProductDetails equals c.Id
                join d in _context.Users on a.UserId equals d.Id
                join e in _context.Products on c.IdProduct equals e.Id 
                select new BillView()
                {
                    IdBill = b.IdBill,
                    IdUser = d.Id,
                    CreateDate = a.CreateDate,
                    Images = c.ImageUrl,
                    Price = b.Price,
                    Quantity = b.Quantity,
                    Status = a.Status,
                    Name = e.Name,
                    Color = c.Color,
                };
            return bill.ToList();
        }

        public List<BillView> GetAllBills()
        {
            var bill = from a in _context.Bills
                join b in _context.BillDetails on a.Id equals b.IdBill
                join c in _context.ProductDetails on b.IdProductDetails equals c.Id
                join d in _context.Users on a.UserId equals d.Id
                join e in _context.Products on c.IdProduct equals e.Id
                group new { a, b, c, d, e } by new { e.Name, c.Color, b.Quantity,a.UserId,a.Status} into g
                select new BillView()
                {
                    Name = g.Key.Name,
                    Quantity = g.Key.Quantity,
                    TotalPrice = g.Sum(x => x.b.Price * x.b.Quantity),
                    TotalQuantity = g.Sum(x => x.b.Quantity),
                };
            return bill.ToList();

        }
    }
}