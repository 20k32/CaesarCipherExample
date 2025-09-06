using Lab1_Caesar.Core.Decryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Infrastructure.Decryption.Caesar
{
    public sealed class CaesarDecriptableEntity(char value) : IDecriptable
    {
        public char Value { get; } = value;
    }
}
