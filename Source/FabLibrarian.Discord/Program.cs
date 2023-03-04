using System.Text.Json;
using Discord;
using Discord.WebSocket;
using FabLibrarian.Discord.Configuration;
using FabLibrarian.Discord.Services.Logging;
using FabLibrarian.Discord.Services.Messages;

var content = await File.ReadAllTextAsync("config.json");
var appConfig = JsonSerializer.Deserialize<ApplicationConfig>(content);

if (appConfig is null)
{
    Console.WriteLine("Failed to load application config from 'config.json'");
    return;
}

var config = new DiscordSocketConfig
{
    GatewayIntents =
        GatewayIntents.DirectMessageTyping | 
        GatewayIntents.DirectMessageReactions | 
        GatewayIntents.DirectMessages | 
        GatewayIntents.GuildMessageTyping | 
        GatewayIntents.GuildMessageReactions | 
        GatewayIntents.GuildMessages | 
        GatewayIntents.GuildVoiceStates |
        GatewayIntents.GuildWebhooks | 
        GatewayIntents.GuildIntegrations | 
        GatewayIntents.GuildEmojis | 
        GatewayIntents.GuildBans | 
        GatewayIntents.Guilds | 
        GatewayIntents.MessageContent
};

var client = new DiscordSocketClient(config);
_ = new LoggingService(client);
_ = new MessageService(client, appConfig);

await client.LoginAsync(TokenType.Bot, appConfig.Token);
await client.StartAsync();

await Task.Delay(-1);