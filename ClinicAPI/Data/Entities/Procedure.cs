namespace ClinicAPI.Data.Entities
{
    public class Procedure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int visitId { get; set; }
        public int animalId { get; set; }
    }
}
