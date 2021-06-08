using CommandLine;

namespace ECommerce.Sample.ConsoleClient.Models
{
    public class CommandLineOptions
    {
        [Value(index: 0, Required = true, HelpText = "Command to run")]
        public string Command { get; set; }
    }
}
