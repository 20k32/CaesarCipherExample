using Lab1_Caesar.Framework;
using Lab1_Caesar.Infrastructure.Decryption.Caesar;
using Lab1_Caesar.Infrastructure.Encryption.Caesar;
using Lab1_Caesar.Infrastructure.Shared.Caesar;
using System.Reflection.PortableExecutable;

namespace Lab1_Caesar
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            Workflow.HandleUserInput();
        }
    }
}
