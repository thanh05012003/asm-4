using ZestWeb.Context;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.ViewModels;

namespace ZestWeb.Service
{
    public class CartsDetailsService:ICartDetailsService
    {
        private AsmDbContext _context;
        public CartsDetailsService()
        {
            _context = new AsmDbContext();
        }
        public bool add(CartsView b)
        {
            try
            {
                CartDetails carteDetails = new CartDetails()
                {
                    IdProductDetails = b.IdProductDetails,
                    IdCart = b.IdCart,
                   Quantity = b.Quantity,
                   Status = b.Status
                };
                _context.CartDetails.Add(carteDetails);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(CartsView b)
        {
            if (b != null)
            {
                var carDt = _context.CartDetails.FirstOrDefault(c =>
                    c.IdCart == b.IdCart && c.IdProductDetails == b.IdProductDetails);
               carDt.Quantity = b.Quantity;
               carDt.Status = b.Status;
                _context.CartDetails.Update(carDt);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool delete(CartsView b)
        {
            if (b != null)
            {
                var carDt = _context.CartDetails.FirstOrDefault(c =>
                    c.IdCart == b.IdCart && c.IdProductDetails == b.IdProductDetails);
                _context.CartDetails.Remove(carDt);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public CartDetails getCartDetailsById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<CartDetails> getCartDetailsByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<CartsView> GetAll()
        {
            var cartdetail = from a in _context.CartDetails
                join b in _context.Carts on a.IdCart equals b.Id
                join c in _context.ProductDetails on a.IdProductDetails equals c.Id
                join d in _context.Products on c.IdProduct equals d.Id
                select new CartsView()
                {
                    IdCart = b.Id,
                    IdProductDetails = a.IdProductDetails,
                    Description = b.Description,
                    Quantity = a.Quantity,
                    UserId = b.UserId,
                    NameProduct = d.Name,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price,
                    Status = a.Status,
                };
           return cartdetail.ToList();
        }
    }
}
