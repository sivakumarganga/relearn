using System.Net.Mail;
using System.Net;
using System.Globalization;

namespace ReLearn.HackerRank.Exam
{
    internal class Program
    {
        static void Main(string[] args)
        {
             string result = CalculateDateString();
            Console.WriteLine("Hello, World!");
        }
        public static string CalculateDateString()
        {
            /*"primdiasemana" is the column of the data source with contains the dates
string format*/

            //variable to store the string dates
            string dataString = "2/2/2023 12:00:00 AM";

            //this conditional is just to segregate empty labels
            if (dataString != null)
            {
                try
                {
                    //DateTime is a .NET class to work with hours and dates

                    /*Converting the strings to DateTime, if you stop here, your column
                    will be typeof DateTime.*/
                    DateTime data = DateTime.ParseExact(dataString, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    /*Using ToString, we convert again the dates to string passing parameters
                      of format (eg: dd/MM/yyyy) and culture (eg: "pt-BR").*/
                    string dataFormatada = data.ToString("dd/MM/yyyy", new CultureInfo("pt-BR"));
                    return dataFormatada;
                }
                catch (FormatException)
                {
                    //Treat invalid format of date
                    return "Unknown";
                }
            }
            else
            {
                return "Unknown";
            }
        }

    }
}