//using Relearn.DotNet.ClassLib;

using System.Reflection;

namespace Relearn.DotNet.RuntimeDebugger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DynamicLauncher.Run();
            var path = Path.Combine(AppContext.BaseDirectory, "Relearn.DotNet.ClassLib.dll");
            var assembly=Assembly.LoadFrom(path);
            var dynamicLanType=assembly.GetExportedTypes().Where(_ => _.Name == "DynamicLauncher").FirstOrDefault();
            if(dynamicLanType != null)
            {
                var runMethod = dynamicLanType.GetMethod("Run");
                runMethod.Invoke(null,null);
            }
            Console.WriteLine("Hello, World!");
        }
    }
}