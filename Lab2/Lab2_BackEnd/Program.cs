// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Diagnostics;
using Lab2_BackEnd.Ciphers.StreamCipher;
using Lab2_BackEnd.Enums;
using Lab2_BackEnd.Factory;

var startStateBools = new bool[32];
Array.Fill(startStateBools, true);

Console.WriteLine("Performance Test");
Console.WriteLine("Enter dir for tests");
var path = Console.ReadLine();
if (path is null)
{
    throw new NullReferenceException();
}


Console.WriteLine("Enter max polynomial power");
var maxPower = int.Parse(Console.ReadLine() ?? string.Empty);
Console.WriteLine("Enter powers to xor");
var powersToXor = Console.ReadLine()?.Split(',').Select(x => uint.Parse(x)).ToList();

if (path is null)
{
    throw new NullReferenceException();
}

var dirs = Directory.GetDirectories(path);
var initPath = string.Empty;
var resultPath = string.Empty;
foreach (var dir in dirs)
{
    var dirName = dir.Split('\\')[^1];
    if ( dirName.Equals("init", StringComparison.OrdinalIgnoreCase))
    {
        initPath = dir;
    }
    
    if ( dirName.Equals("result", StringComparison.OrdinalIgnoreCase))
    {
        resultPath = dir;
    }
}

var cipher = CipherFactory.ProduceCipher(Ciphers.LFSRCipher);


var files = Directory.GetFiles(initPath);

foreach (var filePath in files)
{
    try
    {
        
        var timer = new Stopwatch();
        timer.Start();
        var key = new Key(startStateBools, powersToXor, maxPower);
        var encryptRes = cipher.Encrypt(key, File.ReadAllBytes(filePath));
        var pathToWriteResult =
            Path.Join(resultPath, $"{Path.GetFileName(filePath)}Result{Path.GetExtension(filePath)}");
        File.WriteAllBytes(pathToWriteResult, encryptRes);
        timer.Stop();
        Console.WriteLine($"LOG => File:{Path.GetFileName(filePath)} Time: {timer.ElapsedMilliseconds}");
    }
    catch (ArgumentOutOfRangeException e)
    {
        Console.WriteLine(e.Message);
    }
    catch (ArgumentNullException e)
    {
        Console.WriteLine(e.Message);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
    }
   
}