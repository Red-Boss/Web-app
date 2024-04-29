using System.ComponentModel.DataAnnotations.Schema;

namespace MebelWeb.Models
{
    public class Produkcia
    {
        public int ID { get; set; }
        public string Naimenovanie { get; set; }
        public int edinica_izmerenia { get; set; }
        public Edinica_izmerenia Edinica_izmerenia { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Count { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Sum { get; set; }
        public List<Proizvodstvo_produkci> Proizvodstvo_produkci { get; set; }
        public List<Prodaja_produkci> Prodaja_produkci { get; set; }
    }
}
