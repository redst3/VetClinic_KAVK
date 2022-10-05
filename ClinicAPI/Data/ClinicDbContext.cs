using ClinicAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace ClinicAPI.Data
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext() { }
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Procedure> Procedures { get; set; } 
        public DbSet<User> Users { get; set; }

        

    }
}
