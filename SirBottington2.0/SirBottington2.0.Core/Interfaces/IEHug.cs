using SirBottington.Domain.Models;

namespace SirBottington.Core.Interfaces
{
    public interface IEHugService
    {
        Task<Affirmation> GetEHugAsync();
    }
}