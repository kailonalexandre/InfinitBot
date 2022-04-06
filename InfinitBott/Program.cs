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
    public sealed class Singleton
    {
        // The Singleton's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        private Singleton() 
        {

        }

        // The Singleton's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static Singleton _instance;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.
        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

        // Finally, any singleton should define some business logic, which can
        // be executed on its instance.
        public void someBusinessLogic()
        {
            // ...
        }
    }
    class Program
    {
        delegate Task invokeMethodDelegate(FileSystemEventArgs e);

        public DiscordClient Client { get; private set; }
        public DiscordMessage Message { get; set; }
        static void Main(string[] args)
        {
            var bot = new InfinitBott.Bot();

            //Chama os métodos de monitoramento de arquivos
            invokeMethodDelegate mymethod = new invokeMethodDelegate(onChanged);
            Watcher w1 = new Watcher(@"D:\Outros Plus\Versões Plus", "*.*", mymethod);
            w1.StartWatch();

            Watcher w2 = new Watcher(@"D:\Outros Comercial\Versões Comercial", "*.*", mymethod);
            w2.StartWatch();

            //Inicia o Bot
            bot.MainAsync().GetAwaiter().GetResult();
        }

        public async Task onChanged(FileSystemEventArgs e)
        {
            await Client.Guilds[950850839885525002].Channels[950850840334307340].SendMessageAsync("Versão Liberada");
        }
    }
}