using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KarateBot.Services
{
    public class CommandHandlerService
    {
        private readonly DiscordSocketClient Client;
        private readonly CommandService Commands;
        private readonly IConfiguration Config;
        private readonly LogService Log;
        private IServiceProvider Provider;

        public CommandHandlerService(IServiceProvider provider, DiscordSocketClient client, CommandService commands, LogService log, IConfiguration config)
        {
            Client = client;
            Commands = commands;
            Provider = provider;
            Config = config;
            Log = log;

            Client.MessageReceived += HandleMessage;
            Commands.Log += CommandsLog;
        }


        public async Task InitializeAsync(IServiceProvider provider)
        {
            Provider = provider;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Provider);
        }

        private Task CommandsLog(LogMessage log)
            => Log.LogAsync(log);

        private async Task HandleMessage(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message))
                return;

            if (message.Source != MessageSource.User)
                return;

            int argPos = 0;
            if (!message.HasStringPrefix(Config["commandPrefix"], ref argPos))
                return;

            var context = new SocketCommandContext(Client, message);
            var result = await Commands.ExecuteAsync(context, argPos, Provider);

            if (result.Error.HasValue && result.Error.Value != CommandError.UnknownCommand)
                await Log.LogAsync(new LogMessage(LogSeverity.Error, "MessageHandler", result.ErrorReason));
        }
    }
}
