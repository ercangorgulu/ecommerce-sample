using ECommerce.Application.Enums;
using System.ComponentModel;

namespace ECommerce.Application.ViewModels
{
    public class CampaignInfoViewModel
    {
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Product Code")]
        public string ProductCode { get; set; }

        [DisplayName("Status")]
        public CampaignStatus Status { get; set; }

        [DisplayName("Target Sales Count")]
        public int TargetSalesCount { get; set; }

        [DisplayName("Total Sales Count")]
        public int TotalSalesCount { get; set; }

        //TODO: how it is calculated?
        [DisplayName("Turnover")]
        public int Turnover { get; set; }

        [DisplayName("Average Item Price")]
        public decimal AverageItemPrice { get; set; }
    }
}
