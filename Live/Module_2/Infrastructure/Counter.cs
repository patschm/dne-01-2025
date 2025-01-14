using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

internal class Counter : ICounter
{
    private int _counter = 0;

    public void Increment()
    {
        _counter++;
    }
    public void Show()
    {
        Console.WriteLine($"De waarde is nu {_counter}");
    }
}
