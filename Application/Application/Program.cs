// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Version 1");
Console.WriteLine("Wait...");

Stopwatch sw = Stopwatch.StartNew();
var delay = Task.Delay(1000).ContinueWith(_ =>
{
    sw.Stop();
    return sw.ElapsedMilliseconds;
});

Console.WriteLine("Elapsed milliseconds: {0}", delay.Result);