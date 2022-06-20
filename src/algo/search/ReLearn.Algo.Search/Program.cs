using System;

namespace ReLearn.Algo.Search
{
    public class Program
    {
        static void Main(string[] args)
        {
            foreach(int i in new int[] {0,1,30,51,100,101 })
            {
                BinarySearch.Search(new int[] { 1, 8, 10, 23, 30, 51, 56, 74, 93, 100 }, i);
            }
           
            Console.Read();
        }
    }
}
