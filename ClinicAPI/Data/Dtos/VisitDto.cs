namespace ClinicAPI.Data.Dtos
{
    public record VisitDto(int id, string description, bool isFinished, int animalId);
}
