
using Lab2_BackEnd.Ciphers.StreamCipher;
using Lab2_BackEnd.Interfaces;

namespace Lab2_BackEnd.Factory
{
    public static class CipherFactory
    {
        public static ICipher ProduceCipher(Enums.Ciphers cipher)
        {
            return cipher switch
            {
                Enums.Ciphers.LFSRCipher => new LfsrCipher(),
                _ => throw new ArgumentOutOfRangeException(nameof(cipher), cipher, "Can not recognize cipher.")
            };
        }
    }
}