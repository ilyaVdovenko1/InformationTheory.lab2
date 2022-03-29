namespace Lab2_BackEnd.Ciphers.StreamCipher;

public class Key
{
    public Key(IEnumerable<bool> startKeyPrototype, IEnumerable<uint> bitsToXorPrototype, int maxPower)
    {
        if (maxPower <= 0 )
        {
            throw new ArgumentOutOfRangeException(nameof(maxPower));
        }
        
        var keyStartState = startKeyPrototype.ToList();
        var bitsToXor = bitsToXorPrototype.ToList();
        bitsToXor.Sort();

        if (keyStartState.Count != maxPower)
        {
            throw new ArgumentException("Wrong key start state length.");
        }
        
        if (bitsToXor[^1] > keyStartState.Count - 1)
        {
            throw new ArgumentException("Last index to xor can not be greater then start state of key length.");
        }
 
        this.KeyStartState = keyStartState.ToArray();

        this.BitsToXor = bitsToXor;
    }
    public IEnumerable<bool> KeyStartState { get; }

    public IEnumerable<uint> BitsToXor { get; }
}