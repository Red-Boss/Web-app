using System.ComponentModel.DataAnnotations.Schema;

namespace MebelWeb.Models
{
    public class Ingridient
    {
        public int ID { get; set; }
        public int produkcia { get; set; }
        public int syrio { get; set; }
        public Syrio Syrio { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Count { get; set; }

    }
}
