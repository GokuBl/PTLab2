using PTLab2_testASP.Data;
using PTLab2_testASP.Interfaces_;
using PTLab2_testASP.Models;

namespace PTLab2_testASP.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ShopContext _db;
        public CartRepository(ShopContext db)
        {
            _db = db;
        }

        public bool AddPurchase(Purchase purchase)
        {
            _db.Purchases.Add(purchase);
            return Save();
        }

        public bool AddShopCart(ShopCart shopCart)
        {
            _db.ShopCarts.Add(shopCart);
            return Save();
        }

        public bool DeletePurchase(Purchase purchase)
        {
            _db.Purchases.Remove(purchase);
            return Save();
        }

        public bool DeleteShopCart(ShopCart shopCart)
        {
            _db.ShopCarts.Remove(shopCart);
            return Save();
        }

        public List<Product> GetAllProduct()
        {
            return _db.Products.ToList();

        }

        public List<Purchase> GetAllPurchase()
        {
            return _db.Purchases.ToList();
        }

        public int GetLastIDPurchase()
        {
            return _db.Purchases.OrderByDescending(x => x.ID).First().ID;
        }

        public int GetLastIDShopCart()
        {
            var t = _db.ShopCarts;
            Console.WriteLine(t.First());
            return 1;
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0;
        }

        public bool UpdatePurchase(Purchase purchase)
        {
            _db.Purchases.Update(purchase);
            return Save();
        }

        public bool UpdateShopCart(ShopCart shopCart)
        {
            _db.ShopCarts.Update(shopCart);
            return Save();
        }
    }
}
