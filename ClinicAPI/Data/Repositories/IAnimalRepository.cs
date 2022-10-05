using ClinicAPI.Data.Entities;

namespace ClinicAPI.Data.Repositories
{
    public interface IAnimalRepository
    {
        Task CreateAsync(Animal animal);
        Task<Animal?> GetAsync(int id);
        Task<IReadOnlyList<Animal>> GetListAsync();
        Task RemoveAsync(Animal animal);
        Task UpdateAsync(Animal animal);
    }
}