using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NewWave_bot.Properties;
using System.IO;

namespace NewWave_bot
{
    class Program
    {
        private CommandService commands;
        private CommandHandler handler;
        private DiscordSocketClient client;
        private IServiceProvider services;

        string token = NewWave_bot.Properties.Settings.Default.botToken;

        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();
        
        public async Task Start()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection().BuildServiceProvider();

            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose
            });

            client.Log += Log;
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            var serviceProvider = ConfigureServices();
            handler = new CommandHandler(serviceProvider);
            await handler.ConfigureAsync();


            await Task.Delay(-1);
        }

       
        //copied from https://github.com/MisterMorde/Bot.Template/blob/master/Bot.Template/Program.cs
        //didn't feel like typing out the logger. 
        private static Task Log(LogMessage lmsg)
        {
            var cc = Console.ForegroundColor;
            switch (lmsg.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now} [{lmsg.Severity,8}] {lmsg.Source}: {lmsg.Message}");
            Console.ForegroundColor = cc;
            return Task.CompletedTask;
        }

        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(new CommandService(new CommandServiceConfig { CaseSensitiveCommands = false }));
            var provider = new DefaultServiceProviderFactory().CreateServiceProvider(services);

            return provider;
        }

    }
}
