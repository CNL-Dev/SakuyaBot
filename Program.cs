﻿using Discord;
using Discord.WebSocket;
using SakuyaBot;
using System.Xml;

public class Program
{
    private DiscordSocketClient _client;
    private readonly string xmlFilePath = @"Resources\token.xml";

    public static Task Main(string[] args) => new Program().MainAsync();

    //Obtains the API token from a file and connects to a discord client
    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(xmlFilePath);
        XmlNodeList token = xmlDocument.GetElementsByTagName("token");

        await _client.LoginAsync(TokenType.Bot, token[0].InnerText);
        await _client.StartAsync();

        _client.Ready += () =>
        {
            Console.WriteLine("SakuyaBOT is connected!");
            return Task.CompletedTask;
        };

        _client.MessageReceived += Client_OnMessageReceived;

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

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}