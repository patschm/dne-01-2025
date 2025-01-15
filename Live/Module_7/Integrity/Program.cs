
using System.Security.Cryptography;
using System.Text;

namespace Integrity;

internal class Program
{
    static void Main(string[] args)
    {
        //TestHash();
        //TestHMAC();
        TestDSA();
    }

    private static void TestHash()
    {
        string message = "Hello World";
        // Sender
        HashAlgorithm alg = SHA256.Create();
        byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(message));
        Console.WriteLine(Convert.ToBase64String(hash));

        // Ed
        message += ".";

        // Recipient
        var alg2 = SHA256.Create();
        byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(message));
        Console.WriteLine(Convert.ToBase64String(hash));
        Console.WriteLine(Convert.ToBase64String(hash2));
    }
    private static void TestHMAC()
    {
        // Symmetrisch
        string message = "Hello World";
        // Sender
        var alg = new HMACSHA256();
        byte[] key = alg.Key;
        byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(message));
        Console.WriteLine(Convert.ToBase64String(hash));

        // Ed
        //message += ".";

        // Recipient
        var alg2 = new HMACSHA256();
        //alg2.Key = key;
        byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(message));
        Console.WriteLine(Convert.ToBase64String(hash));
        Console.WriteLine(Convert.ToBase64String(hash2));
    }

    private static void TestDSA()
    {
        string message = "Hello World";
        // Sender
        HashAlgorithm alg = SHA256.Create();
        byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(message));
        var sa = DSA.Create();
        string pubKey = sa.ToXmlString(false);
        Console.WriteLine(pubKey);
        byte[] signature = sa.CreateSignature(hash);
        Console.WriteLine(Convert.ToBase64String(signature));

        // Ed
        //message += ".";

        // Recipient
        var alg2 = SHA256.Create();
        var sa2 = DSA.Create();
        sa2.FromXmlString(pubKey);
        byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(message));
        bool isOk = sa2.VerifySignature(hash2, signature);
        Console.WriteLine(isOk ? "Veilig": "Bedenkelijk");
    }
}
