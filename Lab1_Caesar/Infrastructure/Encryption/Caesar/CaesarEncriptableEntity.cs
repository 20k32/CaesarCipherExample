using Lab1_Caesar.Core.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Infrastructure.Encryption.Caesar
{
    internal sealed class CaesarEncriptableEntity(char value) : IEncriptable
    {
        public char Value { get; } = value;
    }
}
