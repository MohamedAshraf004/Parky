using ParkyAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface INationalParkRepository
    {
        Task<bool> CreateNationalParkAsync(NationalPark nationalPark);
        Task<bool> DeleteNationalParkAsync(NationalPark nationalPark);
        Task<bool> ExistNationalParkByIdAsync(int id);
        Task<bool> ExistNationalParkByNameAsync(string name);
        Task<NationalPark> GetNationalParkByIdAsync(int id);
        Task<NationalPark> GetNationalParkByNameAsync(string name);
        Task<IEnumerable<NationalPark>> GetNationalParksAsync();
        Task<bool> SaveAsync();
        Task<bool> UpdateNationalParkAsync(NationalPark nationalPark);
    }
}