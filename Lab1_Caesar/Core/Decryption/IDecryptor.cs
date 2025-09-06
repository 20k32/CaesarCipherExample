using Lab1_Caesar.Core.Encryption;
using Lab1_Caesar.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Core.Decryption
{
    internal interface IDecryptor<Tin, Tout, TDelta>
        where  Tin : IEnumerable<IDecriptable> 
        where Tout : IEnumerable<IDelta>
        where TDelta : IDelta
    {
        bool TryDecrypt(Tin decriptableEntity, out Tout value);
    }
}
