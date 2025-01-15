//using SomeLibrary;

using System.Reflection;
using System.Text;

namespace TheClient;

internal class Program
{
    static void Main(string[] args)
    {
        //Person p1 = new Person { Name = "Jan", Age = 26 };
        //p1.Introduce();

        Assembly asm = LoadAssembly();
        ShowInfo(asm);
        //UseAssembly(asm);

        //var r1 = new Room();
        ///DoeErIetsMee(r1);

        Console.ReadLine(); 
    }

    private static void DoeErIetsMee(Room r1)
    {
        var attr = r1.GetType().GetCustomAttribute<MyAttribute>();
        if (attr.MinAge >= 18 && attr.MinAge < 67)
        {
            r1.DoeIets();
        }
        else
        {
            Console.WriteLine("Niet de gewenste leeftijd");
        }
    }

    private static void UseAssembly(Assembly asm)
    {
        Type? type = asm.GetType("SomeLibrary.Person");
        object? p1 = Activator.CreateInstance(type);

        PropertyInfo pn = type.GetProperty("Name");
        pn.SetValue(p1, "Patrick");

        PropertyInfo pa = type.GetProperty("Age");
        pa.SetValue(p1, 32);

        FieldInfo fn = type.GetField("_age", BindingFlags.Instance | BindingFlags.NonPublic);
        fn.SetValue(p1, -42);

        MethodInfo mi = type.GetMethod("Introduce");
        mi.Invoke(p1, []);

        dynamic p2 = Activator.CreateInstance(type);
        p2.Name = "Kees";
        p2.Age = 42;
        p2.Introduce();

    }

    private static void ShowInfo(Assembly asm)
    {
        foreach(Type tp in asm.GetTypes())
        {
            Console.WriteLine(tp.FullName);
            foreach(MemberInfo mi in tp.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine(mi.Name);
                if (mi is MethodInfo)
                {
                    var ms = mi as MethodInfo;
                    //var bytes = ms.GetMethodBody()?.GetILAsByteArray();
                    //if (bytes != null) 
                    //    Console.WriteLine(Encoding.Default.GetString(bytes));  
                }
                Console.WriteLine("=============================");
            }
        }
    }

    private static Assembly LoadAssembly()
    {
        var asm = Assembly.LoadFile(@"E:\test\somelibrary.dll");
        Console.WriteLine(asm.FullName);
        return asm;
    }
}
