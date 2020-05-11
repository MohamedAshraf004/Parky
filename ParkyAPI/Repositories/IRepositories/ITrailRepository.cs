using ParkyAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkyAPI.Repositories.IRepositories
{
    public interface ITrailRepository
    {
        Task<bool> CreateTrailAsync(Trail trail);
        Task<bool> DeleteTrailAsync(Trail trail);
        Task<bool> ExistTrailByIdAsync(int id);
        Task<bool> ExistTrailByNameAsync(string name);
        Task<Trail> GetTrailByIdAsync(int id);
        Task<Trail> GetTrailByNameAsync(string name);
        Task<IEnumerable<Trail>> GetTrailsByNationalParkIdAsync(int id);
        Task<IEnumerable<Trail>> GetTrailsAsync();
        Task<bool> SaveAsync();
        Task<bool> UpdateTrailAsync(Trail trail);
    }
}