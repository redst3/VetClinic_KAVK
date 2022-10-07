namespace ClinicAPI.Data.Dtos
{
    public record ProcedureDto(int id, string name, string description, decimal cost, int visitId, int animalId);
}
