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
    [Route("api/animals")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _repository;
        private readonly IVisitRepository _visitRepository;
        private readonly IProcedureRepository _procedureRepository;
        private readonly IAuthorizationService _authorizationService;

        public AnimalController(IAnimalRepository repository, IVisitRepository visitRepository, IProcedureRepository procedureRepository,
            IAuthorizationService authService)
        {
            _repository = repository;
            _visitRepository = visitRepository;
            _procedureRepository = procedureRepository;
            _authorizationService = authService;
        }

        // /animal?pageNumber=1&pageSize=2
        [HttpGet(Name = "GetAnimals ")]
        public async Task<IEnumerable<AnimalDto>> GetListPaging([FromQuery] AnimalSearchParameters searchParameters)
        {
            var animals = await _repository.GetListAsync(searchParameters);
            var previousPageLink = animals.HasPrevious ? CreateAnimalsResourceUri(
                searchParameters, ResourceUriType.PreviousPage) : null;
            var nextPageLink = animals.HasNext ? CreateAnimalsResourceUri(
                searchParameters, ResourceUriType.NextPage) : null;

            var paginationData = new
            {
                totalCount = animals.TotalCount,
                pageSize = animals.PageSize,
                currentPage = animals.CurrentPage,
                totalPages = animals.TotalPages,
                previousPageLink,
                nextPageLink,
            };
            Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationData));
            return animals.Select(a => new AnimalDto(a.Id, a.Name, a.Type, a.Breed, a.Age));
        }

        [HttpGet]
        [Route("{animalId}")]
        public async Task<ActionResult<AnimalDto>> Get(int animalId)
        {
            var animal = await _repository.GetAsync(animalId);
            if (animal == null) 
            {
                return NotFound();
            }
            return new AnimalDto(animal.Id, animal.Name, animal.Type, animal.Breed, animal.Age);
        }

        [HttpPost]
        [Authorize(Roles = ClinicRoles.User)]
        public async Task<ActionResult<AnimalDto>> Create(AnimalDto newAnimal)
        {
             var animal = new Animal { Name = newAnimal.name, Type = newAnimal.type,
                 Breed = newAnimal.breed, Age = newAnimal.age, UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)};
            await _repository.CreateAsync(animal);

            return Created("", new AnimalDto(animal.Id, animal.Name, animal.Type, animal.Breed, animal.Age));
            //return CreatedAtAction("GetAnimal", new {animalId = animal.Id}, 
            //    new AnimalDto(animal.Name, animal.Type, animal.Breed, animal.Age, animal.OwnerName));
        }

        [HttpPut]
        [Route("{animalId}")]
        [Authorize(Roles = ClinicRoles.User)]
        public async Task<ActionResult<AnimalDto>> Update(int animalId, AnimalDto animalDto)
        {
            var animal = await _repository.GetAsync(animalId);
            if(animal == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, animal, PolicyNames.ResourceOwner);
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            animal.Name = animalDto.name;
            animal.Age = animalDto.age;
            animal.Breed = animalDto.breed;
            animal.Type = animalDto.type;

            await _repository.UpdateAsync(animal);

            return Ok(new AnimalDto(animal.Id, animal.Name, animal.Type, animal.Breed, animal.Age));
        }

        [HttpDelete]
        [Route("{animalId}")]
        public async Task<ActionResult> Remove(int animalId)
        {
            var animal = await _repository.GetAsync(animalId);

            if(animal == null)
            {
                return NotFound();
            }
            await _repository.RemoveAsync(animal);

            return NoContent();
        }

        /////////////////////
        
        private string? CreateAnimalsResourceUri(AnimalSearchParameters animalSearchParameters, ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetAnimals", new
                {
                    pageNumber = animalSearchParameters.pageNumber - 1,
                    pageSize = animalSearchParameters.pageSize,
                }),
                ResourceUriType.NextPage => Url.Link("GetAnimals", new
                {
                    pageNumber = animalSearchParameters.pageNumber + 1,
                    pageSize = animalSearchParameters.pageSize,
                }),
                _ => Url.Link("GetAnimals", new
                {
                    pageNumber = animalSearchParameters.pageNumber,
                    pageSize = animalSearchParameters.pageSize
                })
            };
        }

    }
}
