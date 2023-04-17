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
    private readonly IBillService _billService;
    private readonly IBillDetailsService _billDetailsService;

    public CartController()
    {
        _cartDetailsService = new CartsDetailsService();
        _productDetailsService = new ProductDetailsService();
        _typesService = new TypeProductsService();
        _cartService = new CartService();
        _userService = new UserService();
        _billService = new BillService();
        _billDetailsService = new BillDetailsService();
    }

    public IActionResult LstCartDetails()
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null) return RedirectToAction("Login", "Login");
        var cartDetails = _cartDetailsService.GetAll().Where(c => c.UserId == Guid.Parse(userId) && c.Status == 1);
        var jsonOrder = JsonConvert.SerializeObject(cartDetails);
        HttpContext.Session.SetString("jsonOrder", jsonOrder); // đưa thông tin giỏ hàng vào session
        return View(cartDetails);
    }

    public IActionResult AddToCart(ProductsView pro)
    {
        var idUser = HttpContext.Session.GetString("UserId"); // lấy userId từ session khi login thành công
        if (idUser == null) return RedirectToAction("Login", "Login");
        foreach (var t in _cartService.GetAll())
            if (idUser == t.UserId.ToString()) //  nếu userId tồn tại trong giỏ hàng
                foreach (var a in _cartDetailsService.GetAll().Where(c => c.UserId == Guid.Parse(idUser)))
                    if (pro.IdProductDetails == a.IdProductDetails) // cộng thêm số lượng nếu tồn tại
                    {
                        _cartDetailsService.update(new CartsView()
                        {
                            IdProductDetails = pro.IdProductDetails,
                            IdCart = t.IdCart,
                            Status = 1,
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
                            Status = 1,
                            Quantity = pro.Quantity
                        });
                        return RedirectToAction("Home", "Product");
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
                Status = 1,
                Quantity = pro.Quantity
            });
            return RedirectToAction("Home", "Product");
        }

        return RedirectToAction("Home", "Product");
    }

    public IActionResult CheckOut()
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId != null)
        {
            var user = _userService.getUserById(Guid.Parse(userId));
            var cart = JsonConvert.DeserializeObject<List<CartsView>>(HttpContext.Session.GetString("jsonOrder"));
            var placeOrder = new PlaceOrderView()
            {
                User = user,
                CartsView = cart
            };
            return View(placeOrder);
        }

        return View();
    }

    public IActionResult PlaceOrder()
    {
        var userId = HttpContext.Session.GetString("UserId");
        var user = _userService.getUserById(Guid.Parse(userId));
        var cart = JsonConvert.DeserializeObject<List<CartsView>>(HttpContext.Session.GetString("jsonOrder"));

        if (user != null)
        {
            #region Tạo bill

            var bill = new Bill()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Today,
                Status = 0,
                UserId = user.Id
            };
            _billService.add(bill);

            HttpContext.Session.SetString("IdBill", bill.Id.ToString());

            #endregion

            #region Tạo bill chi tiết

            foreach (var item in cart)
            {
                var billDetails = new BillDetails()
                {
                    IdBill = bill.Id,
                    IdProductDetails = item.IdProductDetails,
                    Quantity = item.Quantity,
                    Price = item.Price * item.Quantity
                };
                _billDetailsService.add(billDetails);
            }

            #endregion

            #region Cập nhật lại giỏ hàng

            foreach (var item in cart)
            {
                item.Status = 0;
                _cartDetailsService.update(item);
            }

            #endregion

            #region Giảm số lượng tồn

            foreach (var it in cart)
            {
                var pro = _productDetailsService.getById(it.IdProductDetails);
                pro.AvailableQuantity -= it.Quantity;
                _productDetailsService.update(pro);
            }

            #endregion
        }

        return RedirectToAction("MyPurchase", "Bill");
    }

    public IActionResult DeleteCart(CartsView cart)
    {
        if (_cartDetailsService.delete(cart)) return RedirectToAction("LstCartDetails");
        return RedirectToAction("LstCartDetails");
    }
}