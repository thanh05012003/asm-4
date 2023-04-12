using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
using ZestWeb.Context;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.Service;
using ZestWeb.ViewModels;

namespace ZestWeb.Controllers;

public class CartController : Controller
{
    private ICartDetailsService _cartDetailsService;
    private readonly IProductDetailsService _productDetailsService;
    private readonly ITypeProductsService _typesService;
    private readonly ICartService _cartService;
    private readonly IUserService _userService;


    public CartController()
    {
        _cartDetailsService = new CartsDetailsService();
        _productDetailsService = new ProductDetailsService();
        _typesService = new TypeProductsService();
        _cartService = new CartService();
        _userService = new UserService();
    }

    public IActionResult LstCartDetails()
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null) return RedirectToAction("Home", "Product");
        var cartDetails = _cartDetailsService.GetAll().Where(c => c.UserId == Guid.Parse(userId));
        var jsonOrder = JsonConvert.SerializeObject(cartDetails);
        HttpContext.Session.SetString("jsonOrder",jsonOrder);
        return View(cartDetails);
    }

    public IActionResult AddToCart(ProductsView pro)
    {
        var idUser = HttpContext.Session.GetString("UserId"); // lấy userId từ session khi login thành công
        if (idUser == null) return RedirectToAction("Login", "Login");
        foreach (var t in _cartService.GetAll())
            if (idUser == t.UserId.ToString()) //  nếu userId tồn tại trong giỏ hàng
            {
                foreach (var a in _cartDetailsService.GetAll().Where(c => c.UserId == Guid.Parse(idUser)))
                {
                    if (pro.IdProductDetails == a.IdProductDetails) // cộng thêm số lượng nếu tồn tại
                    {
                        _cartDetailsService.update(new CartsView()
                        {
                            IdProductDetails = pro.IdProductDetails,
                            IdCart = t.IdCart,
                            Quantity = a.Quantity + pro.Quantity //Cộng số lượng
                        });
                        return RedirectToAction("Home", "Product");
                    }
                    else
                    {
                        _cartDetailsService.add(new CartsView()
                        {
                            IdProductDetails = pro.IdProductDetails,
                            IdCart = t.IdCart,
                            Quantity = pro.Quantity
                        });
                        return RedirectToAction("Home", "Product");
                    }
                }
            }

        var cart = new Cart() // tạo mới giỏ hàng 
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse(idUser),
            Description = ""
        };
        if (_cartService.add(cart)) // add sản phẩm
        {
            _cartDetailsService.add(new CartsView()
            {
                IdProductDetails = pro.IdProductDetails,
                IdCart = cart.Id,
                Quantity = pro.Quantity
            });
            return RedirectToAction("Home", "Product");
        }

        return RedirectToAction("Home", "Product");
    }

    public IActionResult CheckOut()
    {
        string userId = HttpContext.Session.GetString("UserId");
        if (userId!=null)
        {
            var user = _userService.getUserById(Guid.Parse(userId));
            var cart = JsonConvert.DeserializeObject<List<CartsView>>(HttpContext.Session.GetString("jsonOrder"));
            PlaceOrderView placeOrder = new PlaceOrderView()
            {
                User = user,
                CartsView = cart
            };
            return View(placeOrder);
        }
        return View();
    }

    //public IActionResult PlaceOrder()
    //{
    //    return 
    //}
}