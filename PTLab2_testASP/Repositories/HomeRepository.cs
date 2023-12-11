using PTLab2_testASP.Data;
using PTLab2_testASP.Models;
using Microsoft.EntityFrameworkCore;
using PTLab2_testASP.Interfaces_;

namespace PTLab2_testASP.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ShopContext _db;
        public HomeRepository(ShopContext db)
        {
            _db = db;
        }


        public bool Add(Product product)
        {
            _db.Products.Add(product);
            return Save();
        }

        public bool Delete(Product product)
        {
            _db.Products.Remove(product);
            return Save();
        }

        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0;
        }

        public bool Update(Product product)
        {
            _db.Products.Update(product);
            return Save();
        }
    }
}
