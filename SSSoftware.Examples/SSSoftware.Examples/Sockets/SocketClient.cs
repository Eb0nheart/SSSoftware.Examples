namespace SSSoftware.Examples.Sockets;

public class SocketClient
{
    private Socket _socket;
    private string _host;
    private int _port;

    public async Task ConnectAsync(string host, int port)
    {
        _host = host;
        _port = port;
        var ipParsed = IPAddress.TryParse(host, out var ip);
        if (!ipParsed)
        {
            throw new ArgumentException($"{nameof(host)} was invalid, couldnt parse");
        }
        
        var remoteEndpoint = new IPEndPoint(ip, port);

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            await _socket.ConnectAsync(remoteEndpoint);
            Console.WriteLine("[CLIENT] Connected to server successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred connecting to server: "+ex.Message);
        }
    }

    public void Disconnect()
    {
        _socket.Shutdown(SocketShutdown.Both);
        _socket.Close();
        Console.WriteLine("[CLIENT] Disconnected to server successfully");
    }

    public async Task Reconnect()
    {
        Disconnect();
        await ConnectAsync(_host, _port);
    }

    public async Task<int> SendMessageAsync(string message)
        => await _socket.SendAsync(Encoding.UTF8.GetBytes(message), SocketFlags.None);
}
