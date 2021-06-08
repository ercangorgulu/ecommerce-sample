using ECommerce.Application.ViewModels;
using ECommerce.Sample.ConsoleClient.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Sample.ConsoleClient.Services.Interpreting
{
    public class GetProductInfoInterpreter : BaseInterpreter<GetProductOptions>
    {
        public override async Task<int> InterpretAsync(GetProductOptions options, HttpClient client)
        {
            var response = await client.GetAsync($"/api/product/code/{options.Code}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error on getting product info with the code {(int)response.StatusCode}");
                return -2;
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductViewModel>>();
            var product = result.Data;
            Console.WriteLine($"Product {product.Code} info; " +
                $"price {product.Price:N0}, " +
                $"stock {product.Stock}");

            return 0;
        }
    }
}
