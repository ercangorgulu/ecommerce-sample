using ECommerce.Sample.ConsoleClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerce.Sample.ConsoleClient.Services.Interpreting
{
    public class MainInterpreter : BaseInterpreter
    {
        public override async Task<int> InterpretAsync(IEnumerable<string> args, HttpClient client)
        {
            return await HandleAsync<CommandLineOptions>(args, async options =>
            {
                IInterpreter interpreter = options.Command switch
                {
                    "create_product" => new CreateProductInterpreter(),
                    "create_campaign" => new CreateCampaignInterpreter(),
                    "create_order" => new CreateOrderInterpreter(),
                    "get_product_info" => new GetProductInfoInterpreter(),
                    "increase_time" => new IncreaseTimeInterpreter(),
                    "get_campaign_info" => new GetCampaignInfoInterpreter(),
                    _ => throw new NotImplementedException(options.Command)
                };

                if (interpreter == null)
                {
                    return -1;
                }

                return await interpreter.InterpretAsync(args.Skip(1), client);
            });
        }
    }
}
