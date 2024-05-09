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
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(1), (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry count #{retryCount} {DateTime.Now.ToShortTimeString()}:({exception.GetType().Name}):  Message:{exception.Message}");
                });
            // This policy will open the circuit if 2 exceptions occur and blocks all the retries for 10 seconds
            var circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(10), (exception, timeSpan, context) =>
                {
                    Console.WriteLine($"Circuit Breaker Opened: {DateTime.Now.ToShortTimeString()}");
                }, (context) =>
                {
                    Console.WriteLine("Circuit Breaker Reset");
                }, () =>
                {
                    Console.WriteLine("Circuit Breaker Half Open");

                });

            var timeoutPolicy = Policy.TimeoutAsync(1, Polly.Timeout.TimeoutStrategy.Pessimistic, (context, timeSpan, task) =>
            {
                Console.WriteLine($" ******** Timeout Policy: {timeSpan}");
                
                return task;
            });

            var policy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy, timeoutPolicy);

            int count = 1;
            // Sample execution with wrapped retry policy
            // This will retry 3 times for Exception and 3 times for TimeoutException
            // Each policy maintains its own retry count
            await policy.ExecuteAsync(async () =>
            {

                try
                {
                    Console.WriteLine($"Executing the task Count:{count} at: {DateTime.Now}");
                    if (count < 3)
                    {
                        Task.Delay(0).Wait();
                        throw new Exception($"This is exception message :{count}");
                    }


                    if (count ==3)
                    {
                        await Task.Delay(5000);
                        throw new TimeoutException($"This is time out exception message:{count}");
                    }


                    if (count > 3)
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
