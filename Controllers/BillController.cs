using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZestWeb.IService;
using ZestWeb.Models;
using ZestWeb.Service;

namespace ZestWeb.Controllers
{
    public class BillController : Controller
    {
        private readonly IBillDetailsService _billDetailsService;
        private readonly IBillService _billService;
        public BillController()
        {
            _billService = new BillService();
            _billDetailsService = new BillDetailsService();
        }
        public IActionResult MyPurchase()
        {
            string userID = HttpContext.Session.GetString("UserId");
            var bill = _billService.GetAll().Where(c =>c.IdUser == Guid.Parse(userID) && c.Status == 0);
            if (userID == null) return View(null);
            return View(bill);
        }

        public IActionResult HoanThanh()
        {
            string userID = HttpContext.Session.GetString("UserId");
            var bill = _billService.GetAll().Where(c => c.IdUser == Guid.Parse(userID) && c.Status == 1);
            if (userID == null) return View(null);
            return View(bill);
        }
        public IActionResult BillSucces(Guid id)
        {
            var bill = _billService.getBillById(id);
            bill.Status = 1;
            _billService.update(bill);
            return RedirectToAction("MyPurchase");
        }


    }
}
