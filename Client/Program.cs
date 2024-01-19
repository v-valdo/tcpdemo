using System.Net;
using System.Net.Sockets;
using System.Text;

IPHostEntry host = await Dns.GetHostEntryAsync(Dns.GetHostName());
IPAddress address = host.AddressList[0];
IPEndPoint endpoint = new(address, 3000);

using TcpClient client = new();
await client.ConnectAsync(endpoint);

await using NetworkStream stream = client.GetStream();

byte[] buffer = new byte[1_024];
int received = await stream.ReadAsync(buffer);

string message = Encoding.UTF8.GetString(buffer, 0, received);
Console.WriteLine(message);