using Discord;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KarateBot.Services
{
    public class LogService
    {
        private readonly SemaphoreSlim semaphore;

        public LogService()
        {
            semaphore = new SemaphoreSlim(1);
        }

        private string TimeStamp => DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm tt");
        const string format = "{0, -5}, {1, 5}";

        internal async Task LogAsync(LogMessage log)
        {
            await semaphore.WaitAsync();
            Console.WriteLine($"[{TimeStamp}] {string.Format(format, log.Source, $": {log.Message}")}");
            semaphore.Release();
        }
    }
}
