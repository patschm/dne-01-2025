
using System.IO.Compression;
using System.Text;
using System.Xml;

namespace Stromingen;

internal class Program
{
    static void Main(string[] args)
    {
        //WriteLikeANeanderthaler();
        //ReadLikeANeanderthaler();
        //WriteModern();
        //ReadModern();
        //WriteModernZipped();
        //ReadModernZipped();
        WriteXml();
    }

    private static void WriteXml()
    {
        Directory.CreateDirectory(@"E:\test");
        FileInfo file = new FileInfo(@"E:\test\demo3.xml");
        if (file.Exists)
        {
            file.Delete();
        }
        FileStream fs = file.Create();
        XmlWriter writer = XmlWriter.Create(fs);
        writer.WriteStartElement("root");
        writer.WriteStartElement("person");
        writer.WriteAttributeString("id", "1");
        writer.WriteStartElement("first-name");
        writer.WriteString("Jane");

        writer.Close();
    }

    private static void ReadModernZipped()
    {
        FileStream fs = File.OpenRead(@"E:\test\demo2.zip");
        GZipStream zip = new GZipStream(fs, CompressionMode.Decompress);
        StreamReader reader = new StreamReader(zip);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
    private static void WriteModernZipped()
    {
        Directory.CreateDirectory(@"E:\test");
        FileInfo file = new FileInfo(@"E:\test\demo2.zip");
        if (file.Exists)
        {
            file.Delete();
        }
        FileStream fs = file.Create();
        GZipStream zip = new GZipStream(fs, CompressionMode.Compress);
        StreamWriter writer = new StreamWriter(zip);

        string txt = "Hello World";
        for (int i = 0; i < 1000; i++)
        {
            writer.WriteLine($"{txt} {i}");
        }
        writer.Flush();
        writer.Close();
     

    }

    private static void ReadModern()
    {
        FileStream fs = File.OpenRead(@"E:\test\demo2.txt");
        StreamReader reader = new StreamReader(fs);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
    private static void WriteModern()
    {
        Directory.CreateDirectory(@"E:\test");
        FileInfo file = new FileInfo(@"E:\test\demo2.txt");
        if (file.Exists)
        {
            file.Delete();
        }
        FileStream fs = file.Create();
        StreamWriter writer = new StreamWriter(fs);

        string txt = "Hello World";
        for (int i = 0; i < 1000; i++)
        {
            writer.WriteLine($"{txt} {i}");
        }
        writer.Flush();
        writer.Close();
        //fs.Flush();
        //fs.Close();
    }

    private static void ReadLikeANeanderthaler()
    {
        FileStream fs = File.OpenRead(@"E:\test\demo1.txt");
        byte[] buffer = new byte[6];
        int bytesRead = 1;

        while (bytesRead > 0)
        {
            Array.Clear(buffer, 0, buffer.Length);
            bytesRead = fs.Read(buffer, 0, buffer.Length);
            string line = Encoding.UTF8.GetString(buffer);
            Console.Write(line);
        }
    }

    private static void WriteLikeANeanderthaler()
    {
        Directory.CreateDirectory(@"E:\test");
        FileInfo file = new FileInfo(@"E:\test\demo1.txt");
        if (file.Exists )
        {
            file.Delete();
        }
        FileStream fs = file.Create();

        string txt = "Hello World";
        byte[] buffer;
        for (int i = 0; i < 1000; i++) 
        {
            buffer = Encoding.UTF8.GetBytes($"{txt} {i}\r\n");
            fs.Write(buffer, 0, buffer.Length);
        }

        fs.Flush();
        fs.Close();
    }
}
