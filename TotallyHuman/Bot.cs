using Discord;
using Discord.WebSocket;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using TotallyHuman.Attributes;

namespace TotallyHuman
{
    public class Bot
    {
        delegate Task CommandHandler(SocketMessage message);

        private string m_Token;
        private string m_CommandPrefix = "!";
        private Dictionary<string, CommandHandler> m_CommandsDictionary = new();
        protected DiscordSocketClient m_Client;

        protected Bot(string token)
        {
            m_Token = token;
            PopulateCommands();
        }

        public async Task SetupClientAsync()
        {
            var config = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };

            m_Client = new DiscordSocketClient(config);

            m_Client.Log += Log;
            m_Client.MessageReceived += MessageHandler;

            await m_Client.LoginAsync(TokenType.Bot, m_Token);
            await m_Client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task MessageHandler(SocketMessage message)
        {
            string s = message.Content;

            if (message.Content.StartsWith(m_CommandPrefix))
            {
                if(m_CommandsDictionary.TryGetValue(s[m_CommandPrefix.Length..], out CommandHandler command))
                {
                    return command(message);
                }
            }

            return Task.CompletedTask;
        }

        private void PopulateCommands()
        {
            Type type = GetType();
            var methods = type.GetMethods();
            Type[] types = { typeof(SocketMessage) };

            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute<CommandAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                if (!method.GetParameters().Select(p => p.ParameterType).SequenceEqual(types))
                {
                    Console.WriteLine($"Invalid signature on command {method.Name}");
                    continue;
                }

                if (method.ReturnType != typeof(Task))
                {
                    Console.WriteLine($"Invalid return type on command {method.Name}");
                    continue;
                }

                m_CommandsDictionary.Add(attribute.m_CommandName, method.CreateDelegate<CommandHandler>(this));
               
            }
        }
    }
}
