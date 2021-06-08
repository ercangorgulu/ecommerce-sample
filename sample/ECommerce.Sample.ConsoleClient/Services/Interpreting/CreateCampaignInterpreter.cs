using ECommerce.Application.ViewModels;
using ECommerce.Domain.Services;
using ECommerce.Sample.ConsoleClient.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Sample.ConsoleClient.Services.Interpreting
{
    public class CreateCampaignInterpreter : BaseInterpreter<CreateCampaignOptions>
    {
        public override async Task<int> InterpretAsync(CreateCampaignOptions options, HttpClient client)
        {
            var request = new CampaignViewModel
            {
                EndDate = DateTimeService.Current.AddHours(options.Duration),
                Name = options.Name,
                PriceManipulationLimit = options.Limit,
                ProductCode = options.ProductCode,
                TargetSalesCount = options.TargetSalesCount
            };
            var response = await client.PostAsJsonAsync("/api/campaign", request);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error on campaign creating with the code {(int)response.StatusCode}");
                return -2;
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CampaignViewModel>>();
            var campaign = result.Data;
            Console.WriteLine($"Campaign created; " +
                $"name {campaign.Name}, " +
                $"product {campaign.ProductCode}, " +
                $"duration {options.Duration}, " +
                $"limit {campaign.PriceManipulationLimit}, " +
                $"target sales count {campaign.TargetSalesCount}");

            return 0;
        }
    }
}
