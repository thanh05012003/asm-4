
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.Service;

namespace ZestWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController()
        {
            _userService = new UserService();
           _roleService = new RoleService();
        }

        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login","Login");
            }
            var user = _userService.getUserById(Guid.Parse(HttpContext.Session.GetString("UserId")));
            
            if (HttpContext.Session.GetString("UserId")!= null && user!=null)
            {
                return View(user);
            }
            
            return RedirectToAction("Home", "Product");
        }
        public IActionResult ShowListAccount()
        {
            var user = _userService.GetAll();
            ViewBag.Role = _roleService.GetAll();
            return View(user);
        }
        public IActionResult AddAccount()
        {
            ViewBag.Role = _roleService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult AddAccount(User user)
        {
            user.Id = Guid.NewGuid();
            user.Status = 1;
            if (_userService.add(user)) return RedirectToAction("ShowListAccount", "User");
            return BadRequest();
        }

        public IActionResult DeleteAccount(Guid id)
        {
            if (_userService.delete(id)) return RedirectToAction("ShowListAccount");
            else return NotFound();
        }
        public IActionResult Error()
        {
            return BadRequest();
        }
        public IActionResult MyProfile()
        {
            string userId = HttpContext.Session.GetString("UserId");
            var user = _userService.getUserById(Guid.Parse(userId));
            if (user!=null)
            {
                return View(user);
            }

            return RedirectToAction("Error", "Home");
        }
    }
}
