using ZestWeb.Context;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.ViewModels;

namespace ZestWeb.Service
{
    public class ProductDetailsService:IProductDetailsService
    {
        private readonly AsmDbContext _context;
        public ProductDetailsService()
        {
            _context = new AsmDbContext();
        }
        public bool add(ProductsView b)
        {
            if (b == null) return false;
            ProductDetails product = new ProductDetails()
            {
                Id = Guid.NewGuid(),
                IdProduct = b.Id,
                Price = b.Price,
                AvailableQuantity = b.AvailableQuantity,
                Status = b.Status,
                ImageUrl = b.ImageUrl,
                Color = b.Color
            };
            _context.ProductDetails.Add(product);
            _context.SaveChanges();
            return true;
        }

        public bool update(ProductsView b)
        {
            if (b != null)
            {
                var proDetails = _context.ProductDetails.FirstOrDefault(c => c.Id == b.Id);
                proDetails.Price = b.Price;
                proDetails.AvailableQuantity = b.AvailableQuantity;
                proDetails.Status = b.Status;
                proDetails.ImageUrl = b.ImageUrl;
                proDetails.Color = b.Color;
                //product.TypeProduct = b.TypeProduct;
                _context.ProductDetails.Update(proDetails);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

     

        public bool delete(Guid id)
        {
            try
            {
                var product = _context.ProductDetails.Find(id);
                _context.ProductDetails.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ProductsView getById(Guid id)
        {
            return viewProduct().FirstOrDefault(c => c.Id == id);
        }

        public List<ProductDetails> getByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<ProductsView> viewProduct()
        {
            var viewPro = from a in _context.ProductDetails.ToList()
                join b in _context.Products.ToList() on a.IdProduct equals b.Id
                select new ProductsView()
                {
                    Id = a.Id,
                    Price = a.Price,
                    Status = b.Status,
                    AvailableQuantity = a.AvailableQuantity,
                    Description = b.Description,
                    ImageUrl = a.ImageUrl,
                    Name = b.Name,
                    Supplier = b.Supplier,
                    Color = a.Color,
                    IdProductDetails = a.Id
                };
            return viewPro.ToList();
        }

        public List<ProductDetails> GetAll()
        {
            return _context.ProductDetails.ToList();
        }
    }
}
