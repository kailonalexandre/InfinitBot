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
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext.Attributes;


namespace MyFirstBot
{

    class Program
    {
        delegate Task invokeMethodDelegate(FileSystemEventArgs e);
        static Bot bot = new InfinitBott.Bot();
        static DateTime lastMessageSended = DateTime.MinValue;
        public DiscordClient Client { get; private set; }
        public DiscordMessage Message { get; set; }
        static void Main(string[] args)
        {
            bot = new InfinitBott.Bot();
            //Chama os métodos de monitoramento de arquivos
            invokeMethodDelegate mymethod = new invokeMethodDelegate(onChanged);
            Watcher w1 = new Watcher(@"D:\Outros Plus\Versões Plus", "*.zip", mymethod);
            w1.StartWatch();

            Watcher w2 = new Watcher(@"D:\Outros Comercial\Versões Comercial", "*.zip", mymethod);
            w2.StartWatch();

            //Inicia o Bot
            bot.MainAsync().GetAwaiter().GetResult();
        }

        public static async Task onChanged(FileSystemEventArgs e)
        {
            var result = lastMessageSended.Subtract(DateTime.Now).TotalMinutes * -1;
            if (result < 1) return;
            bool IsPlus = e.Name.ToLower().Contains("plus");
            ulong channelId = (ulong)(IsPlus ? 000 : 1111);

            DiscordChannel channel = await bot.Client.GetChannelAsync(channelId);

            DiscordEmbedBuilder message = new DiscordEmbedBuilder()
            {
                Title = "Versão Liberada",
                Description = e.Name,
                Color = IsPlus ? DiscordColor.Blue : DiscordColor.Purple

            };
            await channel.SendMessageAsync(message);
        }
    }
}