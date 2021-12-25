using Discord.Commands;
using SirBottington.Services.API;
using SirBottington.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirBottington.Modules
{
    [Name("XKCD")]
    [Summary("Commands related to the XKCD feature branch of SirBottington")]
    public class XKCDModule : ModuleBase<SocketCommandContext>
    {
        private readonly XKCDUtil _util;
        private readonly GetXKCDAPI _api;
        private readonly Random _r;

        public XKCDModule(XKCDUtil util, GetXKCDAPI api, Random r)
        {
            _util = util;
            _api = api;
            _r = r;
        }

        [Command("xkcd update")]
        [RequireOwner]
        public async Task UpdateXKCDAsync()
        {
            
        }
       

        [Command("xkcd")]
        public async Task XKCDAsync(string arg = null)
        {
            if(arg is null)
            {
                var latestComic = await _api.GetLatest();
                var embed = _util.XKCDEmbedBuilder(latestComic);
                await ReplyAsync(embed: embed);
            }
            else if (String.Equals(arg, "random", StringComparison.OrdinalIgnoreCase))
            {
                var randomComic = await _api.GetSpecific(221);
                var embed = _util.XKCDEmbedBuilder(randomComic);
                await ReplyAsync(embed: embed);
            }
            else if(String.Equals(arg, "r", StringComparison.OrdinalIgnoreCase))
            {
                // Get latest comic so we know what our top random number should be
                var latestComic = await _api.GetLatest();
                var latestNumber = latestComic.num;

                // randomize between 1 and max number, store in variable
                var randomComicNum = _r.Next(1, latestNumber + 1);

                // if variable is 404, it's instead 403. otherwise use variable.
                randomComicNum = randomComicNum == 404 ? 403 : randomComicNum;

                // build an embed with random

                var randomComic = await _api.GetSpecific(randomComicNum);
                var embed = _util.XKCDEmbedBuilder(randomComic);
                await ReplyAsync(embed: embed);
            }
            
        }

       


    }
}
