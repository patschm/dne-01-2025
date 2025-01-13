using System.Collections.Generic;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Serializers;

[XmlRoot("people")]
public class People : List<Person> { }

internal class Program
{
    static void Main(string[] args)
    {
        //SerializeDemo();
        DeserializeDemo();
    }

    private static void DeserializeDemo()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(People));
        Stream stream = File.OpenRead("people.xml");

        People? list  = serializer.Deserialize(stream) as People;
        foreach (Person person in list!)
        {
            // serializer.Serialize(writer, person, null);
            Console.WriteLine(person);
        }
    }

    private static void SerializeDemo()
    {
        People list = Generate(100);

        XmlSerializer serializer = new XmlSerializer(typeof(People));
        Stream stream = File.Create("people.xml");
        //XmlWriter writer = XmlWriter.Create(stream);
        //writer.WriteStartElement("persons");
        serializer.Serialize(stream, list);

        foreach (Person person in list)
        {
           // serializer.Serialize(writer, person, null);
        //    Console.WriteLine(person);
        }
    }

    private static People Generate(int v)
    {
        People lost = new People();
        for (int i = 0; i < v; i++) 
        {
            lost.Add(new Person() { Id = i, FirstName = $"First {i}", LastName = $"Last {i}", Age = Random.Shared.Next(0, 123) });
        }
        return lost;
    }
}
