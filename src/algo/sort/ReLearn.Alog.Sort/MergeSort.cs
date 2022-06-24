using System;
using System.Collections.Generic;
using System.Text;

namespace ReLearn.Alog.Sort
{
    public class MergeSort
    {
        public static void Sort(int[] dataStream)
        {
            PrintArray(dataStream, "MergeSort Sort: Before");
            DevideNMerge(dataStream, 0, dataStream.Length - 1);
            PrintArray(dataStream, "MergeSort Sort: After");
        }
        public static void DevideNMerge(int[] dataStream,int left,int right)
        {
            if (left >= right)
                return;

            int m = left + (right - left) / 2;
            DevideNMerge(dataStream, left, m);
            DevideNMerge(dataStream, m+1, right);
            Merge(dataStream, left, right, m);
        }
        public static void Merge(int[] dataStream, int left, int right, int middle)
        {
            int leftArrarySize = middle - left +1;
            int rightArrarySize = right - middle;

            int[] leftArray = new int[leftArrarySize];            
            for (int index = 0; index < leftArrarySize; index++)
                leftArray[index] = dataStream[left + index];

            int[] rightArray = new int[rightArrarySize];
            for (int index = 0; index < rightArrarySize; index++)
                rightArray[index] = dataStream[middle+1 + index];

            int leftArrayIndex = 0, rightArrayIndex = 0;

            int i = left;
            while (leftArrayIndex < leftArrarySize && rightArrayIndex < rightArrarySize)
            {
                if (leftArray[leftArrayIndex] < rightArray[rightArrayIndex])
                {
                    dataStream[i] = leftArray[leftArrayIndex++];
                }
                else
                {
                    dataStream[i] = rightArray[rightArrayIndex++];
                }
                i++;
            }
            while(leftArrayIndex<leftArrarySize)
                dataStream[i++] = leftArray[leftArrayIndex++];
            while (rightArrayIndex < rightArrarySize)
                dataStream[i++] = rightArray[rightArrayIndex++];
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
