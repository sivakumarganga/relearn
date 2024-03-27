using Microsoft.Extensions.DependencyInjection;
using System;

namespace Relearn.DotNet.DI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            TransientDisposablesWithoutDispose();
            Console.Read();
        }

        static void TransientDisposablesWithoutDispose()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ExampleDisposable>();
            services.AddTransient<TransientDisposable>();
            services.AddScoped<ScopedDisposable>();
            services.AddSingleton<SingletonDisposable>();
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            for (int i = 0; i < 10; ++i)
            {

                serviceProvider.GetRequiredService<ExampleDisposable>();
                serviceProvider.GetRequiredService<TransientDisposable>();
                serviceProvider.GetRequiredService<ScopedDisposable>();
                serviceProvider.GetRequiredService<SingletonDisposable>();

            }
            for (int i = 0; i < 10; ++i)
            {
                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    
                    _ = scope.ServiceProvider.GetRequiredService<ExampleDisposable>();
                    _ = scope.ServiceProvider.GetRequiredService<TransientDisposable>();
                    _ = scope.ServiceProvider.GetRequiredService<ScopedDisposable>();
                    _ = scope.ServiceProvider.GetRequiredService<SingletonDisposable>();
                }
               
            }

             serviceProvider.Dispose();
        }
    }

    public sealed class ExampleDisposable : IDisposable
    {
        public ExampleDisposable()
        {
            Console.WriteLine("Object created");
        }
        public void Dispose() => Console.WriteLine($"{nameof(ExampleDisposable)}.Dispose()");
    }

    public sealed class TransientDisposable : IDisposable
    {
        public void Dispose() => Console.WriteLine($"{nameof(TransientDisposable)}.Dispose()");
    }

    public sealed class ScopedDisposable : IDisposable
    {
        public void Dispose() => Console.WriteLine($"{nameof(ScopedDisposable)}.Dispose()");
    }

    public sealed class SingletonDisposable : IDisposable
    {
        public void Dispose() => Console.WriteLine($"{nameof(SingletonDisposable)}.Dispose()");
    }
}