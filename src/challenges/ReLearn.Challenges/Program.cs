using ReLearn.Challenges.ArrayChallenges;
using System;

namespace ReLearn.Challenges
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ArrayChallenge.PrintPairs(new int[] { 1, 2, 3, 4, 5, 10, 20 }, 10);

            Ameneties.FindPlotWithNearestAmenities_BruteForce(Ameneties.PrepareIntputs());
            Console.Read();
        }
    }
}
