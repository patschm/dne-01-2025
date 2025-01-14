using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

internal class CounterHost
{
    private readonly ICounter _counter;

    //public CounterHost([FromKeyedServices("no2")]ICounter counter)
    public CounterHost(ICounter counter)

    {
        _counter = counter;
    }

    public void Demo()
    {
        _counter.Increment();
        _counter.Increment();
        _counter.Show();
    }
}
