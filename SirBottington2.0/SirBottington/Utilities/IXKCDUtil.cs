using Discord;
using SirBottington.Models;

namespace SirBottington.Utilities
{
    public interface IXKCDUtil
    {
        Task<Embed> Search(string arg);
        Task Update();
        Embed XKCDEmbedBuilder(IXKCDModel comic);
        Embed XKCDSearchEmbedBuilder(IXKCDModel comic, string arg, int score);
    }
}