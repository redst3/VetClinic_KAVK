using ClinicAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Data.Repositories
{
    public interface IProcedureRepository
    {
        Task CreateAsync(Procedure procedure);
        Task<Procedure?> GetAsync(int animalId, int visitId, int id);
        Task<IReadOnlyList<Procedure>> GetListAsync(int animalId, int visitId);
        Task RemoveAsync(Procedure procedure);
        Task UpdateAsync(Procedure procedure);
    }

    public class ProcedureRepository : IProcedureRepository
    {
        private readonly ClinicDbContext _context;
        public ProcedureRepository(ClinicDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Procedure?> GetAsync(int animalId, int visitId, int id)
        {
            return await _context.Procedures.FirstOrDefaultAsync(v => v.animalId == animalId && v.visitId == visitId && v.Id == id);
        }

        public async Task<IReadOnlyList<Procedure>> GetListAsync(int animalId, int visitId)
        {
            return await _context.Procedures.Where(v => v.animalId == animalId && v.visitId == visitId).ToListAsync();
        }

        public async Task CreateAsync(Procedure procedure)
        {
            _context.Procedures.Add(procedure);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Procedure procedure)
        {
            _context.Procedures.Update(procedure);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Procedure procedure)
        {
            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync();
        }
    }
}
