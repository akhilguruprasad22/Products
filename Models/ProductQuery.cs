using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class ProductQuery : Product
    {
        [StringLength(3)]
        [RegularExpression("AND|OR", ErrorMessage = "Invalid Condition")]
        public string Condition1 { get; set; }
        
        [StringLength(3)]
        [RegularExpression("AND|OR", ErrorMessage = "Invalid Condition")]
        public string Condition2 { get; set; }

        [StringLength(3)]
        [RegularExpression("AND|OR", ErrorMessage = "Invalid Condition")]
        public string Condition3 { get; set; }

        [StringLength(3)]
        [RegularExpression("AND|OR", ErrorMessage = "Invalid Condition")]
        public string Condition4 { get; set; }
    }
}
