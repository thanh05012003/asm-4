using Microsoft.AspNetCore.Mvc;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.Service;
using ZestWeb.ViewModels;

namespace ZestWeb.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;


    private readonly ILogger<HomeController> _logger;
    private readonly ITypeProductsService _typeProducts;
    private readonly IUserService _userService;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _productService = new ProductService();
        _typeProducts = new TypeProductsService();
        _userService = new UserService();
    }

    public IActionResult TypeProduct()
    {
        var Type = _typeProducts.GetAll();
        var typrPro = new TypeProductView()
        {
            lstTypePro = Type,
            TypeProduct = new TypeProduct()
        };
        ViewBag.TypePro = _typeProducts.GetAll();
        return View(typrPro);
    }

    [HttpPost]
    public IActionResult TypeProduct(TypeProductView pro)
    {
        var Type = _typeProducts.GetAll();
        var typrPro = new TypeProductView()
        {
            lstTypePro = Type,
            TypeProduct = new TypeProduct()
        };

        foreach (var t in Type)
            if (pro.TypeProduct != null && pro.TypeProduct.Name.ToLower().Trim() == t.Name.ToLower().Trim())
            {
                TempData["ErrorMessage"] = "Loại sản phẩm nãy đã tồn tại";
                return View(pro);
            }

        if (_typeProducts.add(pro.TypeProduct))
        {
            TempData["SuccessMessage"] = "Thêm thành công";
            return RedirectToAction("TypeProduct");
        }

        return RedirectToAction("Error");
    }

    public IActionResult Login()
    {
        var userid = HttpContext.Session.GetString("UserId");
        if (userid != null)
        {
           var user = _userService.getUserById(Guid.Parse(userid));
        }
        return RedirectToAction("Login", "Login");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    public IActionResult Error()
    {
        ViewBag.Layout = "~/Views/Shared/_AdminLayout.cshtml";
        return View();
    }
}