using Microsoft.AspNetCore.Mvc;
using PTLab2_testASP.Data;
using PTLab2_testASP.Interfaces_;
using PTLab2_testASP.Models;
using System.Diagnostics;

namespace PTLab2_testASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;
        public HomeController(IHomeRepository db) 
        {
            _homeRepository = db;
        }


        public IActionResult Index()
        {
            List<Product> objectProductList = _homeRepository.GetAll();
            return View(objectProductList);
            
        }


        [HttpGet]
        public IActionResult Buy(string ProductIDList)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buy(Purchase purchase)
        {
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
