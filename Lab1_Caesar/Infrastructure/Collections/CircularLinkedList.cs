using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Infrastructure.Collections
{
    public sealed class CircularLinkedList<T>(T value = default!) : IEnumerable<T>
    {
        private sealed class CircularLinkedListNode<T>(T value, CircularLinkedListNode<T> next = null!)
        {
            public readonly T Value = value;
            public CircularLinkedListNode<T> Next = next;
        }

        private CircularLinkedListNode<T> Root = new(value);
        private CircularLinkedListNode<T> Next = default!;

        public int ActualCount { get; private set; }

        public void Add(T value)
        {
            if (Root is null)
            {
                Root = new(value);
            }
            else if (Next is null)
            {
                Next = new(value, Root);
                Root.Next = Next;
            }
            else
            {
                var newNext = new CircularLinkedListNode<T>(value, Root);
                Next.Next = newNext;
                Next = newNext;
            }

            ActualCount++;
        }

        public IEnumerable<T> AsCircularEnumerable()
        {
            var nextElement = Root;

            while(nextElement is not null)
            {
                yield return nextElement.Value;
                nextElement = nextElement.Next;
            }
        }

        public void Clear()
        {
            Root = default!;
            Next = default!;
            ActualCount = 0;
        }

        public IEnumerator<T> GetEnumerator() => AsCircularEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
