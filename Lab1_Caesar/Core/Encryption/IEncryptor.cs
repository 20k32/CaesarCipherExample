using Lab1_Caesar.Core.Decryption;
using Lab1_Caesar.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Core.Encryption
{
    internal interface IEncryptor<Tin, Tout, TDelta> : ICypherSource<TDelta>
        where Tin : IEnumerable<IEncriptable> 
        where Tout : IEnumerable<IEncryptionResult>
        where TDelta : IDelta
    {
        bool TryEncrypt(Tin entity, out Tout encryptedEntity);
    }
}
