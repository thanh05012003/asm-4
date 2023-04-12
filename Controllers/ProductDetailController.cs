
using Microsoft.AspNetCore.Mvc;
using ZestWeb.IService;
using ZestWeb.Service;
using ZestWeb.ViewModels;

namespace ZestWeb.Controllers;

public class ProductDetailController : Controller
{
    private readonly IProductDetailsService _proDetailsService;
    private readonly IProductService _productService;
    public ProductDetailController()
    {
        _proDetailsService = new ProductDetailsService();
        _productService = new ProductService();
    }
    //public IActionResult Index()
    //{
    //    return View();
    //}
    [HttpGet]
    public IActionResult AddProducts(Guid id)
    {
        ViewBag.products = _productService.GetAll().FirstOrDefault(c => c.Id == id);
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddProducts(ProductsView proDetails, IFormFile file)
    {
           
        proDetails.ImageUrl = "";
        if (file != null && file.Length > 0)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            proDetails.ImageUrl = "/img/" + fileName;
            //proDetails.IdProduct = ViewBag.pro.Id;
        }

        if (_proDetailsService.add(proDetails)) return RedirectToAction("ShowListProduct", "Product");
        else
            return Content("Fail");
    }
    public IActionResult Details(Guid id)
    {
        var products = _proDetailsService.getById(id);
        if (products!=null)
        {
            return View(products);
        }

        return RedirectToAction("Home", "Product");
    }

    [HttpPost]
    public IActionResult Details(ProductsView pro)
    {
        return RedirectToAction("AddToCart", "Cart", pro);
    }
    public IActionResult EditProDetails(Guid id)
    {
        var products = _proDetailsService.getById(id);
        ViewBag.idDetails = id;
        return View(products);
    }
    [HttpPost]
    public async Task<IActionResult> EditProDetails(ProductsView product, IFormFile file)
    {
        product.ImageUrl = "";
        var pr = _productService.viewProductById(product.IdProduct);
        if (file != null && file.Length > 0)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            product.ImageUrl = "/img/" + fileName;
        }

        if (_proDetailsService.update(product))
            return RedirectToAction("ShowListProduct", "Product");
        else
            return RedirectToAction("ERROR", "Home");
    }

    public IActionResult DeleteProDetails(Guid id)
    {
        if (_proDetailsService.delete(id)) return RedirectToAction("ShowListProduct","Product");
        return Content("Fail");
    }
}