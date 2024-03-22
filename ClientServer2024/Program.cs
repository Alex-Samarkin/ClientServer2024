using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer2024
{
    internal class Program
    {
        static async Task Main()
        {
            // Create an IP endpoint
            int port = 8000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(localAddr, port);

            // Create a TcpListener
            TcpListener server = new TcpListener(endPoint);
            server.Start();
            Console.WriteLine($"Listening on port {port}...");

            while (true)
            {
                // Accept incoming client connections
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

                // Handle client communication (read/write data)
                await HandleClientAsync(client);
            }
        }

        static async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {data}");

                    // Process data or send a response back to the client
                    // ...
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }
    }
}
