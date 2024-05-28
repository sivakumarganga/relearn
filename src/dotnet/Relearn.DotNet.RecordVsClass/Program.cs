using System.Text.Json;
using System.Text.Json.Serialization;

namespace Relearn.DotNet.RecordVsClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PointClass pointClass = new PointClass();

            PointRecord pointRecord = new PointRecord(1, 2);

            /* Immutability:  */
            pointClass.X = 3; // No error
            //pointRecord.X = 3; // Error: Property or indexer 'PointRecord.X' cannot be assigned to -- it is read only

            /* Value equality: */
            PointClass pointClass1 = new PointClass { X = 1, Y = 2 };
            PointClass pointClass2 = new PointClass { X = 1, Y = 2 };
            Console.WriteLine(pointClass1 == pointClass2); // False

            PointRecord pointRecord1 = new PointRecord(1, 2);
            PointRecord pointRecord2 = new PointRecord(1, 2);
            Console.WriteLine(pointRecord1 == pointRecord2); // True

            /* Value-based equality: */
            Console.WriteLine(pointRecord1.Equals(pointRecord2)); // True

            /* Deconstruction: */
            var (x, y) = pointRecord1;
            Console.WriteLine($"x: {x}, y: {y}"); // x: 1, y: 2

            /* Pattern matching: */
            if (pointRecord1 is PointRecord { X: 1, Y: 2 })
            {
                Console.WriteLine("Pattern matched");
            }
            else
            {
                Console.WriteLine("Pattern not matched");
            }

            /* With-expressions: */
            PointRecord pointRecord3 = pointRecord1 with { X = 3 };
            Console.WriteLine(pointRecord3); // PointRecord { X = 3, Y = 2 }

            /* ToString: */
            Console.WriteLine(pointRecord1); // PointRecord { X = 1, Y = 2 }
            Console.WriteLine(pointRecord3); // PointRecord { X = 3, Y = 2 }

            /* Limitations: */

            PointRecordMutable pointRecordMutable = new PointRecordMutable { X = 1, Y = 2 };
            //pointRecordMutable.X = 3; // No error
            pointRecordMutable = pointRecordMutable with { X = 3 };


            string serializedObj = JsonSerializer.Serialize(pointRecordMutable);
            var deserializedObject = JsonSerializer.Deserialize<PointRecord>(serializedObj);
            Console.WriteLine("Hello, World!");
        }
    }
}
