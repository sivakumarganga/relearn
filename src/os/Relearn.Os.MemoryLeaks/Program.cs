// See https://aka.ms/new-console-template for more information
using System.Collections.Concurrent;
using System.Reflection.Metadata;

Console.WriteLine("Hello, World!");
//new Thread(() => { }).Start();
//Task.Factory.StartNew(DoIt);
//ThreadPool.QueueUserWorkItem(() => { });

ConcurrentBag<MyClass> MyBag = new ConcurrentBag<MyClass>();

static (List<User> updated,List<User> inserted) CompareUsers(List<User> usersListInDB, List<User> newUseList)
{

    return (usersListInDB, newUseList);
} 
class User
{
    public int Id { get; set; }
    public int Name { get; set; }
}

//void EmptyBag(object state)
//{
//    while (MyBag.TryTake(out MyClass take))
//    {
//        Console.WriteLine(take.Id);
//        take.Dispose();
//    }
//    Console.WriteLine("Bag is empty");
//}
public class MyClass:IDisposable
{
    public int Id { get; set; }
    public byte[] ByteArry { get; set; }
    public MyClass(int id)
    {
        ByteArry = new byte[10000];
        Id = id;
    }

    public void Dispose()
    {
        this.ByteArry = null;
        this.Id = 0;
        GC.SuppressFinalize(this);
    }
}