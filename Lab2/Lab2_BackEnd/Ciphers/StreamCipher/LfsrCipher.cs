using System.Collections;
using System.Text;
using Lab2_BackEnd.Interfaces;

namespace Lab2_BackEnd.Ciphers.StreamCipher;

public class LfsrCipher : ICipher
{
    private ICipher.ShowKey? showKeyHandler;
    public byte[] Encrypt(Key keyStartState, IEnumerable<byte> bytesFromFile)
    {
        var fromFile = bytesFromFile as byte[] ?? bytesFromFile.ToArray();
        var fileData = fromFile.ToArray();
        var keyGenerator = new KeyGenerator(keyStartState);
        var key = keyGenerator.GetNBytes(fileData.Length);
        if (showKeyHandler != null)
        {
            showKeyHandler?.Invoke(key);
        }
        var result = new byte[fileData.Length];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = (byte) (fileData[i] ^ key[i]);
        }
        
        return result;

    }

    public byte[] Decrypt(Key keyStartState, IEnumerable<byte> bytesFromFile)
    {
        var fromFile = bytesFromFile as byte[] ?? bytesFromFile.ToArray();
        var fileData = fromFile.ToArray();
        var keyGenerator = new KeyGenerator(keyStartState);
        var key = keyGenerator.GetNBytes(fileData.Length);
        var result = new byte[fileData.Length];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = (byte) (fileData[i] ^ key[i]);
        }
        
        return result;
    }
    
    private static byte[] ToByteArray(BitArray bits)
    {
        var numBytes = bits.Count / 8;
        if (bits.Count % 8 != 0) numBytes++;
            
        var bytes = new byte[numBytes];
        int byteIndex = 0, bitIndex = 0;

        for (var i = 0; i < bits.Count; i++)
        {
            if (bits[i])
                bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

            bitIndex++;
            if (bitIndex != 8) 
                continue;
            bitIndex = 0;
            byteIndex++;
        }
        return bytes;
    }

    public void RegisterShowKey(ICipher.ShowKey show)
    {
        this.showKeyHandler = show;
    }
}