using ClinicAPI.Data.Dtos;
using ClinicAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ClinicAPI.Data.Repositories;
using ClinicAPI.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using ClinicAPI.Auth.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/animals/{animalId}/visits/{visitId}/procedures")]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureRepository _repository;
        private readonly IVisitRepository _visitRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IAuthorizationService _authorizationService;

        public ProcedureController(IProcedureRepository repository, IVisitRepository visitRepository, IAnimalRepository animalRepository,
            IAuthorizationService authService)
        {
            _repository = repository;
            _visitRepository = visitRepository;
            _animalRepository = animalRepository;
            _authorizationService = authService;
        }

        [HttpGet]
        [Authorize(Roles = ClinicRoles.User + "," + ClinicRoles.Employee)]
        public async Task<IEnumerable<ProcedureDto>?> GetList(int animalId, int visitId)
        {
            var procedures = await _repository.GetListAsync(animalId, visitId);
            return procedures.Select(p => new ProcedureDto(p.Id, p.Name, p.Description, p.Cost, p.visitId, p.animalId));
        }

        [HttpGet]
        [Route("{procedureId}")]
        [Authorize(Roles = ClinicRoles.User + "," + ClinicRoles.Employee)]
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
        [Authorize(Roles = ClinicRoles.Employee)]
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
            var procedure = new Procedure {
                Id = newProcedure.id,
                Name = newProcedure.name,
                Description = newProcedure.description, Cost = newProcedure.cost,
                visitId = visitId, animalId = animalId,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };
            await _repository.CreateAsync(procedure);
            return Created($"/api/animals/{animalId}/visits/{visit.Id}/procedures/{procedure.Id}", procedure);
        }

        [HttpPut]
        [Route("{procedureId}")]
        [Authorize(Roles = ClinicRoles.Employee)]
        public async Task<ActionResult<ProcedureDto>> Update(int animalId, int visitId, int procedureId, ProcedureDto newProcedure)
        {
            if( newProcedure.name == null && newProcedure.description == null && newProcedure.cost == null)
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

            var authResult2 = await _authorizationService.AuthorizeAsync(User, oldProcedure, PolicyNames.ResourceOwner);
            if (!authResult2.Succeeded)
            {
                return Forbid();
            }

            oldProcedure.Name = newProcedure.name;
            oldProcedure.Description = newProcedure.description;
            oldProcedure.Cost = newProcedure.cost;
            await _repository.UpdateAsync(oldProcedure);
            return Ok(oldProcedure);
        }

        [HttpDelete]
        [Route("{procedureId}")]
        [Authorize(Roles = ClinicRoles.Employee)]
        public async Task<ActionResult> Remove(int animalId, int visitId, int procedureId)
        {
            var procedure = await _repository.GetAsync(animalId, visitId, procedureId);
            if (procedure == null)
            {
                return NotFound();
            }
            var authResult = await _authorizationService.AuthorizeAsync(User, procedure, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _repository.RemoveAsync(procedure);
            return NoContent();
        }
    }
}
