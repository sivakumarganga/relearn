using ReLearn.Challenges.ArrayChallenges;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ReLearn.Challenges
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string programKey = "160";
            string organisationKey = "200647";
            string memberKey = "222AAAB";
            string cvt = $"{programKey}-{ organisationKey}-{memberKey}".ToUpper();
            string resultCvt = string.Empty;
            //string cvtResult = "160-200647-222AAAB";
           // Console.WriteLine(results == "09ec81e400ecd40f055ab88904f1cc90bcc436d71464c3588d59003e7d0ec686");
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(cvt));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                resultCvt= builder.ToString();
            }
            Console.WriteLine(resultCvt);
            // ArrayChallenge.PrintPairs(new int[] { 1, 2, 3, 4, 5, 10, 20 }, 10);

            Ameneties.FindPlotWithNearestAmenities_BruteForce(Ameneties.PrepareIntputs());
            Console.Read();
        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
