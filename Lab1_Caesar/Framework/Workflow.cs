using Lab1_Caesar.Infrastructure.Decryption.Caesar;
using Lab1_Caesar.Infrastructure.Encryption.Caesar;
using Lab1_Caesar.Infrastructure.Shared.Caesar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Caesar.Framework
{
    public static class Workflow
    {
        private const string ExitResult = "exit";

        private static readonly CaesarEncryptor encryptor;
        private static readonly CaesarDecryptor decryptor;

        static Workflow()
        {
            encryptor = new CaesarEncryptor();
            decryptor = new CaesarDecryptor();

            ConfigureCeaserCypher();
        }

        private static void ConfigureCeaserCypher()
        {
            var elementCount = 'z' - 'a' + 1;

            var lowercaseSourcee = Enumerable.Range('a', elementCount).Select(character => new CaesarDelta((char)character)).ToArray();
            var uppercaseSource = Enumerable.Range('A', elementCount).Select(character => new CaesarDelta((char)character)).ToArray();

            encryptor.SetLowercaseSource(lowercaseSourcee);
            encryptor.SetUppercaseSource(uppercaseSource);
           
            decryptor.Configure(encryptor, elementCount);
        }

        private static bool AskForOffset(out int offset, out string result)
        {
            result = string.Empty;

            offset = default;

            do
            {
                Console.Clear();
                Console.Write("Enter offset: ");
                result = Console.ReadLine() ?? string.Empty;
            }
            while (!int.TryParse(result, out offset));

            return result != ExitResult;
        }

        public static void HandleUserInput()
        {
            string userResult = string.Empty;

            do
            {
                if (AskForOffset(out var offset, out userResult))
                {
                    encryptor.SetOffset(offset);
                    decryptor.Configure(encryptor);

                    Console.WriteLine("Enter string to encrypt: ");

                    userResult = Console.ReadLine() ?? string.Empty;

                    var encrypted = userResult.Select(character => new CaesarEncriptableEntity(character)).ToArray();

                    if (encryptor.TryEncrypt(encrypted, out var encryptedResult))
                    {
                        var encryptedString = string.Join(string.Empty, encryptedResult.Select(character => character.Value));

                        Console.WriteLine($"Encrypted: {encryptedString}");

                        var decripted = encryptedString.Select(character => new CaesarDecriptableEntity(character)).ToArray();

                        if (decryptor.TryDecrypt(decripted, out var decryptedResult))
                        {
                            var decryptedString = string.Join(string.Empty, decryptedResult.Select(character => character.Value));

                            Console.WriteLine($"Decrypted: {decryptedString}");
                        }
                        else
                        {
                            Console.WriteLine($"Unable to decrypt string: {encryptedString}");
                        }
                    }
                    else if (userResult != ExitResult)
                    {
                        Console.WriteLine($"Unable to encrypt such string: {userResult}.");
                    }
                    
                    userResult = Console.ReadLine()!;
                }
            }
            while (userResult != ExitResult);
        }
    }
}
