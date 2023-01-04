using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.Os.DeadLockApp
{
    public static class Deadlock
    {
        private static readonly object _lock1 = new();
        private static readonly object _lock2 = new();

 
        public async static Task LockThread(CancellationToken cancellationToken)
        {
            var task1 = Task.Run(() =>
            {
                Console.WriteLine("Acquiring lock for _lock1 in Task1");
                lock (_lock1)
                {
                    Console.WriteLine("Acquired lock for _lock1 in Task1");
                    Thread.Sleep(1000);
                    Console.WriteLine("Waiting to lock on _lock2 in Task1");
                    lock (_lock2)
                    {
                        Console.WriteLine("Acquired lock for _lock2 in Task1");
                        Thread.Sleep(1000);
                    }
                }
            }, cancellationToken);

            var task2 = Task.Run(() =>
            {
                Console.WriteLine("Acquiring lock for _lock2 in Task2");
                lock (_lock2)
                {
                    Console.WriteLine("Acquired lock for _lock2 in Task2");
                    Thread.Sleep(1000);
                    Console.WriteLine("Waiting to lock on _lock2 in Task2");
                    lock (_lock1)
                    {
                        Console.WriteLine("Acquired lock for _lock1 in Task2");
                        Thread.Sleep(1000);
                    }
                }
            }, cancellationToken);

            Task.WaitAll(task1, task2);

            await Task.CompletedTask;
        }
    }
}
