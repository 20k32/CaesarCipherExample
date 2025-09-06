using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Core.Encryption
{
    internal interface IEncryptionDelta<T>
    {
        public T Value { get; }
    }
}
