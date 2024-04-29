using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MebelWeb.Models
{
    public class Zarplata
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Сотрдуник")]
        public int? employee { get; set; }
        public Employee? Employee { get; set; }
        [Required]
        [Display(Name = "Год")]
        public int Year { get; set; }
        [Required]
        [Display(Name = "Месяц")]
        public int Month { get; set; }
        [Display(Name = "В закупках")]
        public int ForPurchase { get; set; }
        [Display(Name = "В производстве")]
        public int ForProduction { get; set; }
        [Display(Name = "В продажах")]
        public int ForSale { get; set; }
        [Display(Name = "Всего")]
        public int Common { get; set; }
        [Display(Name = "Оклад")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal oklad { get; set; }
        [Display(Name = "Бонус")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Bonus { get; set; }
        [Display(Name = "Общая зарплата")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal General { get; set; }
        [Display(Name = "Выдано")]
        public bool given { get; set; }
    }
}
