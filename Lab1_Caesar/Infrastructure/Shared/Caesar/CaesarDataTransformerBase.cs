using Lab1_Caesar.Core;
using Lab1_Caesar.Core.Encryption;
using Lab1_Caesar.Infrastructure.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab1_Caesar.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace Lab1_Caesar.Infrastructure.Shared.Caesar
{
    internal abstract class CaesarDataTransformerBase
    {
        private const int NotFoundElementInCollectionIndex = -1;

        protected int ActualCount { get; private set; }
        protected int Offset { get; private set; }

        protected readonly IEnumerable<char> AllowedSymbols = [',', ' ', '.', '!', '?'];
        protected readonly char FallbackSymbol = '_';

        protected readonly CircularLinkedList<CaesarDelta> LowercaseSource = new();
        protected readonly CircularLinkedList<CaesarDelta> UppercaseSource = new();

        public void Configure(CaesarDataTransformerBase transformerBase, int elementCount)
        {
            Configure(transformerBase.GetLowercaseSource(), 
                transformerBase.GetUppercaseSource(),
                transformerBase.Offset);
        }
        public void Configure(CaesarDataTransformerBase transformerBase) => ConfigureOffset(transformerBase.Offset);

        protected IEnumerable<CaesarDelta> GetLowercaseSource() => LowercaseSource.Take(LowercaseSource.ActualCount);
        protected IEnumerable<CaesarDelta> GetUppercaseSource() => UppercaseSource.Take(UppercaseSource.ActualCount);

        protected IEnumerable<CaesarDelta> GetLowercaseSourceWithOffset() => GetLowercaseSourceWithOffset(Offset);
        protected IEnumerable<CaesarDelta> GetUppercaseSourceWithOffset() => GetUppercaseSourceWithOffset(Offset);

        protected IEnumerable<CaesarDelta> GetLowercaseSourceWithOffset(int offset) => LowercaseSource.Skip(offset).Take(LowercaseSource.ActualCount);
        protected IEnumerable<CaesarDelta> GetUppercaseSourceWithOffset(int offset) => UppercaseSource.Skip(offset).Take(UppercaseSource.ActualCount);

        protected void Configure(IEnumerable<CaesarDelta> lowercaseSource, IEnumerable<CaesarDelta> uppercaseSource, int offset)
        {
            ConfigureSource(LowercaseSource, lowercaseSource);
            ConfigureSource(UppercaseSource, uppercaseSource);
            ConfigureOffset(offset);
        }

        protected void ConfigureSource(CircularLinkedList<CaesarDelta> source, IEnumerable<CaesarDelta> sourceValues)
        {
            source.Clear();

            foreach (var item in sourceValues)
            {
                source.Add(item);
            }
        }

        protected void ConfigureOffset(int offset)
        {
            Offset = offset;
        }

        protected (int index, IEnumerable<CaesarDelta> collection) TryFindIndexInCollections(IEnumerable<CaesarDelta> lowercaseCollection, IEnumerable<CaesarDelta> uppercaseCollection, CaesarDelta value)
        {
            IEnumerable<CaesarDelta> collection = default!;
            var index = NotFoundElementInCollectionIndex;

            index = lowercaseCollection.IndexOf(value);

            if (index == NotFoundElementInCollectionIndex)
            {
                index = uppercaseCollection.IndexOf(value);

                if (index != NotFoundElementInCollectionIndex)
                {
                    collection = uppercaseCollection;
                }
            }
            else
            {
                collection = lowercaseCollection;
            }

            return (index, collection);
        }

        protected (int index, IEnumerable<CaesarDelta> collection) TryFindIndexInCollections(IEnumerable<CaesarDelta> lowercaseCollection, IEnumerable<CaesarDelta> uppercaseCollection, char value)
        {
            IEnumerable<CaesarDelta> collection = default!;
            var index = NotFoundElementInCollectionIndex;

            index = lowercaseCollection.IndexOf(new CaesarDelta(value));

            if (index == NotFoundElementInCollectionIndex)
            {
                index = uppercaseCollection.IndexOf(new CaesarDelta(value));

                if (index != NotFoundElementInCollectionIndex)
                {
                    collection = uppercaseCollection;
                }
            }
            else
            {
                collection = lowercaseCollection;
            }

            return (index, collection);
        }
    }
}
