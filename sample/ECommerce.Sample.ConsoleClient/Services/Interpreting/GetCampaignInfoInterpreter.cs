using ECommerce.Application.ViewModels;
using ECommerce.Sample.ConsoleClient.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Sample.ConsoleClient.Services.Interpreting
{
    public class GetCampaignInfoInterpreter : BaseInterpreter<GetCampaignOptions>
    {
        public override async Task<int> InterpretAsync(GetCampaignOptions options, HttpClient client)
        {
            var response = await client.GetAsync($"/api/campaign/name/{options.Name}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error on getting campaign info with the code {(int)response.StatusCode}");
                return -2;
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CampaignInfoViewModel>>();
            var campaignInfo = result.Data;
            Console.WriteLine($"Campaign {campaignInfo.Name} info; " +
                $"Status {campaignInfo.Status}, " +
                $"Target Sales {campaignInfo.TargetSalesCount}, " +
                $"Total Sales {campaignInfo.TotalSalesCount}, " +
                //$"Turnover {campaignInfo.Turnover}, " +//TODO:how it is calculated?
                $"Average Item Price {campaignInfo.AverageItemPrice:N2}");

            return 0;
        }
    }
}
