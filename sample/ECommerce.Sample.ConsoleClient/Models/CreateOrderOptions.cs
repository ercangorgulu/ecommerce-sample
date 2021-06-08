using CommandLine;

namespace ECommerce.Sample.ConsoleClient.Models
{
    public class CreateOrderOptions
    {
        [Value(index: 0, Required = true)]
        public string ProductCode { get; set; }

        [Value(index: 1, Required = true)]
        public int Quantity { get; set; }
    }
}
