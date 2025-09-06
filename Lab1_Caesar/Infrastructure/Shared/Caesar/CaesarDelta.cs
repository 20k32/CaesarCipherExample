using Lab1_Caesar.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Infrastructure.Shared.Caesar
{
    internal sealed class CaesarDelta(char value) : IDelta, IComparable<CaesarDelta>, IComparable
    {
        private const int UnsuccessfullComparisonResult = -1;

        public char Value { get; } = value;

        public int CompareTo(object? obj) => CompareTo(obj as CaesarDelta);

        public int CompareTo(CaesarDelta? other)
        {
            int result = UnsuccessfullComparisonResult;

            if (other is not null)
            {
                result = other.Value.CompareTo(Value);
            }

            return result;
        }
    }
}
