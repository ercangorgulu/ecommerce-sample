using CommandLine;

namespace ECommerce.Sample.ConsoleClient.Models
{
    public class CreateProductOptions
    {
        [Value(index: 0, Required = true)]
        public string Code { get; set; }

        [Value(index: 1, Required = true)]
        public decimal Price { get; set; }

        [Value(index: 2, Required = true)]
        public int Stock { get; set; }
    }
}
