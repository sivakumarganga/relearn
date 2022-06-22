using System;
using System.Collections.Generic;
using System.Text;

namespace ReLearn.Alog.Sort
{
    public class QuickSort
    {
        public static void Sort(int[] dataStream)
        {
            PrintArray(dataStream, "Quick Sort: Before");
            QuickSortAlgo(dataStream, 0, dataStream.Length - 1);
            PrintArray(dataStream, "Quick Sort: After");
        }
        public static void QuickSortAlgo(int[] dataStream, int low, int high)
        {
            if (low >= high)
                return;
            int pivot = QuickPartition(dataStream, low, high);
            QuickSortAlgo(dataStream, low, pivot - 1);
            QuickSortAlgo(dataStream, pivot + 1, high);

        }
        public static int QuickPartition(int[] dataStream,int low,int high)
        {
            int pivot = dataStream[high];
            int pointerToLowerItem = low-1;
            for (int i = low; i < high; i++)
            {
                //Check if array item is less then the pivot 
                if (dataStream[i] < pivot)
                {
                    //Then Swap with last pointer
                    pointerToLowerItem++;
                    int temp = dataStream[pointerToLowerItem];
                    dataStream[pointerToLowerItem] = dataStream[i];
                    dataStream[i] = temp;
                }
            }
            //At last swap pivot (high indexed) value to center
            dataStream[high] = dataStream[++pointerToLowerItem];
            dataStream[pointerToLowerItem] = pivot;
            return pointerToLowerItem;
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
