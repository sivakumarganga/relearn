using System;
using System.Collections.Generic;
using System.Text;

namespace ReLearn.Alog.Sort
{

    /// <summary>
    /// There is no best case the time complexity is O(N2) in all cases.
    /// 
    /// The selection sort is used when   
    /// A small list is to be sorted    
    /// The cost of swapping does not matter 
    /// Checking of all the elements is compulsory Cost of writing to memory matters like in flash memory(number of Swaps is O(n) as compared to O(n2) of bubble sort)
    /// </summary>
    public class SelectionSort
    { 
        public static void Sort(int[] dataStream)
        {
            PrintArray(dataStream,"Before Sorting");
            for (int i = 0; i < dataStream.Length; i++)
            {
                int shortIndex = i;
                for (int j = i + 1; j < dataStream.Length; j++)
                {
                    if( dataStream[j]< dataStream[shortIndex])
                    {
                        shortIndex = j;
                    }
                }
                int temp = dataStream[i];
                dataStream[i] = dataStream[shortIndex];
                dataStream[shortIndex] = temp;
            }
            PrintArray(dataStream,"After Sorting");
           
        }
        public static void PrintArray(int[] dataStream,string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < dataStream.Length; i++)
                Console.Write($"{dataStream[i]} ");
            Console.WriteLine("");
        }
    }
}
