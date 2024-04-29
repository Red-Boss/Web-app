using System.ComponentModel.DataAnnotations.Schema;

namespace MebelWeb.Models
{
    public class Proizvodstvo_produkci
    {
        public Proizvodstvo_produkci() { 
            check = false;
        }
        public int ID { get; set; }
        public int produkcia { get; set; }
        public Produkcia Produkcia { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Count { get; set; }
        public DateTime data { get; set; }
        public int employee { get; set; }
        public Employee Employee { get; set; }
        public bool check { get; set; }
    }
}
