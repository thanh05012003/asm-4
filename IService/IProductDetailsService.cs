using ZestWeb.Models;
using ZestWeb.ViewModels;


namespace ZestWeb.IService
{
    public interface IProductDetailsService
    {
        public bool add(ProductsView b);
        public bool update(ProductsView b);
        public bool delete(Guid id);
        public ProductsView getById(Guid id);
        public List<ProductDetails> getByName(string name);
        public List<ProductsView> viewProduct();
        public List<ProductDetails> GetAll();
    }
}
