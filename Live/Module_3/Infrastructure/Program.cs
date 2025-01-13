
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

internal class Program
{
    static void Main(string[] args)
    {
        //Configuraties();
        Environments();
    }

    private static void Environments()
    {
        Console.WriteLine(Environment.MachineName);
        var env1 = Environment.GetEnvironmentVariable("Bla");
        Console.WriteLine(env1);
    }

    private static void Configuraties()
    {
        ConfigurationBuilder cbld = new ConfigurationBuilder();
        cbld.AddJsonFile("appsettings.json", optional: true, reloadOnChange:false);
        //cbld.AddXmlFile("infra.xml", optional:true, reloadOnChange:false);
        IConfiguration config = cbld.Build();

        var section = config.GetSection("First");
        Console.WriteLine(section.Value);

        var constr =  config.GetConnectionString("CONSTR1");
        Console.WriteLine(constr);

        section= config.GetSection("MyApp:Name");
        Console.WriteLine(section.Value);

        section = config.GetSection("MyApp:Age");
        Console.WriteLine(section.Value);

        var ma = config.GetSection("MyApp").Get<MyApp>();
        Console.WriteLine(ma.Name);

        var ma2 = new MyApp();
        config.GetSection("MyApp").Bind(ma2);
        Console.WriteLine(ma2.Age);

    }
}
