using ZestWeb.Models;
using ZestWeb.ViewModels;


namespace ZestWeb.IService
{
    public interface ICartDetailsService
    {
        public bool add(CartsView b);
        public bool update(CartsView b);
        public bool delete(Guid id);
        public CartDetails getCartDetailsById(Guid id);
        public List<CartDetails> getCartDetailsByName(string name);
        public List<CartsView> GetAll();
    }
}
