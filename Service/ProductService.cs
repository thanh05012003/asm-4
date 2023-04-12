using ZestWeb.Context;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.ViewModels;

namespace ZestWeb.Service;

public class ProductService : IProductService
{
    private AsmDbContext _DbContext;

    public ProductService()
    {
        _DbContext = new AsmDbContext();
    }

    public bool add(ProductsView b)
    {
        if (b == null) return false;

        Products products = new Products()
        {
            Id = b.Id,
            Name = b.Name,
            Status = b.Status,
            Description = b.Description,
            IdTypeProduct = b.IdTypeProduct,
            Supplier = b.Supplier,
        };
        _DbContext.Products.Add(products);
        _DbContext.SaveChanges();
        return true;
    }

    public bool update(ProductsView b)
    {
        if (b != null)
        {
            var product = _DbContext.Products.FirstOrDefault(p => p.Id == b.Id);
            product.Name = b.Name;
            product.Description = b.Description;
            product.Status = b.Status;
            product.IdTypeProduct = b.IdTypeProduct;
            _DbContext.Products.Update(product);
            _DbContext.SaveChanges();
            return true;
        }

        return false;
    }

    public bool delete(Guid id)
    {
        try
        {
            var product = _DbContext.Products.Find(id);
            _DbContext.Products.Remove(product);
            _DbContext.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public Products getById(Guid id)
    {
        return _DbContext.Products.FirstOrDefault(p => p.Id == id);
    }

    public List<Products> getByName(string name)
    {
        return _DbContext.Products.Where(c => c.Name == name).ToList();
    }

    public List<ProductsView> viewProduct() //Load trang chủ
    {
        var viewPro = from a in _DbContext.ProductDetails.ToList()
            join b in _DbContext.Products.ToList() on a.IdProduct equals b.Id
            join c in _DbContext.TypeProducts.ToList() on b.IdTypeProduct equals c.Id 
            select new ProductsView()
            {
                Id = b.Id,
                Price = a.Price,
                Status = b.Status,
                AvailableQuantity = a.AvailableQuantity,
                Description = b.Description,
                ImageUrl = a.ImageUrl,
                Name = b.Name,
                Supplier = b.Supplier,
                Color = a.Color,
                IdProductDetails = a.Id,
                TypeProduct = c.Name,
                IdTypeProduct = c.Id
            };
        return viewPro.ToList();
    }

    public ProductsView viewProductById(Guid id)
    {
       var pro = viewProduct().FirstOrDefault(p => p.Id == id);
       return pro;
    }


    public List<ProductsView> GetAll()
    {
        var img = _DbContext.ProductDetails.ToList();
        var pro = from a in _DbContext.Products
            join b in _DbContext.TypeProducts on a.IdTypeProduct equals b.Id
            select new ProductsView()
            {
                Id = a.Id,
                Name = a.Name,
                Status = a.Status,
                Description = a.Description,
                Supplier = a.Supplier,
                TypeProduct = b.Name,
            };
        return pro.ToList();
    }


}