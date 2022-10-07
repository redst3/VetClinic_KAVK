using ClinicAPI.Data.Dtos;
using ClinicAPI.Data.Entities;
using ClinicAPI.Helpers;

namespace ClinicAPI.Data.Repositories
{
    public interface IAnimalRepository
    {
        Task CreateAsync(Animal animal);
        Task<Animal?> GetAsync(int id);
        Task<IReadOnlyList<Animal>> GetListAsync();
        Task<PagedList<Animal>> GetListAsync(AnimalSearchParameters animalSearchParameters);
        Task RemoveAsync(Animal animal);
        Task UpdateAsync(Animal animal);
    }
}