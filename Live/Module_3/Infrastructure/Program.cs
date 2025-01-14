
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Infrastructure;
class Test
{

}
internal class Program
{
    static void Main(string[] args)
    {
        //Configuraties();
        //Environments();
        Loggings();
    }

    private static void Loggings()
    {
        ConfigurationBuilder cbld = new ConfigurationBuilder();
        //cbld.SetBasePath(Environment.CurrentDirectory);
        cbld.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
        var config = cbld.Build();

        var fact = LoggerFactory.Create(conf => {
            //conf.AddFilter((cat, lgolvl) => {
            //    Console.WriteLine(cat);
            //    return lgolvl >= LogLevel.Trace && cat == typeof(Program).FullName;

            //});
            conf.AddConfiguration(config.GetSection("Blaat"));
            conf.ClearProviders();
            conf.AddConsole();
        });

        ILogger<Program> logger = fact.CreateLogger<Program>();
        logger.LogTrace("Trees");
        logger.LogDebug("Debug");
        logger.LogInformation("Info");
        logger.LogWarning("Warning");
        logger.LogError("Error");
        logger.LogCritical("Critical");
        //logger.Log()
    }

    private static void Environments()
    {
        Console.WriteLine(Environment.MachineName);
        var env1 = Environment.GetEnvironmentVariable("Bla");
        Console.WriteLine(env1);

        Console.WriteLine(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT"));
    }

    private static void Configuraties()
    {
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        ConfigurationBuilder cbld = new ConfigurationBuilder();
        
        cbld.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
        cbld.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false);
        cbld.AddUserSecrets<Program>();
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
