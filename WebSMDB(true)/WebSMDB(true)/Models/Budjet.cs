using System.ComponentModel.DataAnnotations.Schema;

namespace MebelWeb.Models
{
    public class Budjet
    {
        public int ID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal budjet { get; set; }
        public int bonus { get; set; }
        public int pr_prodaji { get; set; }
    }
}
