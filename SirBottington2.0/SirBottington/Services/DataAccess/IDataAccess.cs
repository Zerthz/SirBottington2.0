using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirBottington.Services.DataAccess
{
    public interface IDataAccess<T>
    {
        Task<List<T>> GetAll();
        Task Insert(T entity);
        Task Update(T entity);
    }
}
