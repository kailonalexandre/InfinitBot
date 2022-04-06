using System;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using InfinitBott;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Bot.Commands;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace InfinitBott
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public async Task MainAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
            };


            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;


            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(5)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
              
            };
            
            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<Comandos>();
          
            
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        public async Task onChanged()
        {
            await Client.Guilds[950850839885525002].Channels[950850840334307340].SendMessageAsync("Versão Liberada");
        }
        private Task OnClientReady(object sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

    }
}
