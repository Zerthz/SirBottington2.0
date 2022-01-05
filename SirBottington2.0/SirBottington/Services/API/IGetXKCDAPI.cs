using SirBottington.Models;

namespace SirBottington.Services.API
{
    public interface IGetXKCDAPI
    {
        Task<IXKCDModel> GetLatest();
        Task<IXKCDModel> GetSpecific(int number);
    }
}