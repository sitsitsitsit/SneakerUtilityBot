using AJAXTools.Commands;
using AJAXTools.Utility;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AJAXTools
{
    class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        
        public static List<Proxy> _proxies = new List<Proxy>();
        public static int _currentProxy = 0;

        public async Task RunAsync()
        {
            string[] lines = System.IO.File.ReadAllLines(@"proxies.txt");
            foreach (var line in lines)
            {
                _proxies.Add(Proxy.ParseProxy(line));
            }

            // Bot connection information
            var config = new DiscordConfiguration
            {
                Token = "INSERT_TOKEN",
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            // Logs Client events
            Client.Ready += OnClientReady;

            // Sets prefix and other bot settings
            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { "!" },
                EnableMentionPrefix = true,
                EnableDms = false,
                CaseSensitive = false,
                EnableDefaultHelp = true,
                IgnoreExtraArguments = false,
                DmHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            // Registers command classes
            Commands.RegisterCommands<OtherCommands>();
            Commands.RegisterCommands<EbayCommand>();
            await Client.ConnectAsync();

            // Prevents connection from terminating
            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            // Logs client startup
            return Task.CompletedTask;
        }
    }
}
