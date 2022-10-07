namespace ClinicAPI.Data.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ConfirmedDate { get; set; }
        public bool isFinished { get; set; }
        public int AnimalId { get; set; }
    }
}
