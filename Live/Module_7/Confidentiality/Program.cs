
using System.Security.Cryptography;
using System.Text;

namespace Confidentiality;

internal class Program
{
    static void Main(string[] args)
    {
        //Asymmetrisch();
        Symmetrisch();
    }

    private static void Symmetrisch()
    {
        // Zender
        string message = "Hello World";
        Aes alg = Aes.Create();
        //alg.Mode = CipherMode.CBC;
        byte[] key = alg.Key;
        byte[] iv = alg.IV;

        byte[] crypto;
        using(MemoryStream ms = new MemoryStream()) 
        {
            using (var cstr = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write))        
            using (var writer = new StreamWriter(cstr))
            {
                writer.Write(message);
            }
            crypto = ms.ToArray();
        }

        // Ontvanger
        Aes alg2= Aes.Create();
        //alg2.Mode = CipherMode.CBC;
        alg2.Key = key;
        alg2.IV = iv;

        using(var memstr= new MemoryStream(crypto))
        using(var cs2 = new CryptoStream(memstr, alg2.CreateDecryptor(), CryptoStreamMode.Read))
        using (var reader = new StreamReader(cs2))
        {
            var data = reader.ReadToEnd();
            Console.WriteLine(data);
        }
    }

    private static void Asymmetrisch()
    {
        // Ontvanger genereert een public key
        RSA rsaOntvanger = RSA.Create();
        string pubKey = rsaOntvanger.ToXmlString(false);

        // Zender
        string message = "Hello World";
        RSA rsaSender = RSA.Create();
        rsaSender.FromXmlString(pubKey);
        byte[] data = Encoding.UTF8.GetBytes(message);
        byte[] crypt = rsaSender.Encrypt(data, RSAEncryptionPadding.OaepSHA256);

        // Ed wil lezen
        //Console.WriteLine(Encoding.UTF8.GetString(crypt));

        // Ontvanger
        byte[] data2 = rsaOntvanger.Decrypt(crypt, RSAEncryptionPadding.OaepSHA256);
        Console.WriteLine(Encoding.UTF8.GetString(data2));
    }
}
