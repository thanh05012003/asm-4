using ZestWeb.Models;
using ZestWeb.ViewModels;


namespace ZestWeb.IService
{
    public interface ICartService
    {
        public bool add(Cart b);
        public bool update(Cart b);
        public bool delete(Guid id);
        public Cart getCartById(Guid id);
        public List<Cart> getCartByName(string name);
        public List<CartsView> GetAll();
    }
}
