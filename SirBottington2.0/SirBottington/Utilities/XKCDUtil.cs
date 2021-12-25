using Discord;
using SirBottington.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirBottington.Utilities
{
    public class XKCDUtil
    {
        private readonly EmbedBuilder _embedBuilder;

        public XKCDUtil(EmbedBuilder embedBuilder)
        {
            _embedBuilder = embedBuilder;
        }
        public Embed XKCDEmbedBuilder(XKCDModel comic)
        {            
            _embedBuilder.WithTitle(comic.title)
                      .WithDescription(comic.link)
                      .WithFooter($"Alt: {comic.alt}")
                      .WithImageUrl(comic.img);
            return _embedBuilder.Build();
        }
    }
}
