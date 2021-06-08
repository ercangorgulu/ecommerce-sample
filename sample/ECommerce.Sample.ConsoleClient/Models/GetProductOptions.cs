using CommandLine;

namespace ECommerce.Sample.ConsoleClient.Models
{
    public class GetProductOptions
    {
        [Value(index: 0, Required = true)]
        public string Code { get; set; }
    }
}
