using Lab1_Caesar.Core;
using Lab1_Caesar.Core.Encryption;
using Lab1_Caesar.Core.Shared;
using Lab1_Caesar.Infrastructure.Collections;
using Lab1_Caesar.Infrastructure.Decryption.Caesar;
using Lab1_Caesar.Infrastructure.Extensions;
using Lab1_Caesar.Infrastructure.Shared.Caesar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Infrastructure.Encryption.Caesar
{
    internal sealed class CaesarEncryptor : CaesarDataTransformerBase,
        IEncryptor<ICollection<CaesarEncriptableEntity>, ICollection<CaesarEncryptionResult>, CaesarDelta>
    {
        public bool TryEncrypt(ICollection<CaesarEncriptableEntity> entity, out ICollection<CaesarEncryptionResult> encryptedEntity)
        {
            bool result = entity.Count > 0;
            encryptedEntity = new List<CaesarEncryptionResult>();

            if (result)
            {
                var lowercase = GetLowercaseSource();
                var uppercase = GetUppercaseSource();

                var lowercaseWithOffset = GetLowercaseSourceWithOffset();
                var uppercaseWithOffset = GetUppercaseSourceWithOffset();

                foreach (var item in entity)
                {
                    try
                    {
                        CaesarEncryptionResult localResult = default;

                        var elementFoundResult = TryFindIndexInCollections(lowercase, uppercase, item.Value);

                        if (elementFoundResult.index >= 0)
                        {
                            if (elementFoundResult.collection == lowercase)
                            {
                                var index = lowercase.IndexOf(new CaesarDelta(item.Value));
                                localResult = new CaesarEncryptionResult(lowercaseWithOffset.ElementAt(index).Value);
                            }
                            else
                            {
                                var index = uppercase.IndexOf(new CaesarDelta(item.Value));
                                localResult = new CaesarEncryptionResult(uppercaseWithOffset.ElementAt(index).Value);
                            }
                        }
                        else if (AllowedSymbols.Contains(item.Value))
                        {
                            localResult = new CaesarEncryptionResult(item.Value);
                        }
                        else
                        {
                            localResult = new CaesarEncryptionResult(FallbackSymbol);
                        }

                        if (localResult is not null)
                        {
                            encryptedEntity.Add(localResult);
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public void SetLowercaseSource(IEnumerable<CaesarDelta> source) => ConfigureSource(LowercaseSource, source);

        public void SetUppercaseSource(IEnumerable<CaesarDelta> source) => ConfigureSource(UppercaseSource, source);

        public void SetOffset(int offset = 0) => ConfigureOffset(offset);
    }
}
