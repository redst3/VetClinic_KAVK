using ClinicAPI.Data.Dtos;
using ClinicAPI.Data.Entities;
using ClinicAPI.Helpers;
using Microsoft.EntityFrameworkCore;
namespace ClinicAPI.Data.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ClinicDbContext _context;
        public AnimalRepository(ClinicDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Animal?> GetAsync(int id)
        {
            return await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IReadOnlyList<Animal>> GetListAsync()
        {
            return await _context.Animals.ToListAsync();
        }
        public async Task<PagedList<Animal>> GetListAsync(AnimalSearchParameters animalSearchParameters)
        {
            var queryable = _context.Animals.AsQueryable().OrderBy(a => a.Id);
            return await PagedList<Animal>.CreateAsync(queryable, animalSearchParameters.pageNumber,
                animalSearchParameters.pageSize);
        }

        public async Task CreateAsync(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Animal animal)
        {
            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Animal animal)
        {
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
        }
    }
}
