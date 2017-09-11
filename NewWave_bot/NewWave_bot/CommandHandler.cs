using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace NewWave_bot
{

    //creating the command handler class that will need to be installed with the installCommands() method?
    public class CommandHandler
    {
        private CommandService commands;
        private DiscordSocketClient bot;
        private IServiceProvider map;

        public CommandHandler (IServiceProvider provider)
        {
            map = provider;
            bot = map.GetService<DiscordSocketClient>();

            bot.MessageReceived += HandleCommand;
            commands = map.GetService<CommandService>();
        }

        public async Task ConfigureAsync()
        {
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(SocketMessage MessageParam)
        {
            var message = MessageParam as SocketUserMessage;
            if (message == null)
                return;
            var context = new SocketCommandContext(bot, message);

            int argPos = 0;

            if (message.HasStringPrefix(".", ref argPos))
            {
                if (message.Author.IsBot)
                    return;
                var result = await commands.ExecuteAsync(context, argPos, map);

                if (!result.IsSuccess && result.ErrorReason != "Unknown Command Dumbass.")

                    await message.Channel.SendMessageAsync("**Error:** {result.ErrorReason}");
            }

                    
        }


    }
}
