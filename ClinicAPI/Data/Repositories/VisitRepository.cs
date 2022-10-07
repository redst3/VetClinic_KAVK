using ClinicAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Data.Repositories
{
    public interface IVisitRepository
    {
        Task CreateAsync(Visit visit);
        Task<Visit?> GetAsync(int animalId, int id);
        Task<IReadOnlyList<Visit>> GetListAsync(int animalId);
        Task RemoveAsync(Visit visit);
        Task UpdateAsync(Visit visit);
    }

    public class VisitRepository : IVisitRepository
    {
        private readonly ClinicDbContext _context;
        public VisitRepository(ClinicDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Visit?> GetAsync(int animalId, int id)
        {
            return await _context.Visits.FirstOrDefaultAsync(v => v.AnimalId == animalId && v.Id == id);
        }

        public async Task<IReadOnlyList<Visit>> GetListAsync(int animalId)
        {
            return await _context.Visits.Where(v => v.AnimalId == animalId).ToListAsync();
        }

        public async Task CreateAsync(Visit visit)
        {
            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Visit visit)
        {
            _context.Visits.Update(visit);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Visit visit)
        {
            _context.Visits.Remove(visit);
            await _context.SaveChangesAsync();
        }
    }
}
