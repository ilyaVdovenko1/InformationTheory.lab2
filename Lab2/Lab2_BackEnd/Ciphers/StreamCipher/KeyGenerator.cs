using System.Collections;
using System.Diagnostics;

namespace Lab2_BackEnd.Ciphers.StreamCipher;
public delegate void KeyShowHandler(bool flag);
public class KeyGenerator
{
    private readonly List<uint> bitsToXor;

    private readonly BitArray currentKeyState;

    private int counter;

    private KeyShowHandler? show;
    
    
    public KeyGenerator(Key startState)
    {
        this.bitsToXor = startState.BitsToXor.ToList();
        this.currentKeyState =new BitArray( startState.KeyStartState.ToArray());
    }

    public byte[] GetNBytes(int numberOfBytesToGenerate)
    {
        var result = new byte[numberOfBytesToGenerate];
        for (var index = 0; index < numberOfBytesToGenerate; index++)
        {
            result[index] = GenerateOneByte();
        }

        return result;
    }



    private byte GenerateOneByte()
    {
        var byteAsFlags = new bool[8];
        for (int i = 0; i < byteAsFlags.Length; i++)
        {
            byteAsFlags[i] = GetNextBit();
        }

        return ConvertBoolArrayToByte(byteAsFlags);
    }
    
    private static byte ConvertBoolArrayToByte(bool[] source)
    {
        if (source.Length > 8 )
        {
            throw new ArgumentOutOfRangeException(nameof(source));
        }
        
        byte result = 0;
        // This assumes the array never contains more than 8 elements!
        int index = 8 - source.Length;

        // Loop through the array
        foreach (bool b in source)
        {
            // if the element is 'true' set the bit at that position
            if (b)
                result |= (byte)(1 << (7 - index));

            index++;
        }

        return result;
    }

    public BitArray GetNBits(int numberOfBitsToGenerate)
    {
        this.counter = 0;
        var listOfBits = new List<bool>();
        var timer = new Stopwatch();
        timer.Start();
        for (var i = 0; i < numberOfBitsToGenerate; i++)
        {
            listOfBits.Add(GetNextBit());
        }
        


        return new BitArray(listOfBits.ToArray());
    }

    private bool GetNextBit()
    {
        
        var timer = new Stopwatch();
        var result = this.currentKeyState[^1];
        
        
        var xorResult = this.currentKeyState[(int)this.bitsToXor[0]];
        
        for (var i = 1; i < this.bitsToXor.Count; i++)
        {
            
            xorResult ^= this.currentKeyState[(int)this.bitsToXor[i]];

        }
        this.currentKeyState.LeftShift(1);
        
        
        this.currentKeyState[0] = xorResult;
        
        
        
        Console.WriteLine($"LOG => KeyGen => KeyBit: {result} KeyBitID {counter++}");
        return result;
    }
}