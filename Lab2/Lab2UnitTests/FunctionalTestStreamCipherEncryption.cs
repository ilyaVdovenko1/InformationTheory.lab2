using System;
using System.Collections;
using System.Collections.Generic;
using Lab2_BackEnd.Ciphers.StreamCipher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab2UnitTests;

[TestClass]
public class FunctionalTestStreamCipherEncryption
{
    [TestMethod]
    public void EncryptOneByte()
    {
        var cipher = new LfsrCipher();
        var maxPower = 32;
        var bitsToXor = new List<uint>()
        {
            0,31,27,26
        };
        var keyState = new bool[maxPower] ;
        Array.Fill(keyState,true);
        var key = new Key(keyState, bitsToXor, maxPower);
        var data = new byte[]
        {
            0, Byte.MaxValue, 
        };
        var expected = new byte[]
        {
            byte.MaxValue, 0
        };
        //act
        var actual = cipher.Encrypt(key, data);
        //assert
        CollectionAssert.AreEqual(expected, actual);
    }
}