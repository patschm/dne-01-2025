using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;

namespace GarbageSpewer;

internal class Program
{
    static void Main(string[] args)
    {
        string s = "";
        StringBuilder bs = new StringBuilder();
        var sw = new Stopwatch();

        sw.Start();
        for(int i = 0; i < 100000; i++)
        {
            //s += i;
            bs.Append(i);
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
        Console.ReadLine();
    }
}
