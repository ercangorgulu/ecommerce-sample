using CommandLine;

namespace ECommerce.Sample.ConsoleClient.Models
{
    public class IncreaseTimeOptions
    {
        [Value(index: 0, Required = true)]
        public int Hours { get; set; }
    }
}
