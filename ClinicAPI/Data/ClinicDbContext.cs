using ClinicAPI.Auth.Models;
using ClinicAPI.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ClinicAPI.Data
{
    public class ClinicDbContext : IdentityDbContext<ClinicUser>
    {
        public ClinicDbContext() { }
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Procedure> Procedures { get; set; } 

    }
}
