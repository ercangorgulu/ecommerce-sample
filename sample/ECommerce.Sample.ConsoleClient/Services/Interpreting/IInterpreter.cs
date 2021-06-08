using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerce.Sample.ConsoleClient.Services.Interpreting
{
    public interface IInterpreter
    {
        public Task<int> InterpretAsync(IEnumerable<string> args, HttpClient client);
    }
}
