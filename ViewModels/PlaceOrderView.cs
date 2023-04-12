using ZestWeb.Models;

namespace ZestWeb.ViewModels
{
    public class PlaceOrderView
    {
        public User User { get; set; }
        public List<CartsView>  CartsView { get; set; }

    }
}
