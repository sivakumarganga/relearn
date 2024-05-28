using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.RecordVsClass
{
    public class PointClass
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public record PointRecord(int X, int Y)
    {
       
    }

    public record PointRecordMutable
    {
        public PointRecordMutable()
        {
            X = 0;
        }
        public int X { get; init; } // Mutable
        public int Y { get; init; } // Mutable
       
    }

    public class BaseClass
    {
        public virtual string Name => "Base";
    }

    public class DerivedClass : BaseClass
    {
        public override string Name => "Derived";
    }

    //Record can only be inherited from another record
    //public record DerivedRecord : DerivedClass
    //{
    //    public override string Name => base.Name;

    //    public override int GetHashCode()
    //    {
    //        return base.GetHashCode();
    //    }

    //    public override string? ToString()
    //    {
    //        return base.ToString();
    //    }
    //}
}
