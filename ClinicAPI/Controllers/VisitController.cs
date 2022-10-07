using ClinicAPI.Data.Dtos;
using ClinicAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ClinicAPI.Data.Repositories;
using ClinicAPI.Data;
using System.Text.Json;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/animals/{animalId}/visits")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitRepository _repository;
        private readonly IAnimalRepository _animalRepository;

        public VisitController(IVisitRepository repository, IAnimalRepository animalRepository)
        {
            _repository = repository;
            _animalRepository = animalRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<VisitDto>?> GetList(int animalId)
        {
            //var animal = await _animalRepository.GetAsync(animalId);
            //if(animal == null)
            //{
            //    return NotFound($"There are no animal by this id:{animalId}") as IEnumerable<VisitDto>;
            //}
            var visits = await _repository.GetListAsync(animalId);
            //if (visits == null)
            //{
            //    return NotFound($"there are no visits") as IEnumerable<VisitDto>;
            //}
            return visits.Select(v => new VisitDto(v.Id, v.Description, v.isFinished, v.AnimalId));
        }

        [HttpGet]
        [Route("{visitId}")]
        public async Task<ActionResult<VisitDto>> Get(int animalId, int visitId)
        {
            var visit = await _repository.GetAsync(animalId, visitId);
            if(visit == null)
            {
                return NotFound($"There are no animal by this id:{animalId} or there are no visit id:{visitId}");
            }
            return Ok(new VisitDto(visit.Id, visit.Description, visit.isFinished, visit.AnimalId));
        }

        [HttpPost]
        public async Task<ActionResult<VisitDto>> Create(int animalId, VisitDto newVisit)
        {
            var animal = await _animalRepository.GetAsync(animalId);
            if(animal == null)
            {
                return NotFound($"Selected animalId:{animalId} not found");
            }
            var visit = new Visit
            {
                Description = newVisit.description,
                CreatedDate = DateTime.Now,
                isFinished = false,
                AnimalId = animalId
            };
            await _repository.CreateAsync(visit);
            return Created($"/api/animals/{animalId}/visits/{visit.Id}", visit);
        }

        [HttpPut]
        [Route("{visitId}")]
        public async Task<ActionResult<VisitDto>> Update(int animalId, int visitId, VisitDto newVisit)
        {
            if (newVisit.description == null)
                return BadRequest();
            var animal = await _animalRepository.GetAsync(animalId);
            if (animal == null)
            {
                return NotFound($"Selected animalId:{animalId} was not found");
            }

            var oldVisit = await _repository.GetAsync(animalId, visitId);
            if(oldVisit == null)
            {
                return NotFound();
            }
            oldVisit.Description = newVisit.description;
            await _repository.UpdateAsync(oldVisit);
            return Ok(oldVisit);

        }

        [HttpDelete]
        [Route("{visitId}")]
        public async Task<ActionResult> Remove(int animalId, int visitId)
        {
            var post = await _repository.GetAsync(animalId, visitId);
            if(post == null)
            {
                return NotFound();
            }
            await _repository.RemoveAsync(post);
            return NoContent();
        }
    }
}
