using Discord;
using Discord.Commands;
using Discord.Rest;

namespace FabLibrarian.Discord;

public class LoggingService
{
    public LoggingService(BaseDiscordClient client)
    {
        client.Log += LogAsync;
    }
    
    private static async Task LogAsync(LogMessage message)
    {
        await Task.Run(() =>
        {
            if (message.Exception is CommandException cmdException)
            {
                Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}" +
                                  $" failed to execute in {cmdException.Context.Channel}.");
                Console.WriteLine(cmdException);
            }
            else
            {
                Console.WriteLine($"[General/{message.Severity}] {message}");
            }
        });
    }
}