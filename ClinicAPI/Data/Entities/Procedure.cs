namespace ClinicAPI.Data.Entities
{
    public class Procedure
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }


    }
}
