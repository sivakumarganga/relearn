using Polly.Retry;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.PollyPlay
{
    public static class RetryPolicyLearn
    {
        public static async Task MainAsync()
        {
            Console.WriteLine("########## Retry Policy ##########");

            RetryPolicy retryPolicyException = Policy.Handle<Exception>()
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(2), (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry count ({nameof(Exception)}): {retryCount}");
                });
            //Sample execution with retry policy
            retryPolicyException.Execute(() =>
            {
                Console.WriteLine($"Hello, World! {DateTime.Now}");
                throw new Exception("Something went wrong");
            });

            RetryPolicy retryPolicyForTimeout = Policy.Handle<TimeoutException>()
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(10), (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry count ({nameof(TimeoutException)}): {retryCount}");
                });
            //Sample execution with retry policy for TimeoutException
            retryPolicyForTimeout.Execute(() =>
            {
                Console.WriteLine($"Hello, World! {DateTime.Now}");
                throw new TimeoutException("Something went wrong");
            });

            //Wrap both retry policies
            var retryPolicy = Policy.Wrap(retryPolicyException, retryPolicyForTimeout);
            int count = 0;
            // Sample execution with wrapped retry policy
            // This will retry 3 times for Exception and 3 times for TimeoutException
            // Each policy maintains its own retry count
            retryPolicy.Execute(() =>
            {
                Console.WriteLine($"Hello, World! {DateTime.Now}");
                count++;
                if (count == 3)
                    throw new TimeoutException("Something went wrong");

                throw new Exception("Something went wrong");

            });
            await Task.CompletedTask;
        }
    }
}
