using System;
using System.Collections.Generic;
using System.Text;

namespace ReLearn.Alog.Sort
{
    /// <summary>
    /// The best-case time complexity is O(N) when the array is already in ascending order.
    /// It is more efficient than the Selection sort.
    /// The insertion sort is used when:
    ///     The array is has a small number of elements
    ///    There are only a few elements left to be sorted
    /// </summary>
    public class InsertionSort
    {
        public static void Sort(int[] dataStream)
        {
            // 9,    4,   5,   1,   3,   2
            for (int step = 1; step < dataStream.Length; step++)
            {
                int key = dataStream[step];
                int i = step-1;
                while (i >= 0&&key < dataStream[i])
                {
                    dataStream[i + 1] = dataStream[i--];
                }
                dataStream[i+1] = key;
            }
            PrintArray(dataStream, "After sort");
        }
        public static void PrintArray(int[] dataStream, string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < dataStream.Length; i++)
                Console.Write($"{dataStream[i]} ");
            Console.WriteLine("");
        }
    }
}
