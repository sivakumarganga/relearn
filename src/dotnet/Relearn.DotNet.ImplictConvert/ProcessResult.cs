using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.ImplictConvert
{
    public class ProcessResult<T>
    {
        public T? Value { get; set; }
        public ProcessResult(T value)
        {
            this.Value = value;
        }

        public static implicit operator ProcessResult<T>(T value)
        {
            return new ProcessResult<T>(value);
        }
         
    }
    public class ProcessResultTester
    {
        public ProcessResult<int> Processs()
        {
            return 0;
        }
    }
}
