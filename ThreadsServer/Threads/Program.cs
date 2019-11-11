using System;
using System.Threading;

namespace ThreadsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(delegate ()
            {
                // replace the IP with your system IP Address...
                Server myserver = new Server("127.0.0.1", 13000);
            });
            t.Start();

            Console.WriteLine("Server Started...!");
        }
    }
}