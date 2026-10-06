using System.ComponentModel.DataAnnotations;

namespace Tanjente_Midterm_Store.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        [Range(0.01, 999999999)]
        public decimal Price { get; set; }

        public string Category { get; set; } = "";
    }
}