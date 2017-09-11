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

namespace NewWave_bot
{
    class Program
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private IServiceProvider services;

        string token = "";
        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();
        
        public async Task Start()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection().BuildServiceProvider();
            
            await InstallCommands(); //need to figure out how to define this method

            await client.LoginAsync(TokenType.Bot, token);

            await client.StartAsync();

            client.Log += Log;

            await Task.Delay(-1);
        }
        
    }
}
