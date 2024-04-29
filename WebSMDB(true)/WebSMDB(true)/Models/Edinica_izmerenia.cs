namespace MebelWeb.Models
{
    public class Edinica_izmerenia
    {
        public int ID { get; set; }
        public string Naimenovanie { get; set; }
        public List<Produkcia> Produkcia { get; set; }
        public List<Syrio> Syrio { get; set; }

    }
}
