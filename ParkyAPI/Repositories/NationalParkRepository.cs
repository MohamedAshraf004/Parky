using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkyAPI.Repositories
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly AppDbContext _dbContext;

        public NationalParkRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<bool> CreateNationalParkAsync(NationalPark nationalPark)
        {
            await _dbContext.NationalParks.AddAsync(nationalPark);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteNationalParkAsync(NationalPark nationalPark)
        {
            _dbContext.NationalParks.Remove(nationalPark);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistNationalParkByIdAsync(int id)
        {
            return await _dbContext.NationalParks.AnyAsync(n => n.Id == id);
        }

        public async Task<bool> ExistNationalParkByNameAsync(string name)
        {
            return await _dbContext.NationalParks.AnyAsync(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public async Task<NationalPark> GetNationalParkByIdAsync(int id)
        {
            return await _dbContext.NationalParks.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<NationalPark> GetNationalParkByNameAsync(string name)
        {
            return await _dbContext.NationalParks.FirstOrDefaultAsync(n => n.Name == name);
        }

        public async Task<IEnumerable<NationalPark>> GetNationalParksAsync()
        {
            return await  _dbContext.NationalParks.OrderBy(c=>c.Name).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0; 
        }

        public async Task<bool> UpdateNationalParkAsync(NationalPark nationalPark)
        {           
             _dbContext.NationalParks.Update(nationalPark);
             return await _dbContext.SaveChangesAsync()>0;
        }
    }
}
