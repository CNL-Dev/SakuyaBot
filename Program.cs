using Discord;
using Discord.Commands;
using Discord.WebSocket;
using SakuyaBot;
using System.Xml;

public class Program
{
    private DiscordSocketClient client;
    private readonly string xmlFilePath = @"Resources\token.xml";

    public static Task Main(string[] args) => new Program().MainAsync();

    //Obtains the API token from a file and connects to a discord client
    public async Task MainAsync()
    {
        client = new DiscordSocketClient();
        CommandService commandService = new CommandService();
        LoggingService logging = new LoggingService(client, commandService);

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(xmlFilePath);
        XmlNodeList token = xmlDocument.GetElementsByTagName("token");

        CommandHandler commandHandler = new CommandHandler(client, commandService);

        await client.LoginAsync(TokenType.Bot, token[0].InnerText);
        await client.StartAsync();

        client.Ready += () =>
        {
            Console.WriteLine("SakuyaBOT is connected!");
            return Task.CompletedTask;
        };

        await commandHandler.InstallCommandsAsync();

        await Task.Delay(-1);
    }

    private Task Client_OnMessageReceived(SocketMessage arg)
    {
        if (arg.Content.StartsWith("!helloworld"))
        {
            arg.Channel.SendMessageAsync($"User '{arg.Author.Username}' successfully ran helloworld!");
        }

        return Task.CompletedTask;
    }
}