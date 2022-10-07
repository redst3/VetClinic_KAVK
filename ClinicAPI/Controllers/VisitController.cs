using Microsoft.AspNetCore.Mvc;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/animals/{animalId}/visits")]
    public class VisitController : ControllerBase
    {
        //private readonly IAnimalRepository _repository;

        //public AnimalController(IAnimalRepository repository)
        //{
        //    _repository = repository;
        //}

        //[HttpGet]
        //public async Task<IEnumerable<AnimalDto>> GetList()
        //{
        //}

        //[HttpGet]
        //[Route("{visitId}")]
        //public async Task<ActionResult<AnimalDto>> Get(int animalId, int visitId)
        //{
        //    // topic exists + post exists
        //    // else NotFound()
        //}

        //[HttpPost]
        //public async Task<ActionResult<AnimalDto>> Create(AnimalDto newAnimal)
        //{
        //}

        //[HttpPut]
        //[Route("{animalId}")]
        //public async Task<ActionResult<AnimalDto>> Update(int animalId, AnimalDto animalDto)
        //{
        //}

        //[HttpDelete]
        //[Route("{animalId}")]
        //public async Task<ActionResult> Remove(int animalId)
        //{
        //}
    }
}
