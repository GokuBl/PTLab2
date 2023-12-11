using PTLab2_testASP.Models;

namespace PTLab2_testASP.Interfaces_
{
    public interface IHomeRepository
    {
        bool Add(Product product);
        bool Update(Product product);
        bool Delete(Product product);
        bool Save();

        List<Product> GetAll();
    }
}
