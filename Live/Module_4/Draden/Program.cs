



using System.Threading.Channels;

namespace Draden;

internal class Program
{
    static void Main(string[] args)
    {
        //OrigineelProbleem();
        //TaskDemo();
        //TaskMetResultaat();
        TaskChaining();

        Console.WriteLine("Einde programma");
        //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        Console.ReadLine();
    }

    private static void TaskChaining()
    {
        Task<int> t1 = new Task<int>(() =>
        {
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int result = LongAdd(2, 3);
            return result;
        });
        
        t1.Start();

        t1.ContinueWith(vorigeTaak =>
        {
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            var res = vorigeTaak.Result;
            Console.WriteLine($"Het resultaat is: {res}");
        });
        t1.ContinueWith(pt => Console.WriteLine("Andere"));

        LongAddSpecial(10, 20).ContinueWith(pt => Console.WriteLine(pt.Result));
    }

    private static void TaskMetResultaat()
    {
        Task<int> t1 = new Task<int>(() =>
        {
            int result = LongAdd(2, 3);
            return result;
        });
        t1.Start();
        var res = t1.Result;
        Console.WriteLine($"Het resultaat is: {res}");
    }

    private static void TaskDemo()
    {
        Task t1 = new Task(() =>
        {
            int result = LongAdd(2, 3);
            Console.WriteLine($"Het resultaat is: {result}");
        });

        t1.Start();


        var t2 = Task.Run(() =>
        {
            int result = LongAdd(12, 13);
            Console.WriteLine($"Het resultaat is: {result}");
        });
    }

    private static void OrigineelProbleem()
    {
        int result = LongAdd(2, 3);
        Console.WriteLine($"Het resultaat is: {result}");
    }

    static int LongAdd(int a, int b)
    {
        Task.Delay(5000).Wait();
        return a + b;
    }

    static Task<int> LongAddSpecial(int a, int b)
    {
        Task<int> t1 = new Task<int>(() =>
        {
            int result = LongAdd(a, b);
            //Console.WriteLine($"Het resultaat is: {result}");
            return result;
        });

        t1.Start();
        return t1;
    }
}
