using System.Globalization;

namespace Relearn.DotNet.Cultures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// Display the name of the current culture.
            //Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.Name);

            //// Change the current culture to th-TH.
            //CultureInfo.CurrentCulture = new CultureInfo("th-TH", false);
            //Console.WriteLine("CurrentCulture is now {0}.", CultureInfo.CurrentCulture.Name);

            //// Display the name of the current UI culture.
            //Console.WriteLine("CurrentUICulture is {0}.", CultureInfo.CurrentUICulture.Name);

            //// Change the current UI culture to ja-JP.
            //CultureInfo.CurrentUICulture = new CultureInfo("ja-JP", false);
            //Console.WriteLine("CurrentUICulture is now {0}.", CultureInfo.CurrentUICulture.Name);

            // Change the current UI culture to ja-JP.
            //CultureInfo.CurrentUICulture = new CultureInfo("nl-Aruba", false);
            //Console.WriteLine("CurrentUICulture is now {0}.", CultureInfo.CurrentUICulture.Name);
            while (true)
            {
                Console.WriteLine("Please enter desired language name or all or exit");
                string language = Console.ReadLine()!.ToLower();
                if (language == "exit")
                {
                    break;
                }
                var result = CultureInfo.GetCultures(CultureTypes.AllCultures).Where(c => c.Name.ToLower().Contains(language)).ToList();
                result.ForEach(c =>
                {

                    Console.WriteLine("---  Name: {0}  | English Name:{1}  -----", c.Name, c.EnglishName);
                    CultureInfo.CurrentCulture = new CultureInfo(language, true);
                    CultureInfo.CurrentUICulture = new CultureInfo(language, true);
                    //print all the formats from the current culture
                    Console.WriteLine("Date Format    ");
                    Console.WriteLine("\tShort Date ({0}): {1}", c.DateTimeFormat.ShortDatePattern, DateTime.Now.ToString("d"));
                    Console.WriteLine("\tLong Date({0}): {1}", c.DateTimeFormat.LongDatePattern, DateTime.Now.ToString("D"));

                    Console.WriteLine("Number ({0}): {1}", c.NumberFormat.NumberGroupSeparator, 123456789.42.ToString("N"));
                    Console.WriteLine("Currency ({0}): {1}", c.NumberFormat.CurrencyDecimalSeparator, 123456789.42.ToString("C"));
                    Console.WriteLine("Currency ({0}): {1}", c.NumberFormat.CurrencySymbol, 123456789.42.ToString("C", c));
                    Console.WriteLine("Percentage ({0}): {1}", c.NumberFormat.PercentDecimalSeparator, 0.42.ToString("P"));
                    Console.WriteLine("Per Mile ({0})", c.NumberFormat.PerMilleSymbol);
                    Console.WriteLine("Custom ({0}): {1}", "#,##0.00", 123456789.42.ToString("#,##0.00"));
                    Console.WriteLine("---  End  -----");
                    Console.WriteLine();

                });
            }
        }
    }
}
