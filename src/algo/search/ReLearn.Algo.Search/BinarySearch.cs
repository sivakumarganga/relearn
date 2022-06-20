using System;
using System.Collections.Generic;
using System.Text;

namespace ReLearn.Algo.Search
{
    public static class BinarySearch
    {
        public static void Search(int[] dataStream,int number)
        {
            int tail=0;
            int head = dataStream.Length - 1;
            int mid = -1;
            int iterations=0;
            while (tail!=head&&tail < dataStream.Length && head < dataStream.Length)
            {
                 mid = (tail + head) / 2;
                if(dataStream[mid] == number)
                {
                    break;
                }
                else if(dataStream[mid] <number)
                {
                    tail = mid + 1;
                }else
                {
                    head = mid;
                }
                mid = -1;
                iterations++;
            }
            Console.WriteLine($"Found Value:{number} at {mid} with Iterations:{iterations}");
        }
    }
}
