using ClinicAPI.Data.Dtos;
using ClinicAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ClinicAPI.Data.Repositories;
using ClinicAPI.Data;
using System.Text.Json;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _repository;

        public AnimalController(IAnimalRepository repository)
        {
            _repository = repository;
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
            return animals.Select(a => new AnimalDto(a.Id, a.Name, a.Type, a.Breed, a.Age, a.OwnerName));
        }

        //[HttpGet]
        public async Task<IEnumerable<AnimalDto>> GetList()
        {
            var animals = await _repository.GetListAsync();
            return animals.Select(a => new AnimalDto(a.Id, a.Name, a.Type, a.Breed, a.Age, a.OwnerName));
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
            return new AnimalDto(animal.Id, animal.Name, animal.Type, animal.Breed, animal.Age, animal.OwnerName);
        }

        [HttpPost]
        public async Task<ActionResult<AnimalDto>> Create(AnimalDto newAnimal)
        {
             var animal = new Animal { Name = newAnimal.name, Type = newAnimal.type,
                 Breed = newAnimal.breed, Age = newAnimal.age, OwnerName = newAnimal.owner };
            await _repository.CreateAsync(animal);

            return Created("", new AnimalDto(animal.Id, animal.Name, animal.Type, animal.Breed, animal.Age, animal.OwnerName));
            //return CreatedAtAction("GetAnimal", new {animalId = animal.Id}, 
            //    new AnimalDto(animal.Name, animal.Type, animal.Breed, animal.Age, animal.OwnerName));
        }

        [HttpPut]
        [Route("{animalId}")]
        public async Task<ActionResult<AnimalDto>> Update(int animalId, AnimalDto animalDto)
        {
            var animal = await _repository.GetAsync(animalId);
            if(animal == null)
            {
                return NotFound();
            }
            animal.Name = animalDto.name;
            animal.Age = animalDto.age;
            animal.Breed = animalDto.breed;
            animal.Type = animalDto.type;
            animal.OwnerName = animalDto.owner;

            await _repository.UpdateAsync(animal);

            return Ok(new AnimalDto(animal.Id, animal.Name, animal.Type, animal.Breed, animal.Age, animal.OwnerName));
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
