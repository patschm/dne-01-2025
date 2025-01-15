namespace SomeLibrary;

public class Person
{
    private int _age;

    public int Age
    {
        get { return _age; }
        set 
        { 
            if (value >= 0 && value < 123) _age = value; 
        }
    }

    public string? Name { get; set; }
    
    public void Introduce()
    {
        Console.WriteLine($"Hallo, ik ben {Name} en mijn leeftijd is {Age}");
    }
}
