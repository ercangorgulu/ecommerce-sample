using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product Code is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Product Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "The Price is Required")]
        [Range(0, double.MaxValue)]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Stock is Required")]
        [Range(0, double.MaxValue)]
        [DisplayName("Stock")]
        public int Stock { get; set; }
    }
}
