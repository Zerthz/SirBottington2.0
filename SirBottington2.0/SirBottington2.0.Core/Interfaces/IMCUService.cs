using Discord;
using SirBottington.Domain.Models;

namespace SirBottington.Core.Interfaces
{
    public interface IMCUService
    {
        Task<Embed> GetMCUCountownEmbedAsync();
    }
}