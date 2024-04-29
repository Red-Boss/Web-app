using System.ComponentModel.DataAnnotations.Schema;

namespace MebelWeb.Models
{
    public class Zakupka_syria
    {
        public Zakupka_syria()
        {
            // Установка check по умолчанию в false
            check = false;
        }
        public int ID { get; set; }
        public int syrio { get; set; }
        public Syrio Syrio { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Count { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Sum { get; set; }
        public DateTime data { get; set; }
        public int employee { get; set; }
        public Employee Employee { get; set; }
        public bool check { get; set; }
    }
}
