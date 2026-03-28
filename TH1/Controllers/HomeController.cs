using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TH1.Models;

namespace TH1.Controllers
{
    public class HomeController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index()
        {
            var ds = db.TDanhMucSps.Where(x => x.MaDtNavigation.TenLoai == "Doanh nhân").Include(x => x.MaDtNavigation).ToList();
            return View(ds);
        }

        public IActionResult GetChatLieu()
        {
            var ds = db.TChatLieus.ToList();
            return PartialView(ds);
        }

        // Action mới để lấy sản phẩm theo chất liệu
        public IActionResult GetProductByChatLieu(string maChatLieu)
        {
            var products = db.TDanhMucSps
                .Where(x => x.MaChatLieu == maChatLieu)
                .ToList();
            
            return PartialView("_ProductList", products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
