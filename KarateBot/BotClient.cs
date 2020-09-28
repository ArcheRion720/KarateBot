using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using KarateBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KarateBot
{
    public class BotClient
    {
        private DiscordSocketClient Client { get; set; }
        private CommandService Command { get; set; }
        private LogService Log { get; set; }
        private IServiceProvider Services { get; set; }
        private IConfiguration Config { get; set; }

        public BotClient()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 50,
                LogLevel = LogSeverity.Debug
            });

            Command = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Verbose,
                CaseSensitiveCommands = false
            });

            Log = new LogService();
        }

        public async Task Run()
        {
            Config = BuildConfig();

            await Client.LoginAsync(TokenType.Bot, Config["token"]);
            await Client.StartAsync();

            Client.Log += LogAsync;

            Services = ConfigureServices();
            await Services.GetRequiredService<CommandHandlerService>().InitializeAsync(Services);

            await Task.Delay(-1);
        }

        private async Task LogAsync(LogMessage log)
            => await Log.LogAsync(log);

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(Client)
                .AddSingleton(Command)
                .AddSingleton(Config)
                .AddSingleton<CommandHandlerService>()
                .AddSingleton<LogService>()
                .BuildServiceProvider();
        }

        private IConfiguration BuildConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath($@"{Directory.GetCurrentDirectory()}\Resources")
                .AddJsonFile("config.json")
                .Build();
        }
    }
}
