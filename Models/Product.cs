using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        [StringLength(50)]
        public string? ProductName { get; set; }

        [StringLength(1,MinimumLength = 1, ErrorMessage = "Invalid size length")]
        [RegularExpression("d|s|m|l", ErrorMessage = "Invalid Size")]
        [Display(Name = "Product Size")]
        public string? Size { get; set; }

        [RegularExpression("0|100|275|500", ErrorMessage = "Invalid Price")]
        [Display(Name = "Product Price")]
        public int Price { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid Date Type")]
        [Display(Name = "Product Mfg Date")]
        public DateTime MfgDate { get; set; }
        
        [RegularExpression("default|standard|premium|economy", ErrorMessage = "Invalid Category")]
        [Display(Name = "Product Category")]
        public string? Category { get; set;}
    }
}
