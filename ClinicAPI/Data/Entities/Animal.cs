using ClinicAPI.Auth.Models;
using System.ComponentModel.DataAnnotations;

namespace ClinicAPI.Data.Entities
{
    public class Animal : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }

        [Required]
        public string UserId { get; set; }
        public ClinicUser User { get; set; }


    }
}
