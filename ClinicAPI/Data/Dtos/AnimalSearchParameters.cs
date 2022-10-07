namespace ClinicAPI.Data.Dtos
{
    public class AnimalSearchParameters
    {
        //api/animals
        private int _pageSize = 3;
        private const int maxSize = 10;

        public int pageNumber { get; set; } = 1;

        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxSize ? maxSize : value;
        }
    }
}
