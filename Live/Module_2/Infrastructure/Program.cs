
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        //Loggings();
        //DepInj();
        AllInOne();
    }

    private static void AllInOne()
    {
        var builder1 = new HostApplicationBuilder();
        builder1.Services.AddSingleton<ICounter, Counter>();
        var host = builder1.Build();

        //host.Services.GetRequiredService();

        var builder =  Host.CreateDefaultBuilder();
        builder.ConfigureServices(sc =>
        {
            sc.AddTransient<ICounter, Counter>();
            sc.AddTransient<CounterHost>();
        });
        var app =builder.Build();
        
        app.Services.GetRequiredService<CounterHost>().Demo();
    }

    private static void DepInj()
    {
        ServiceCollection services = new ServiceCollection();
        //services.AddKeyedScoped<ICounter, Counter>("no1");
        //services.AddKeyedScoped<ICounter, Counter2>("no2");
        services.AddScoped<ICounter, Counter>();
        services.AddScoped<ICounter, Counter2>();

        services.AddTransient<CounterHost>();

        var factory = new DefaultServiceProviderFactory();
        var builder = factory.CreateBuilder(services);
        var provider = builder.BuildServiceProvider();

        var scp1 = provider.CreateScope();
        //ICounter cnt = scp1.ServiceProvider.GetRequiredKeyedService<ICounter>("no1");
        ICounter cnt = scp1.ServiceProvider.GetRequiredService<ICounter>();
        cnt.Increment();
        cnt.Increment();
        cnt.Show();

        var scp2 = provider.CreateScope();
        //var cnt2 = scp2.ServiceProvider.GetRequiredKeyedService<ICounter>("no2");
        var cnt2 = scp2.ServiceProvider.GetRequiredService<ICounter>();
        cnt2.Increment();
        cnt2.Increment();
        cnt2.Show();

        //var cnt3= scp1.ServiceProvider.GetRequiredService<ICounter>();
        //cnt3.Increment();
        //cnt3.Increment();
        //cnt3.Show();

        Console.WriteLine("=============================");
        CounterHost ch = provider.GetRequiredService<CounterHost>();
        ch.Demo();




        //ICounter c = new Counter();
        //c.Increment();
        //c.Increment();
        //c.Show();

        //CounterHost host = new CounterHost(c);
        //host.Demo();
    }

    private static void Loggings()
    {
        ConfigurationBuilder cbld = new ConfigurationBuilder();
        //cbld.SetBasePath(Environment.CurrentDirectory);
        cbld.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
        var config = cbld.Build();

        var fact = LoggerFactory.Create(conf =>
        {
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

        var constr = config.GetConnectionString("CONSTR1");
        Console.WriteLine(constr);

        section = config.GetSection("MyApp:Name");
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
