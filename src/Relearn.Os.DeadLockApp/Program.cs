// See https://aka.ms/new-console-template for more information
using Relearn.Os.DeadLockApp;

Console.WriteLine("Hello, World!");
var cancelSource = new CancellationTokenSource();
await Deadlock.LockThread(cancelSource.Token);
//cancelSource.Cancel();
