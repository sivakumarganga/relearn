using Polly;
using Polly.Retry;
namespace Relearn.DotNet.PollyPlay
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Application started");
            //await RetryPolicyLearn.MainAsync();
            //await RetryPolicyWithCircuitBreaker.MainAsync();
            await RetryPolicyWithTimeOutAndCircuitBreaker.MainAsync();
            Console.Read();
        }
    }
}
