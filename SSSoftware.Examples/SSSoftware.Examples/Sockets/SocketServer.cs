namespace SSSoftware.Examples.Sockets;

public class SocketServer
{
    public static async Task StartServerAsync(string host, int port, CancellationToken token)
    {
        var ipAddressParsed = IPAddress.TryParse(host, out var address);
        if (!ipAddressParsed)
        {
            throw new ArgumentException($"{nameof(host)} is not a IP address");
        }

        var remoteEndpoint = new IPEndPoint(address, port);

        var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            serverSocket.Bind(remoteEndpoint);
            serverSocket.Listen(port);

            // This will keep accepting clients until token is cancelled.
            while (!token.IsCancellationRequested)
            {
                var socket = await serverSocket.AcceptAsync(token);
                Console.WriteLine("[SERVER] Connected new client successfully");

                // This will accept and write out messages from the connected client in its own context, until disconnected.
                _ = ListenForMessagesAsync(socket);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("[SERVER] Something went wrong while binding or accepting connection: "+ex.Message);
        }
    }

    private static async Task ListenForMessagesAsync(Socket socket)
    {
        int bytesRead = 1;
        try
        {
            // Keep reading until bytes read is 0, as this indicates disconnected client. socket.Connected doesnt seem to work..??
            while (bytesRead > 0)
            {
                var buffer = new byte[1024];
                bytesRead = await socket.ReceiveAsync(buffer, SocketFlags.None);
                if (bytesRead == 0) continue;
                Console.WriteLine("[SERVER] Received message: "+Encoding.UTF8.GetString(buffer));
            }

            Console.WriteLine("[SERVER] Client disconnected");
        }
        catch (Exception ex)
        {
            Console.WriteLine("[SERVER] Error occurred receiving message: "+ex.Message);
        }
    }
}