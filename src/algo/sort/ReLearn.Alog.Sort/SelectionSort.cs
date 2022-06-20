using System;
using System.Collections.Generic;
using System.Text;

namespace ReLearn.Alog.Sort
{
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
