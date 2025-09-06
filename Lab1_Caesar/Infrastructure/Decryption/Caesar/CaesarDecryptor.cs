using Lab1_Caesar.Core;
using Lab1_Caesar.Core.Decryption;
using Lab1_Caesar.Core.Encryption;
using Lab1_Caesar.Infrastructure.Collections;
using Lab1_Caesar.Infrastructure.Encryption.Caesar;
using Lab1_Caesar.Infrastructure.Extensions;
using Lab1_Caesar.Infrastructure.Shared.Caesar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Infrastructure.Decryption.Caesar
{
    internal sealed class CaesarDecryptor : CaesarDataTransformerBase,
        IDecryptor<ICollection<CaesarDecriptableEntity>, ICollection<CaesarDecriptionResult>, CaesarDelta>
    {
        public bool TryDecrypt(ICollection<CaesarDecriptableEntity> decriptableEntity, out ICollection<CaesarDecriptionResult> decryptedEntity)
        {
            var result = decriptableEntity.Count > 0;

            decryptedEntity = new List<CaesarDecriptionResult>();

            if (result)
            {
                var lowercaseSource = GetLowercaseSourceWithOffset(0);
                var uppercaseSource = GetUppercaseSourceWithOffset(0);

                var lowercaseSourceWithOffset = GetLowercaseSourceWithOffset();
                var uppercaseSourceWithOffset = GetUppercaseSourceWithOffset();

                foreach (var entity in decriptableEntity)
                {
                    try
                    {
                        CaesarDecriptionResult localResult = default;

                        var elementFoundResult = TryFindIndexInCollections(lowercaseSourceWithOffset, uppercaseSourceWithOffset, new CaesarDelta(entity.Value));

                        if (elementFoundResult.index >= 0)
                        {
                            if (elementFoundResult.collection == lowercaseSourceWithOffset)
                            {
                                var actualElement = lowercaseSource.ElementAt(elementFoundResult.index);
                                localResult = new CaesarDecriptionResult(actualElement.Value);
                            }
                            else
                            {
                                var actualElement = uppercaseSource.ElementAt(elementFoundResult.index);
                                localResult = new CaesarDecriptionResult(actualElement.Value);
                            }
                        }
                        else if (AllowedSymbols.Contains(entity.Value))
                        {
                            localResult = new CaesarDecriptionResult(entity.Value);
                        }
                        else
                        {
                            localResult = new CaesarDecriptionResult(FallbackSymbol);
                        }

                        if (localResult is not null)
                        {
                            decryptedEntity.Add(localResult);
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
    }
}
