using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static async Task Main()
        {
            try
            {
                // Server IP address and port
                string serverIp = "127.0.0.1"; // Change this to your server's IP
                int serverPort = 8000; // Change this to your server's port

                // Create a TcpClient and connect to the server
                TcpClient client = new TcpClient();
                await client.ConnectAsync(serverIp, serverPort);
                Console.WriteLine($"Connected to {serverIp}:{serverPort}");

                // Send data to the server
                string messageToSend = "Hello, server!"; // Your message here
                byte[] dataToSend = Encoding.ASCII.GetBytes(messageToSend);
                await client.GetStream().WriteAsync(dataToSend, 0, dataToSend.Length);
                Console.WriteLine($"Sent: {messageToSend}");

                // Receive data from the server
                byte[] buffer = new byte[1024];
                int bytesRead = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {receivedData}");

                // Close the connection
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
