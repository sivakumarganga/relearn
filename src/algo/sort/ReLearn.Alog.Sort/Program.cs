using System;

namespace ReLearn.Alog.Sort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SelectionSort.Sort(new int[] { 64, 25, 12, 22, 11 });
            InsertionSort.Sort(new int[] { 9, 4, 5, 1, 3, 2 });
            Console.Read();
        }
    }
}
