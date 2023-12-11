using System.ComponentModel.DataAnnotations;

namespace PTLab2_testASP.Models
{
    public class Purchase
    {
        [Key]
        public int ID { get; set; }
        public string Person { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
    }
}

