using CommandLine;

namespace ECommerce.Sample.ConsoleClient.Models
{
    public class CreateCampaignOptions
    {
        [Value(index: 0, Required = true)]
        public string Name { get; set; }

        [Value(index: 1, Required = true)]
        public string ProductCode { get; set; }

        [Value(index: 3, Required = true)]
        public int Duration { get; set; }

        [Value(index: 4, Required = true)]
        public int Limit { get; set; }

        [Value(index: 5, Required = true)]
        public int TargetSalesCount { get; set; }
    }
}
