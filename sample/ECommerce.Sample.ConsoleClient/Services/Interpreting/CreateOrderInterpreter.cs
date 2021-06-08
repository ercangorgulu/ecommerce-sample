using ECommerce.Application.ViewModels;
using ECommerce.Sample.ConsoleClient.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Sample.ConsoleClient.Services.Interpreting
{
    public class CreateOrderInterpreter : BaseInterpreter<CreateOrderOptions>
    {
        public override async Task<int> InterpretAsync(CreateOrderOptions options, HttpClient client)
        {
            var request = new OrderViewModel
            {
                ProductCode = options.ProductCode,
                Quantity = options.Quantity
            };
            var response = await client.PostAsJsonAsync("/api/order", request);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error on order creating with the code {(int)response.StatusCode}");
                return -2;
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<OrderViewModel>>();
            var order = result.Data;
            Console.WriteLine($"Order created; " +
                $"product {order.ProductCode}, " +
                $"quantity {order.Quantity}");

            return 0;
        }
    }
}
