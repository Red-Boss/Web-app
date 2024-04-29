using System.ComponentModel.DataAnnotations.Schema;

namespace MebelWeb.Models
{
    public class Syrio
    {
        public int ID { get; set; }
        public string Naimenovanie_materiala { get; set; }
        public int edinica_izmerenia { get; set; }
        public Edinica_izmerenia Edinica_izmerenia { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Count { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Sum { get; set; }
        public List<Ingridient> Ingridient { get; set; }
        public List<Zakupka_syria> Zakupka_syria { get; set; }
    }
}
