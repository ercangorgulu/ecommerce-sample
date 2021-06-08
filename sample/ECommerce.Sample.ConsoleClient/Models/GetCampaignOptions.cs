using CommandLine;

namespace ECommerce.Sample.ConsoleClient.Models
{
    public class GetCampaignOptions
    {
        [Value(index: 0, Required = true)]
        public string Name { get; set; }
    }
}
