

using ZestWeb.Context;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.ViewModels;

namespace ZestWeb.Service
{
    public class CartService:ICartService
    {
        private readonly AsmDbContext context;
        public CartService()
        {
            context = new AsmDbContext();
        }

        public bool add(Cart b)
        {
            try
            {
                context.Carts.Add(b);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(Cart b)
        {
            if (b != null)
            {
                //var cart = context.Carts.FirstOrDefault(p => p.Id == b.id);
                //product.Name = b.Name;
                //product.Description = b.Description;
                //product.AvailableQuantity = b.AvailableQuantity;
                //product.Status = b.Status;
                //product.Url = b.Url;
                //product.Price = b.Price;
                //product.Supplier = b.Supplier;
                //context.Products.Update(product);
                //context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Cart getCartById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Cart> getCartByName(string name)
        {
            throw new NotImplementedException();
        }


        public List<CartsView> GetAll()
        {
            var cart = from a in context.CartDetails
                join b in context.Carts on a.IdCart equals b.Id
                join c in context.ProductDetails on a.IdProductDetails equals c.Id
                join d in context.Products on c.IdProduct equals d.Id
                select new CartsView()
                {
                    IdCart = b.Id,
                    IdProductDetails = a.IdProductDetails,
                    Description = b.Description,
                    Quantity = a.Quantity,
                    UserId = b.UserId,
                    NameProduct = d.Name,
                    ImageUrl = c.ImageUrl
                };
            return cart.ToList();
        }
    }
}
