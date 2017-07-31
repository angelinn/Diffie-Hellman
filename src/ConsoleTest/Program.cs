using DiffieHellman;
using System;
using System.Text;

namespace ConsoleTest
{
    class Program
    {
        public static void PrintByteArray(byte[] bytes)
        {
            var sb = new StringBuilder("new byte[] { ");
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }
            sb.Append("}");
            Console.WriteLine(sb.ToString());
        }

        static void Main(string[] args)
        {
            DiffieHellmanEncryption client = new DiffieHellmanEncryption();
            Payload request = client.GenerateRequest();

            DiffieHellmanEncryption server = new DiffieHellmanEncryption();
            Payload response = server.GenerateResponse(request);

            client.FinalizeKey(response);

            PrintByteArray(client.Secret);
            PrintByteArray(server.Secret);
        }
    }
}
