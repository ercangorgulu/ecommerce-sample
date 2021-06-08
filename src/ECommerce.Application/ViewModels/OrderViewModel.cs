using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.ViewModels
{
    public class OrderViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product Code is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Product Code")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "The Quantity is Required")]
        [Range(0, int.MaxValue)]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }

        [DisplayName("Campaign Id")]
        public Guid? CampaignId { get; set; }
    }
}
