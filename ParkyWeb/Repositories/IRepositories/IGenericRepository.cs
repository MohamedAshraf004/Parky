using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkyWeb.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<T> GetAsync(string url,int id);
        Task<bool> CreateAsync(string url,T objCreated);
        Task<bool> UpdateAsync(string url,T objUpdated);
        Task<bool> DeleteAsync(string url,int id);

    }
}
