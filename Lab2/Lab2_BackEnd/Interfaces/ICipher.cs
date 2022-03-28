using System.Collections;
using System.Collections.ObjectModel;
using Lab2_BackEnd.Ciphers.StreamCipher;

namespace Lab2_BackEnd.Interfaces
{
    public interface ICipher
    {
        public byte[] Encrypt(Key keyStartState, IEnumerable<byte> bytesFromFile);
        public byte[] Decrypt(Key keyStartState, IEnumerable<byte> bytesFromFile);
    }
}