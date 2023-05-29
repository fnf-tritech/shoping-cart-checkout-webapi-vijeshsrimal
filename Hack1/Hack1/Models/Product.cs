using System.ComponentModel.DataAnnotations;

namespace Hack1.Models
{
    public class Product
    {
        [Key]
        public char Item { get; set; }
        public int Price { get; set; }
        public string? Offer { get; set; }
    }
}
