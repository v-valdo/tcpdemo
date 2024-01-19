using System.Net;
using System.Net.Sockets;
using System.Text;

IPHostEntry host = await Dns.GetHostEntryAsync(Dns.GetHostName());
IPAddress address = host.AddressList[0];
IPEndPoint endpoint = new(address, 3000);

TcpListener listener = new(endpoint);

try
{
    listener.Start();
    Console.WriteLine();
    using TcpClient handler = await listener.AcceptTcpClientAsync();
    await using NetworkStream stream = handler.GetStream();

    string message = $"Connection Established";
    byte[] bytes = Encoding.UTF8.GetBytes(message);

    await stream.WriteAsync(bytes);
}

finally
{
    listener.Stop();
}
