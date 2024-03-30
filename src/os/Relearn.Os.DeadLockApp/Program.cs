// See https://aka.ms/new-console-template for more information
using Relearn.Os.DeadLockApp;
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, World!");
var cancelSource = new CancellationTokenSource();
await Deadlock.RunTheTasksStart(cancelSource.Token);
Thread.Sleep(3000);
cancelSource.Cancel();

Console.WriteLine("After cancellation");
Console.Read();
foreach (var num in GetNaturalNumbers(10))
{
    Console.WriteLine(num);
}
foreach (var num in GetNaturelNumberAsEnumerable(10))
{
    Console.WriteLine(num);
}

int[] GetNaturalNumbers(int number)
{
    int[] arr = new int[number];
    for (int i = 0; i < number; i++)
    {
        arr[i] = i;
    }
    return arr;
}

IEnumerable<int> GetNaturelNumberAsEnumerable(int number)
{
    for (int i = 0; i < number; i++)
    {
        yield return i;
    }
}