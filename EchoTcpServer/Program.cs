using System;
using System.Net;
using System.Net;
using EchoServer.Handlers;
using EchoServer.Infrastructure;
using EchoServer.Server;
using EchoServer.Udp;

namespace EchoServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Setup dependencies
            var listener = new TcpListenerWrapper(IPAddress.Any, 5000);
            var clientHandler = new EchoClientHandler();
            var server = new EchoServer.Server.EchoServer(listener, clientHandler);

            // Start the server
            _ = Task.Run(() => server.StartAsync());

            // Setup UDP sender
            string host = "127.0.0.1";
            int port = 60000;
            int intervalMilliseconds = 5000;

            using (var sender = new UdpTimedSender(host, port))
            {
                Console.WriteLine("Press any key to stop sending...");
                sender.StartSending(intervalMilliseconds);

                Console.WriteLine("Press 'q' to quit...");
                while (Console.ReadKey(intercept: true).Key != ConsoleKey.Q)
                {
                    // Wait until 'q' is pressed
                }

                sender.StopSending();
                server.Stop();
                Console.WriteLine("Sender stopped.");
            }
        }
    }
}