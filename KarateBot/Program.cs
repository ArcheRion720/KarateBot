using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KarateBot
{
    public class Program
    {
        public static void Main(string[] args)
            => new BotClient().Run().GetAwaiter().GetResult();
    }
}
