using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repositories.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repositories
{
    public class TrailRepository : ITrailRepository
    {
        private readonly AppDbContext _dbContext;

        public TrailRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<bool> CreateTrailAsync(Trail trail)
        {
            await _dbContext.Trails.AddAsync(trail);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTrailAsync(Trail trail)
        {
            _dbContext.Trails.Remove(trail);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistTrailByIdAsync(int id)
        {
            var re = await _dbContext.Trails.AnyAsync(n => n.Id == id);
            return re;
        }

        public async Task<bool> ExistTrailByNameAsync(string name)
        {
            return await _dbContext.Trails.AnyAsync(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
        }
        public async Task<IEnumerable<Trail>> GetTrailsByNationalParkIdAsync(int id)
        {
            
            return await _dbContext.Trails.Include(n => n.NationalPark).Where(n=>n.NationalParkId==id).ToListAsync();
            
        }

        public async Task<Trail> GetTrailByIdAsync(int id)
        {
            return await _dbContext.Trails.Include(n => n.NationalPark).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Trail> GetTrailByNameAsync(string name)
        {
            return await _dbContext.Trails.Include(n => n.NationalPark).FirstOrDefaultAsync(n => n.Name == name);
        }

        public async Task<IEnumerable<Trail>> GetTrailsAsync()
        {
            return await _dbContext.Trails.Include(n => n.NationalPark).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTrailAsync(Trail trail)
        {
            _dbContext.Trails.Update(trail);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
