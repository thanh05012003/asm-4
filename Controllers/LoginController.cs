
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.Service;
using ZestWeb.ViewModels;

namespace ZestWeb.Controllers;

public class LoginController : Controller
{
    private readonly IUserService _userService;
    private readonly ILogger<LoginController> _logger;
    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
        _userService = new UserService();
    }

 
    public IActionResult Login()
    {
        HttpContext.Session.Remove("UserId");
        return View();
    }

    [HttpPost]
    public IActionResult Login(UserView model)
    {
        var user = _userService.GetAll()
            .FirstOrDefault(c => c.Phone == model.Phone && c.Password == model.Password);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty,"Sai số điện thoại hoặc mật khẩu");
        }
        else if (user.IdRole == Guid.Parse("6d77730e-0138-4258-9803-0a9cafae0aec")) // idRole trùng với id admin
        {
            var jsonData = user.Id.ToString();
            HttpContext.Session.SetString("UserId", jsonData); // gán id user vào session userId
            return RedirectToAction("ShowListProduct", "Product"); // return trang showlistProduct của  admin
        }
        else
        {
            var jsonData = user.Id.ToString();
            HttpContext.Session.SetString("UserId", jsonData); // gán id user vào session userId
            return RedirectToAction("Products", "Product");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        var users = _userService.getUserByPhone(user.Phone);
        if (users == null)
        {
            user.Id = Guid.NewGuid();
            user.IdRole = Guid.Parse("34aa6d76-a519-4e73-9bff-9abe0b2340ac"); // id người dùng
            user.Status = 1;
            if (_userService.add(user)) return RedirectToAction("Home", "Product");
        }
        else
        {
            ModelState.AddModelError("Phone", "Số điện thoại này đã được sử dụng");
        }
        return View(user);
    }
}