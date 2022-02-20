using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

public static class NetworkHelper
{
    static string _ua { get; } = "insomnia/2021.7.2";

    public static void SendSlowPost(string host, int port, string ep, string content, TimeSpan packetDelay)
    {
        string headers = $"POST {ep} HTTP/1.1\r\n" +
                         $"Host: {host}\r\n" +
                         $"Connection: Keep-Alive\r\n" +
                         $"User-Agent: {_ua}\r\n" +
                         $"Accept: */*\r\n" +
                         $"Content-Type: application/json\r\n" +
                         $"Content-Length: {content.Length}\r\n\r\n";

        SendByTcpClient(host, port, headers, content, packetDelay);
    }

    static void SendByTcpClient(string server, int port, string data, string content, TimeSpan packetDelay)
    {
        using var client = new TcpClient(server, port);
        var header = Encoding.ASCII.GetBytes(data);
        using var stream = client.GetStream();

        var sw = new Stopwatch();
        sw.Start();
        stream.Write(header, 0, header.Length);
        Console.WriteLine("Sent headers");

        if (true)
        {
            Console.WriteLine("Sending content");
            var json = Encoding.ASCII.GetBytes(content);
            var length = json.Length;
            for (var n = 0; n < length; n++)
            {
                stream.Write(json, n, 1);
                Console.Write((char)json[n]);
                Thread.Sleep((int)packetDelay.TotalMilliseconds);
            }

            var end = Encoding.ASCII.GetBytes("\r\n\r\n");
            Console.WriteLine("\nSending EOD");
            stream.Write(end, 0, end.Length);
            Console.WriteLine($"Sent EOD - Total time: {sw.Elapsed.TotalSeconds} seconds");
        }

        Console.WriteLine("\nReading response");
        var read_buffer = new Byte[5120];
        int bytes_read;
        do
        {
            bytes_read = stream.Read(read_buffer, 0, read_buffer.Length);
            var response = Encoding.ASCII.GetString(read_buffer, 0, bytes_read);
            Console.WriteLine($"Received: {response}");
        } while (bytes_read == 0);
    }
}