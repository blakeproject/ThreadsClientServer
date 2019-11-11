using System;
using System.Net.Sockets;
using System.Threading;

namespace ThreadsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set i = number of threads to start
            for (var i = 0; i < 10000; i++)
            {
                var j = i;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Connect("127.0.0.1", $"Hello I'm Device {j}...");
                }).Start();
            }
            Console.ReadLine();
        }

        static void Connect(String server, String message)
        {
            try
            {
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                int count = 0;
                while (count++ < 3)
                {
                    // Translate the Message into ASCII.
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                    // Send the message to the connected TcpServer. 
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", message);

                    // Bytes Array to receive Server Response.
                    data = new Byte[1024];
                    String response = String.Empty;

                    // Read the Tcp Server Response Bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", response);
                }

                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }

            Console.Read();
        }
    }
}