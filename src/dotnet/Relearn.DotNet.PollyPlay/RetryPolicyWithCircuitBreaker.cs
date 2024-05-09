using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.PollyPlay
{
    public static class RetryPolicyWithCircuitBreaker
    {
        public static async Task MainAsync()
        {
            Console.WriteLine("########## Retry Policy with Circuit Breaker ##########");
            var retryPolicy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(2), (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry count ({exception.GetType().Name}): #{retryCount} Message:{exception.Message}");
                });

            var circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10), (exception, timeSpan, context) =>
                {
                    Console.WriteLine($"Circuit Breaker Opened: {exception.Message}");
                }, (context) =>
                {
                    Console.WriteLine("Circuit Breaker Reset");
                });

            var policy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

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
                    if (count < 5)
                        throw new Exception($"This is time out exception message :{count}");

                    if (count == 5)
                        throw new TimeoutException($"This is time out exception message:{count}");

                    if (count == 7)
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
            /*
             ########## Retry Policy with Circuit Breaker ##########
                Executing the task Count:1 at: 08-May-2024 07:50:55 PM
                 Exception thrown for #: 1
                Retry count (Exception): #1 Message:This is time out exception message :1
                Executing the task Count:2 at: 08-May-2024 07:50:57 PM
                 Exception thrown for #: 2
                Circuit Breaker Opened: This is time out exception message :2
                Retry count (Exception): #2 Message:This is time out exception message :2
                Retry count (BrokenCircuitException): #3 Message:The circuit is now open and is not allowing calls.
                Retry count (BrokenCircuitException): #4 Message:The circuit is now open and is not allowing calls.
                Retry count (BrokenCircuitException): #5 Message:The circuit is now open and is not allowing calls.
                Retry count (BrokenCircuitException): #6 Message:The circuit is now open and is not allowing calls.
                Executing the task Count:3 at: 08-May-2024 07:51:07 PM
                 Exception thrown for #: 3
                Circuit Breaker Opened: This is time out exception message :3
                Retry count (Exception): #7 Message:This is time out exception message :3
                Retry count (BrokenCircuitException): #8 Message:The circuit is now open and is not allowing calls.
                Retry count (BrokenCircuitException): #9 Message:The circuit is now open and is not allowing calls.
                Retry count (BrokenCircuitException): #10 Message:The circuit is now open and is not allowing calls.
                Unhandled exception. Polly.CircuitBreaker.BrokenCircuitException: The circuit is now open and is not allowing calls.
                 ---> System.Exception: This is time out exception message :3
             */
        }
    }
}
