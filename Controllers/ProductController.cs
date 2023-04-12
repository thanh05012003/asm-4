
using Microsoft.AspNetCore.Mvc;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.Service;
using ZestWeb.ViewModels;

namespace ASM.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductDetailsService _productDetailsService;
        private readonly ITypeProductsService _typesService;
        private readonly ICartService _cartService;
        private readonly ICartDetailsService _cartDetailsService;
        private readonly IUserService _userService;

        public ProductController()
        {
            _productService = new ProductService();
            _productDetailsService = new ProductDetailsService();
            _typesService = new TypeProductsService();
            _cartService = new CartService();
            _cartDetailsService = new CartsDetailsService();
            _userService = new UserService();
        }

        public IActionResult Home()
        {
           
            var lstPro = _productService.viewProduct();
            ViewBag.TypePro = _typesService.GetAll();
            return View(lstPro);
        }
        public IActionResult Products()
        {
            var lstPro = _productService.viewProduct();
            ViewBag.TypePro = _typesService.GetAll();
            return View(lstPro);
        }
        public IActionResult ShowListProduct()
        {
            var products = _productService.GetAll();
            return View(products);
        }
        public IActionResult Create()
        {
            ViewBag.typePro = _typesService.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductsView products, IFormFile file)
        {
            var pr = _productService.GetAll();
            products.Id = Guid.NewGuid();
            products.ImageUrl = "";
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                products.ImageUrl = "/img/" + fileName;
                products.Status = 1; // trạng thái lúc đầu = 1 (Hoạt động)
            }

            foreach (var item in pr)
            {
                if (products.Name.ToLower().Trim() == item.Name.ToLower().Trim())
                {
                    ModelState.AddModelError(string.Empty, "Tên sản phẩm này đã tồn tại");
                    //return RedirectToAction("ShowListProduct");
                    return View();
                }
            }

            if (_productService.add(products) && _productDetailsService.add(products)) return RedirectToAction("ShowListProduct");
            else
                return Content("Fail");
        }

        public IActionResult ViewLstProDetails(Guid Id)
        {
            var proDetail = _productDetailsService.GetAll().Where(c => c.IdProduct == Id);
            ViewBag.pro = _productService.GetAll().FirstOrDefault(c => c.Id == Id);
            return View(proDetail);
        }
        
        public IActionResult Delete(Guid id)
        {
            if (_productService.delete(id)) return RedirectToAction("ShowListProduct");

            return Content("Fail");
        }
   


        public IActionResult Edit(Guid id)
        {
            var products = _productService.viewProductById(id);
            ViewBag.typePro = _typesService.GetAll();
            ViewBag.type = _typesService.GetAll().FirstOrDefault(c => c.Id == products.IdTypeProduct);
            return View(products);
        }
        [HttpPost]
        public IActionResult Edit(ProductsView pro)
        {
            if (_productService.update(pro))
            {
                return RedirectToAction("ShowListProduct");
            }

            return BadRequest();
        }
    }
}
