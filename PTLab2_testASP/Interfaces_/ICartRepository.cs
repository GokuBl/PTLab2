using PTLab2_testASP.Models;

namespace PTLab2_testASP.Interfaces_
{
    public interface ICartRepository
    {
        bool AddPurchase(Purchase purchase);
        bool UpdatePurchase(Purchase purchase);
        bool DeletePurchase(Purchase purchase);

        bool AddShopCart(ShopCart shopCart);
        bool UpdateShopCart(ShopCart shopCart);
        bool DeleteShopCart(ShopCart shopCart);
        bool Save();

        List<Product> GetAllProduct();
        int GetLastIDShopCart();
        int GetLastIDPurchase();
    }
}
