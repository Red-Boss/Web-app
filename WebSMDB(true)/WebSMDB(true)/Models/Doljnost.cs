namespace MebelWeb.Models
{
    public class Doljnost
    {
        public int ID { get; set; }
        public string name_doljnost { get; set; }

        public List<Employee> Employee { get; set; }
    }
}
