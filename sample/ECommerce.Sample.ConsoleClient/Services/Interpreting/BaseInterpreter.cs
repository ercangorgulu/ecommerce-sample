using CommandLine;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerce.Sample.ConsoleClient.Services.Interpreting
{
    public abstract class BaseInterpreter<T> : BaseInterpreter
    {
        public override async Task<int> InterpretAsync(IEnumerable<string> args, HttpClient client)
            => await HandleAsync<T>(args, options => InterpretAsync(options, client));

        public abstract Task<int> InterpretAsync(T options, HttpClient client);
    }

    public abstract class BaseInterpreter : IInterpreter
    {
        protected static readonly Parser CustomParser = new Parser(x =>
        {
            x.AutoHelp = false;
            x.AutoVersion = false;
        });

        protected static async Task<int> HandleAsync<T>(IEnumerable<string> args, Func<T, Task<int>> action)
        {
            return await CustomParser.ParseArguments<T>(args)
                .MapResult(async (T options) =>
                {
                    try
                    {
                        return await action(options);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return -3;
                    }
                },
                errs =>
                {
                    foreach (var err in errs)
                    {
                        Console.WriteLine(err.Tag);
                    }
                    return Task.FromResult(-1);
                });
        }

        public abstract Task<int> InterpretAsync(IEnumerable<string> args, HttpClient client);
    }
}
