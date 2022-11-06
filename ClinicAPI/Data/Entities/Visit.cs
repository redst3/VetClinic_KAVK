using ClinicAPI.Auth.Models;
using System.ComponentModel.DataAnnotations;

namespace ClinicAPI.Data.Entities
{
    public class Visit : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ConfirmedDate { get; set; }
        public bool isFinished { get; set; }
        public int AnimalId { get; set; }

        [Required]
        public string UserId { get; set; }
        public ClinicUser User { get; set; }
    }
}
