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
    [Route("api/animals/{animalId}/visits")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitRepository _repository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IProcedureRepository _procedureRepository;
        private readonly IAuthorizationService _authorizationService;

        public VisitController(IVisitRepository repository, IAnimalRepository animalRepository, IProcedureRepository procedureRepository,
            IAuthorizationService authService)
        {
            _repository = repository;
            _animalRepository = animalRepository;
            _procedureRepository = procedureRepository;
            _authorizationService = authService;
        }

        [HttpGet]
        [Authorize(Roles = ClinicRoles.User + "," + ClinicRoles.Employee)]
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
        [Authorize(Roles = ClinicRoles.User + "," + ClinicRoles.Employee)]
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
        [Authorize(Roles = ClinicRoles.Employee)]
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
                AnimalId = animalId,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };
            await _repository.CreateAsync(visit);
            return Created($"/api/animals/{animalId}/visits/{visit.Id}", visit);
        }

        [HttpPut]
        [Route("{visitId}")]
        [Authorize(Roles = ClinicRoles.Employee)]
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
            var test = User;
            //var authResult = await _authorizationService.AuthorizeAsync(User, animal, PolicyNames.ResourceOwner);
            var authResult2 = await _authorizationService.AuthorizeAsync(User, oldVisit, PolicyNames.ResourceOwner);
            if (!authResult2.Succeeded)
            {
                return Forbid();
            }

            oldVisit.Description = newVisit.description;
            await _repository.UpdateAsync(oldVisit);
            return Ok(oldVisit);

        }

        [HttpDelete]
        [Route("{visitId}")]
        [Authorize(Roles = ClinicRoles.Employee)]
        public async Task<ActionResult> Remove(int animalId, int visitId)
        {
            var visit = await _repository.GetAsync(animalId, visitId);
            if(visit == null)
            {
                return NotFound();
            }
            var authResult = await _authorizationService.AuthorizeAsync(User, visit, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            await _repository.RemoveAsync(visit);
            return NoContent();
        }
    }
}
