using API.Data.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AktorRepository : IAktorRepository
    {
        private DataContext _dbContext;

        public AktorRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAktorToDatabase(Aktor aktor)
        {
            _dbContext.Aktors.Add(aktor);

        }

        public async Task<bool> DoesAktorWithIdExist(string aktorId)
        {
            return await _dbContext.Aktors.AnyAsync(a => a.AktorId == aktorId);

        }

        public async Task<Aktor> GetAktorByIdAsync(string aktorId)
        {
            return await _dbContext.Aktors.SingleOrDefaultAsync(a => a.AktorId == aktorId);
        }

        public async Task<IEnumerable<Aktor>> GetAktorsAsync(int currentPage, int maxPerPage)
        {
            return await _dbContext.Aktors.OrderBy(a => a.AktorId).Skip((currentPage - 1) * maxPerPage).Take(maxPerPage).ToListAsync();
        }

        public void Update(Aktor aktor)
        {
            _dbContext.Entry(aktor).State = EntityState.Modified;

        }
    }
}