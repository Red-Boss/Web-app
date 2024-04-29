using System.ComponentModel.DataAnnotations.Schema;

namespace MebelWeb.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public int doljnost { get; set; }
        public Doljnost Doljnost { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Oklad { get; set; }
        public string Adres { get; set; }
        public string Number { get; set; }

        public List<Zarplata> Zarplata { get; set; }
        public List<Zakupka_syria> Zakupka_syria { get; set; }
        public List<Proizvodstvo_produkci> Proizvodstvo_produkci { get; set; }
        public List<Prodaja_produkci> Prodaja_produkci { get; set; }
    }
}
