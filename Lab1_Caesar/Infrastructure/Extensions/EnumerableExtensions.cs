using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        private const int SuccessComparisonResult = 0;
        private const int NotFoundElementInCollectionResult = -1;

        public static int IndexOf<T>(this IEnumerable<T> source, T elemet) where T : IComparable<T>, IComparable
        {
            var result = 0;
            var elementFound = false;

            foreach (var item in source)
            {
                if (item.CompareTo(elemet) == SuccessComparisonResult)
                {
                    elementFound = true;
                    break;
                }

                result++;
            }

            return elementFound ? result : NotFoundElementInCollectionResult;
        }
    }
}
