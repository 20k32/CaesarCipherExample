using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Core.Shared
{
    internal interface ICipherSource<T>
    {
        void SetLowercaseSource(IEnumerable<T> source);
        void SetUppercaseSource(IEnumerable<T> source);
        void SetOffset(int offset = 0);
    }
}
