namespace Relearn.DotNet.ClassLib
{
    public class DynamicLauncher
    {
        public static void Run()
        {
            System.Diagnostics.Debugger.Launch();
            int x = 0;
            Console.WriteLine($"Hello World {x}");
        }

    }
}