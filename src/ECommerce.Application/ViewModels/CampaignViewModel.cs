using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.ViewModels
{
    public class CampaignViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product Code is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Product Code")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "The EndDate is Required")]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(255)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The PriceManipulationLimit is Required")]
        [Range(0, 100)]
        [DisplayName("PriceManipulationLimit")]
        public double PriceManipulationLimit { get; set; }

        [Required(ErrorMessage = "The TargetSalesCount is Required")]
        [Range(0, int.MaxValue)]
        [DisplayName("Target Sales Count")]
        public int TargetSalesCount { get; set; }
    }
}
