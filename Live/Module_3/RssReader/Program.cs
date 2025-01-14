
using System.Xml;
using System.Xml.Serialization;

namespace RssReader;

internal class Program
{
    static void Main(string[] args)
    {
        foreach(int nr in GetNumbers())
        {
            Console.WriteLine(nr);
        }


        var stream = DoCallAsync().Result;
        var items = HandleStream(stream);
       foreach (var item in items)
        {
            Console.WriteLine($"** {item.Category}");
            Console.WriteLine($"- {item.Title}");
            Console.WriteLine(item.Description);
            Console.WriteLine();
        }
    }

    static IEnumerable<int> GetNumbers()
    {
        yield return 1;
        Console.WriteLine("een");
        yield return 2;
        Console.WriteLine("twee");
        yield return 3;
        Console.WriteLine("drie");
        yield return 4;
    }

    private static IEnumerable<Item?> HandleStream(Stream? stream)
    {
        if (stream == null) yield return null;

        var ser = new XmlSerializer(typeof(Item));
        var reader = XmlReader.Create(stream);
        //var items = new List<Item?>();
        while(reader.ReadToFollowing("item"))
        {          
            var item =  ser.Deserialize(reader.ReadSubtree()) as Item;
           //items.Add(item);
            yield return item;
        }
        //return items;
    }

    private static async Task<Stream?> DoCallAsync()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://www.nu.nl/rss");

        var response = await client.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStreamAsync();
        }
        return null;
    }
}
