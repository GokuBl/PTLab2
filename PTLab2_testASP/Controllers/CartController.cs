using Microsoft.AspNetCore.Mvc;
using PTLab2_testASP.Data;
using PTLab2_testASP.Interfaces_;
using PTLab2_testASP.Models;
using System.Linq;
using System.Text.Json;

namespace PTLab2_testASP.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository db)
        {
            _cartRepository = db;
        }

        public IActionResult Index()
        {
            List<int> productIdList;
            try
            {
                productIdList = JsonProductIDList(Request.Form["ProductIDList"]);

            }
            catch
            { 
                List<Product> pr = new List<Product>(); 
                return View(pr);
            }

            List<Product> objectProductList = _cartRepository.GetAllProduct();

            var resultList = productIdList?.Join(objectProductList,
                                                        curp => curp,
                                                        p => p.ID,
                                                        (curp, p) =>
                                                        new
                                                        {
                                                            ID = p.ID,
                                                            Name = p.Name,
                                                            Price = p.Price
                                                        });
            ViewData["productIdList"] = JsonSerializer.Serialize(resultList.Select(p => p.ID));
            ViewData["TotalPrice"] = resultList.Sum(p=>p.Price);
            return View(resultList);
        }

        [HttpPost]
        public IActionResult Buy()
        {
            List<int> productIdList = JsonProductIDList(Request.Form["ProductIDList"]);
            string fio = Request.Form["FIO"];
            string address = Request.Form["Address"];

            Purchase purchase = new Purchase
            {
                Date = DateTime.Now,
                Address = address,
                Person = fio
            };
            _cartRepository.AddPurchase(purchase);
            int lastId = _cartRepository.GetLastIDPurchase();


            for (int i = 0; i < productIdList.Count; i++)
            {
                ShopCart shopCart = new ShopCart
                {
                    ProductId = productIdList[i],
                    PurchaseId = lastId
                };
                _cartRepository.AddShopCart(shopCart);                                
            }

            return RedirectToAction("Index");
        }

        private List<int> JsonProductIDList(string request)
        {
            var productIds = request;
            List<int> productIdList = new List<int>();
            productIdList = JsonSerializer.Deserialize<List<int>>(productIds);
            return productIdList;
        }
    }
}
