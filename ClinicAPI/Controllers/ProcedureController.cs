using ClinicAPI.Data.Dtos;
using ClinicAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ClinicAPI.Data.Repositories;
using ClinicAPI.Data;
using System.Text.Json;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/animals/{animalId}/visits/{visitId}/procedures")]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureRepository _repository;
        private readonly IVisitRepository _visitRepository;
        private readonly IAnimalRepository _animalRepository;

        public ProcedureController(IProcedureRepository repository, IVisitRepository visitRepository, IAnimalRepository animalRepository)
        {
            _repository = repository;
            _visitRepository = visitRepository;
            _animalRepository = animalRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProcedureDto>?> GetList(int animalId, int visitId)
        {
            var procedures = await _repository.GetListAsync(animalId, visitId);
            return procedures.Select(p => new ProcedureDto(p.Id, p.Name, p.Description, p.Cost, p.visitId, p.animalId));
        }

        [HttpGet]
        [Route("{procedureId}")]
        public async Task<ActionResult<ProcedureDto>> Get(int animalId, int visitId, int procedureId)
        {
            var visit = await _visitRepository.GetAsync(animalId, visitId);
            if (visit == null)
            {
                return NotFound($"There are no animal by this id:{animalId} or there are no visit id:{visitId}");
            }
            var procedure = await _repository.GetAsync(animalId, visitId, procedureId);
            if(procedure == null)
            {
                return NotFound($"Procedure was not found by id:{procedureId}");
            }

            return Ok(new ProcedureDto(procedure.Id, procedure.Name, procedure.Description,
                procedure.Cost, procedure.visitId, procedure.animalId));
        }

        [HttpPost]
        public async Task<ActionResult<ProcedureDto>> Create(int animalId, int visitId, ProcedureDto newProcedure)
        {
            var animal = await _animalRepository.GetAsync(animalId);
            if (animal == null)
            {
                return NotFound($"Selected animalId:{animalId} not found");
            }
            var visit = await _visitRepository.GetAsync(animalId, visitId);
            if (visit == null)
            {
                return NotFound($"Selected animalId:{animalId} or visit id: {visitId} was not found");
            }
            var procedure = new Procedure {Id = newProcedure.id, Name = newProcedure.name,
                Description = newProcedure.description, Cost = newProcedure.cost,
                visitId = visitId, animalId = animalId };
            await _repository.CreateAsync(procedure);
            return Created($"/api/animals/{animalId}/visits/{visit.Id}/procedures/{procedure.Id}", procedure);
        }

        [HttpPut]
        [Route("{procedureId}")]
        public async Task<ActionResult<ProcedureDto>> Update(int animalId, int visitId, int procedureId, Procedure newProcedure)
        {
            if( newProcedure.Name == null && newProcedure.Description == null && newProcedure.Cost == null)
                return BadRequest();
            var animal = await _animalRepository.GetAsync(animalId);
            if (animal == null)
            {
                return NotFound();
            }

            var visit = await _visitRepository.GetAsync(animalId, visitId);
            if (visit == null)
            {
                return NotFound();
            }
            var oldProcedure = await _repository.GetAsync(animalId, visitId, procedureId);
            oldProcedure.Name = newProcedure.Name;
            oldProcedure.Description = newProcedure.Description;
            oldProcedure.Cost = newProcedure.Cost;
            await _repository.UpdateAsync(oldProcedure);
            return Ok(oldProcedure);
        }

        [HttpDelete]
        [Route("{procedureId}")]
        public async Task<ActionResult> Remove(int animalId, int visitId, int procedureId)
        {
            var post = await _repository.GetAsync(animalId, visitId, procedureId);
            if (post == null)
            {
                return NotFound();
            }
            await _repository.RemoveAsync(post);
            return NoContent();
        }
    }
}
