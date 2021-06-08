using ECommerce.Domain.Services;
using ECommerce.Infra.CrossCutting.Identity.Models.AccountViewModels;
using ECommerce.Sample.ConsoleClient.Models;
using ECommerce.Sample.ConsoleClient.Services.Interpreting;
using ECommerce.Services.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Sample.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Initializing...");
            DateTimeService.SetTime(DateTime.Now.Date);
            var factory = new CustomWebApplicationFactory<Startup>();
            var client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            var token = await GetTokenAsync(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            while (true)
            {
                Console.Write("Enter index [1,5]: ");
                if (!int.TryParse(Console.ReadLine(), out var index) || index < 1 || index > 5)
                {
                    Console.Write("Invalid index. ");
                    continue;
                }
                await RunScenario(index, client);
                break;
            }
            Console.ReadLine();
        }

        private static async Task RunScenario(int index, HttpClient client)
        {
            try
            {
                var interpreter = new MainInterpreter();
                var lines = await File.ReadAllLinesAsync($"Scenarios/Scenario{index}.txt");
                foreach (var line in lines)
                {
                    var result = await interpreter.InterpretAsync(line.Split(' '), client);
                    if (result != 0)
                    {
                        Console.WriteLine("Done with errors!");
                        return;
                    }
                }
                Console.WriteLine("Done running!");
            }
            catch (Exception ex)
            {

            }
        }

        private static async Task<TokenViewModel> GetTokenAsync(HttpClient client)
        {
            var createRequest = new RegisterViewModel
            {
                Email = "ercangorgulu@yandex.com",
                Password = "123456abcdefABCDEF!@#$%^"
            };
            createRequest.ConfirmPassword = createRequest.Password;
            var createResponse = await client.PostAsJsonAsync("/api/Account/register", createRequest);
            var createResult = await createResponse.Content.ReadAsStringAsync();

            var loginRequest = new LoginViewModel
            {
                Email = createRequest.Email,
                Password = createRequest.Password
            };
            var loginResponse = await client.PostAsJsonAsync("/api/Account/login", loginRequest);
            var loginResult = await loginResponse.Content.ReadFromJsonAsync<ApiResponse<TokenViewModel>>();
            return loginResult.Data;
        }
    }
}
