using System.ComponentModel.DataAnnotations;

namespace PTLab2_testASP.Models
{
    public class ShopCart
    {
        [Key]
        public int ID { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }

    }
}
