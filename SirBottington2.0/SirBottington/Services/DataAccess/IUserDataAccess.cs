using SirBottington.Models;

namespace SirBottington.Services.DataAccess
{
    public interface IUserDataAccess : IDataAccess<UserModel>
    {
        Task<UserModel> GetUser(string id);
    }
}