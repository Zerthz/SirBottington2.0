using Discord;
using Discord.Commands;
using SirBottington.Services.API;
using SirBottington.Utilities;

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
       
        [Command("xkcd")]
        public async Task XKCDAsync(string arg = null)
        {
            if (arg is null)
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
            else if (String.Equals(arg, "r", StringComparison.OrdinalIgnoreCase))
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
            else if(int.TryParse(arg, out int num))
            {
                // Get latest comic to see if what our maximum is.
                var latestComic = await _api.GetLatest();
                Embed embed;
                // if they ask for maximum just return latest comic
                if(num == latestComic.num)
                {
                    embed = _util.XKCDEmbedBuilder(latestComic);
                }
                // Otherwise get that comic
                else if(num < latestComic.num && num > 0)
                {
                    var comic = await _api.GetSpecific(num);
                    embed = _util.XKCDEmbedBuilder(comic);
                }
                // If they ask for something higher than maximum tell them no
                else
                {
                    await ReplyAsync("That's not a comic that's available as of now. The latest comic at the moment is " + latestComic.num);
                    return;
                }
                await ReplyAsync(embed: embed);
            }
            else if(String.Equals(arg, "update", StringComparison.OrdinalIgnoreCase))
            {
                await _util.Update();
            }
            else if(arg is not null)
            {
                var embed = await _util.Search(arg);
                if(embed is not null)
                {
                    await ReplyAsync(embed: embed);
                }
                else
                {
                    await ReplyAsync("I failed to a find a match for that search");
                }
            }
            
        }

       


    }
}
