﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace NewWave_bot.Modules
{
    public class TestModule : ModuleBase
    {
        [Command("test")] //how to actually call the bot to do something
        [Remarks("blah blah, testing bot code")]
        public async Task test()
        {
            try
            {
                await ReplyAsync(DefineAnewMethod());
            }
            catch ( Exception e)
            {
                await ReplyAsync(e.ToString());
            }
        }

        public string DefineAnewMethod()
        {

            String someText;
            someText = " Hi, I am a  method, I just want to be represented";

            return someText;

        }



    }
}
