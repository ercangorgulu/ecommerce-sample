using ECommerce.Application.ViewModels;
using ECommerce.Sample.ConsoleClient.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Sample.ConsoleClient.Services.Interpreting
{
    public class CreateProductInterpreter : BaseInterpreter<CreateProductOptions>
    {
        public override async Task<int> InterpretAsync(CreateProductOptions options, HttpClient client)
        {
            var request = new ProductViewModel
            {
                Code = options.Code,
                Price = options.Price,
                Stock = options.Stock
            };
            var response = await client.PostAsJsonAsync("/api/product", request);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error on product creating with the code {(int)response.StatusCode}");
                return -2;
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductViewModel>>();
            var product = result.Data;
            Console.WriteLine($"Product created; " +
                $"code {product.Code}, " +
                $"price {product.Price:N0}, " +
                $"stock {product.Stock}");

            return 0;
        }
    }
}
