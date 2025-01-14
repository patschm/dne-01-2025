
using System.Net.Sockets;

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
        //TaskKewl();
        //AdvancedTaskAsync();
        //NogMeerAdvanced();
        SemaphoreGarage();

        Console.WriteLine("Einde programma");
        //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        Console.ReadLine();
    }


    private static void SemaphoreGarage()
    {
        var rnd = new Random();
        Semaphore trafficLight = new Semaphore(25, 25);

        ThreadPool.SetMinThreads(100, 100);
        var max = 0;
        for (var i = 0; i < 100; i++)
        {
            ThreadPool.QueueUserWorkItem(Car, i);
        }

        void Car(object nr)
        {
            if (max >= 25)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Car {nr} arriving parking lot...");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Car {nr} arriving parking lot...");
                Console.ResetColor();
            }
            trafficLight.WaitOne();
            //lock (locker)
           // {
                max++;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\tCar {nr} driving into the parking lot ({25 - max} spaces left)");
                Console.ResetColor();
            //}
            var delay = rnd.Next(1000, 10000);
            Thread.Sleep(20000 + delay);
            Console.WriteLine($"Car {nr} driving out...");
            int semnr = trafficLight.Release();
            Console.WriteLine($"Sem Nr {semnr}, {max}");
            Interlocked.Decrement(ref max);
            //lock(locker) max--;
        }
    }


    static object stokje = new object();

    private static void NogMeerAdvanced()
    {
        int counter = 0;

        //ThreadPool.SetMinThreads(10, 10);
        Parallel.For(0, 10, idx =>
        {
            //Monitor.Enter(stokje);
            lock (stokje)
            {
                Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId}");
                int tmp = counter;
                tmp++;
                Task.Delay(30).Wait();
                counter = tmp;
                Console.WriteLine(counter);
            }
            //Monitor.Exit(stokje);
        });
    }

    private static async Task AdvancedTaskAsync()
    {
        int a = 0;
        int b = 0;

        AutoResetEvent zl1 = new AutoResetEvent(false);
        AutoResetEvent zl2 = new AutoResetEvent(false);

        var t1 = Task.Run(() =>
        {
            Task.Delay(1000).Wait();
            a = 10;
            //zl1.Set();
        });
        var t2 = Task.Run(() =>
        {
            Task.Delay(2000).Wait();
            b = 30;
            zl2.Set();
        });


        //WaitHandle.WaitAll([zl1, zl2]);
        //Task.WaitAll(t1, t2);
        await Task.WhenAll(t1, t2);
        Console.WriteLine(a + b);
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
            await Task.Run(() =>
            {
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
        Task.Run(() =>
        {
            Console.WriteLine("Start");
            throw new Exception("Ooops");
        }).ContinueWith(pt =>
        {
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
