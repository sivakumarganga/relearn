using System;
using System.Collections.Generic;
using System.Text;

namespace ReLearn.Challenges.ArrayChallenges
{
    public class ArrayChallenge
    {
        /// <summary>
        /// 
        ///            # Examples
        ///            # array = [1,2,3,4,5,10,20] step = 10 should return 1 pair: (10,20)
        ///            # array = [1,2,3,4,5,10,20] step = 1 should return 4 pairs: (1,2)(2,3)(3,4)(4,5)
        ///            # write a method accepts an array/list of ints
        ///            # second integer parameter
        ///# return all pairs separated by the value of the second int parameter (step)

        /// </summary>
        public static void PrintPairs(int[] dataInputs, int step)
        {
            Dictionary<int, int> pair = new Dictionary<int, int>();
            for (int i = 0; i < dataInputs.Length; i++)
                pair.Add(dataInputs[i], dataInputs[i]);

            for (int i = 0; i < dataInputs.Length; i++)
            {
                int val = dataInputs[i] - step;
                if (pair.ContainsKey(val))
                {
                    Console.WriteLine($"({val},{dataInputs[i]})");
                }
            }

        }
    }
}
