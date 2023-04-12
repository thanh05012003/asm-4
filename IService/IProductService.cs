using ZestWeb.Models;
using ZestWeb.ViewModels;


namespace ZestWeb.IService
{
    public interface IProductService
    {
        public bool add(ProductsView b);
        public bool update(ProductsView b);
        public bool delete(Guid id);
        public Products getById(Guid id);
        public List<Products> getByName(string name);
        public List<ProductsView> viewProduct();
        public ProductsView viewProductById(Guid id);
        public List<ProductsView> GetAll();
        
    }
}
