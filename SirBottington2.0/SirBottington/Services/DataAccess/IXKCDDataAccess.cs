using SirBottington.Models;

namespace SirBottington.Services.DataAccess
{
    public interface IXKCDDataAccess : IDataAccess<XKCDModel>
    {
        Task<Dictionary<string, XKCDModel>> GetHistory();
        Task InsertComicHistory(XKCDSearchModel comic);
    }
}