using Discord;
using Discord.WebSocket;

namespace TotallyHuman
{
    internal class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync()
        {
            DiscordSocketClient client = new DiscordSocketClient();

            client.Log += Log;

            string token = File.ReadAllText(@"..\..\..\..\Token.txt");

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
