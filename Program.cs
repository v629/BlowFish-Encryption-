using System;
using System.Text;

using System.IO;

namespace Blowfish_encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Path : ");
            string filePath = Console.ReadLine();

            Console.WriteLine("Password : ");
            string password = Console.ReadLine();

            char input;
            while (true)
            {
                Console.WriteLine("a) Encrypt");
                Console.WriteLine("b) Decrypt");
                Console.WriteLine("c) Exit");

                input = Console.ReadKey().KeyChar;

                if (input == 'c')
                {
                    break;
                }
                else if (input == 'a')
                {
                    EncryptFile(filePath, password);
                }
                else if (input == 'b')
                {
                    DecryptFile(filePath, password);
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }

        private static void EncryptFile(string filePath, string password)
        {
            byte[] content = File.ReadAllBytes(filePath);
            byte[] key = Encoding.UTF8.GetBytes(password);

            var memStream = new MemoryStream();

            var bf = new Blowfish(key);

            BlowfishStream bfStream = new BlowfishStream(memStream, bf, BlowfishStream.Target.Normal);

            bfStream.Write(content, 0, content.Length);
            bfStream.Flush();

            File.WriteAllBytes(filePath, memStream.ToArray());

            Console.WriteLine("Encrypted successfully : " + filePath);
        }

        private static void DecryptFile(string filePath, string password)
        {
            byte[] content = File.ReadAllBytes(filePath);
            byte[] key = Encoding.UTF8.GetBytes(password);

            var memStream = new MemoryStream();

            var bf = new Blowfish(key);

            BlowfishStream bfStream = new BlowfishStream(memStream, bf, BlowfishStream.Target.Encrypted);

            bfStream.Write(content, 0, content.Length);
            bfStream.Flush();
            File.WriteAllBytes(filePath, memStream.ToArray());

            Console.WriteLine("Decrypted successfully : " + filePath);
        }
    }
}
