using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace SakuyaBot
{
    // The LoggingService will handle the console logs for the bot. 
    public class LoggingService
    {
        // Subscribe the client and command logs to the LogAsync upon construction
        public LoggingService(DiscordSocketClient client, CommandService command)
        {
            client.Log += LogAsync;
            command.Log += LogAsync;
        }

        private Task LogAsync(LogMessage message)
        {
            if(message.Exception is CommandException cmdException)
            {
                Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
                    + $" failed to execute in {cmdException.Context.Channel}.");
                Console.WriteLine(cmdException.Message);
            }
            else
            {
                Console.WriteLine($"[General/{message.Severity}] {message}");
            }

            return Task.CompletedTask;
        }
    }
}
