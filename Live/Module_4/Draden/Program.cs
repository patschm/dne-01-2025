
namespace Draden;

internal class Program
{
    static void Main(string[] args)
    {
        //OrigineelProbleem();
        //TaskDemo();
        //TaskMetResultaat();
        //TaskChaining();
        //TaskFouten();
        //TaskCancels();
        TaskKewl();

        Console.WriteLine("Einde programma");
        //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        Console.ReadLine();
    }

    private static async void TaskKewl()
    {
        Task<int> t1 = new Task<int>(() => LongAdd(4, 5));
        t1.Start();

        Console.WriteLine("Voor 1");
        //var x = t1.Result;
        var x = await t1;
        Console.WriteLine($"Na 1 {x}");

        var x2 = await LongAddAsync(6, 9);
        Console.WriteLine(x2);

        try
        {
            await Task.Run(() => {
                Console.WriteLine("Start");
                throw new Exception("Ooops");
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private static void TaskCancels()
    {
        CancellationTokenSource nikko = new CancellationTokenSource();

        CancellationToken bommetje = nikko.Token;
        Task.Run(() =>
        {
            for (int i = 0; ; i++)
            {
                Console.WriteLine($"Doe iets {i}");
                Task.Delay(500).Wait();
                if (bommetje.IsCancellationRequested)
                {
                    return;
                }
            }
        });

        nikko.CancelAfter(5000);
    }

    private static void TaskFouten()
    {
       // try
        //{
            Task.Run(() => {
                Console.WriteLine("Start");
                throw new Exception("Ooops");
            }).ContinueWith(pt=> {
                Console.WriteLine(pt.Status);
                if (pt.Exception != null)
                {
                    Console.WriteLine(pt.Exception.InnerException);
                }
            });
       // }
        //catch (Exception ex)
        //{
            //Console.WriteLine(ex.ToString());
        //}
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

        LongAddAsync(10, 20).ContinueWith(pt => Console.WriteLine(pt.Result));
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

    static Task<int> LongAddAsync(int a, int b)
    {
        Task<int> t1 = new Task<int>(() =>
        {
            int result = LongAdd(a, b);
            return result;
        });

        t1.Start();
        return t1;
    }
}
