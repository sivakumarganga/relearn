namespace Relearn.DotNet.NameOf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using static Relearn.DotNet.NameOf.Program;

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine($"nameof(SuperMan): Expected:SuperMan  Actual: {nameof(SuperMan)}");
            Console.WriteLine($"nameof(SuperMan.SuperManName): Expected:SuperManName  Actual: {nameof(SuperMan.SuperManName)}");
            Console.WriteLine($"nameof(SuperMan.SuperManAlive): Expected:SuperManAlive  Actual: {nameof(SuperMan.SuperManAlive)}");
            Console.WriteLine($"nameof(SuperMan.IsItSuperMan): Expected:IsItSuperMan  Actual: {nameof(SuperMan.IsItSuperMan)}");


            Console.WriteLine($"nameof(GenericSuperMan<object>): Expected:GenericSuperMan  Actual: {nameof(GenericSuperMan<object>)}");
            Console.WriteLine($"nameof(GenericSuperMan<object>.SuperManName): Expected:SuperManName  Actual: {nameof(GenericSuperMan<object>.SuperManName)}");
            Console.WriteLine($"nameof(GenericSuperMan<object>.SuperManAlive): Expected:SuperManAlive  Actual: {nameof(GenericSuperMan<object>.SuperManAlive)}");
            Console.Read();
        }
      
        public class SuperMan
        {
            public string SuperManName { get; set; }
            public bool SuperManAlive()
            {
                return true;
            }
            public bool IsItSuperMan<T>(T man) where T : class
            {
                return man is SuperMan;
            }
        }
        public class GenericSuperMan<T>
        {
            public string SuperManName { get; set; }
            public bool SuperManAlive()
            {
                return true;
            }
        }
    }
}