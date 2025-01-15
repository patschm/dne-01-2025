namespace TheMess;

internal class Program
{
    static UnmanagedResource u1 = new UnmanagedResource();
    static UnmanagedResource u2 = new UnmanagedResource();

    static void Main(string[] args)
    {
        try
        {
            u1.Open();
        }
        finally
        {
            u1.Dispose();
        }
        u1 = null;

        using (u2)
        {
            u2.Open();
        }
        u2 = null;

        using(var u3 = new UnmanagedResource())
        {
            u3.Open();
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();


        Console.ReadLine();
    }
}
