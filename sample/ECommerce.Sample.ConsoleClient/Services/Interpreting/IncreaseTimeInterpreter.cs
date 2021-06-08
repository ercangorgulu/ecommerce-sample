using ECommerce.Domain.Services;
using ECommerce.Sample.ConsoleClient.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerce.Sample.ConsoleClient.Services.Interpreting
{
    public class IncreaseTimeInterpreter : BaseInterpreter<IncreaseTimeOptions>
    {
        public override Task<int> InterpretAsync(IncreaseTimeOptions options, HttpClient _)
        {
            DateTimeService.SetTime(DateTimeService.Current.AddHours(options.Hours));
            Console.WriteLine($"Time is {DateTimeService.Current:HH:mm}");
            return Task.FromResult(0);
        }
    }
}
