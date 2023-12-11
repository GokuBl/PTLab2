using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace PTLab2_testASP.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

    }
}
