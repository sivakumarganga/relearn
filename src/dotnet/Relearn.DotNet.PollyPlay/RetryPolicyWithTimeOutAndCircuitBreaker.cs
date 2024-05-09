using Polly;

namespace Relearn.DotNet.PollyPlay
{
    public static class RetryPolicyWithTimeOutAndCircuitBreaker
    {
        public static async Task MainAsync()
        {
            Console.WriteLine("########## Retry Policy with TimeOut and Circuit Breaker ##########");
            //This policy will retry 10 times with 2 seconds delay
            var retryPolicy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(2), (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry count {DateTime.Now.ToShortTimeString()}:({exception.GetType().Name}): #{retryCount} Message:{exception.Message}");
                });
            // This policy will open the circuit if 2 exceptions occur and blocks all the retries for 10 seconds
            var circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10), (exception, timeSpan, context) =>
                {
                    Console.WriteLine($"Circuit Breaker Opened: {DateTime.Now.ToShortTimeString()}");
                }, (context) =>
                {
                    Console.WriteLine("Circuit Breaker Reset");
                });

            var timeoutPolicy = Policy.TimeoutAsync(5, (context, timeSpan, task) =>
            {
                Console.WriteLine($" ******** Timeout Exception: {timeSpan}");
                return task;
            });

            var policy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy, timeoutPolicy);

            int count = 1;
            // Sample execution with wrapped retry policy
            // This will retry 3 times for Exception and 3 times for TimeoutException
            // Each policy maintains its own retry count
            await policy.ExecuteAsync(async () =>
            {
               
                await Task.CompletedTask;

                try
                {
                    Console.WriteLine($"Executing the task Count:{count} at: {DateTime.Now}");
                    if (count < 10)
                    {
                        Task.Delay(1000).Wait();
                        throw new Exception($"This is exception message :{count}");
                    }
                       

                    if (count == 10)
                    {
                        Task.Delay(20000).Wait();
                       // throw new TimeoutException($"This is time out exception message:{count}");
                    }
                        

                    if (count == 11)
                        return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Exception thrown for #: {count}");
                    throw ex.InnerException ?? ex;
                }
                finally
                {
                    count++;
                }
            });
        }
    }
}
