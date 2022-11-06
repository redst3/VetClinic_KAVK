using ClinicAPI.Auth.Models;
using System.ComponentModel.DataAnnotations;

namespace ClinicAPI.Data.Entities
{
    public class Procedure : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int visitId { get; set; }
        public int animalId { get; set; }

        [Required]
        public string UserId { get; set; }
        public ClinicUser User { get; set; }
    }
}
