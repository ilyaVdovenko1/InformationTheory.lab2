using System;
using System.Collections.Generic;
using System.IO;
using Lab2_BackEnd.Ciphers.StreamCipher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab2UnitTests;
[TestClass]
public class FullCycleAbstractFilesTest
{
    [TestMethod]
    public void TestAbstractTextFile()
    {
        var fileData = File.ReadAllBytes(@"C:\Users\Lenovo\Documents\бгуир\предметы\2курс\4сем\ТИ\ЛР\2lab-streamCiphers\tests\init\test.txt");
        var cipher = new LfsrCipher();
        const int maxPower = 32;
        var bitsToXor = new List<uint>()
        {
            0,31,27,26
        };
        var keyState = new bool[maxPower] ;
        Array.Fill(keyState,true);
        var key = new Key(keyState, bitsToXor, maxPower);
        var result = cipher.Decrypt(key, cipher.Encrypt(key, fileData));
        
        CollectionAssert.AreEqual(fileData, result);
    }
    
}