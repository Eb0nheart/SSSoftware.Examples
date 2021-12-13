using SSSoftware.Examples.Sockets;

var client = new SocketClient();

var host = "127.0.0.1";
var port = 6000;
var tokenSource = new CancellationTokenSource();

_ = SocketServer.StartServerAsync(host, port, tokenSource.Token);

Console.WriteLine("Server is ready, press enter to connect client!");
Console.ReadLine();
await client.ConnectAsync(host, port);

Console.WriteLine("Write messages to send from client to server. Input 'Q' to quit!");
Console.WriteLine("If you input rc, client will reconnect");
var input = Console.ReadLine();
while(input.ToLowerInvariant() != "q")
{
    if(input.ToLowerInvariant() == "rc")
    {
        await client.Reconnect();
    }
    else
    {
        await client.SendMessageAsync(input);
    }
    
    input = Console.ReadLine();
}