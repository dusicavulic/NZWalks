using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[ApiController]
	//[Route("Walks")]
	[Route("[controller]")]
	public class WalksController : Controller
	{
		private readonly IWalkRepository walkRepository;
		private readonly IMapper mapper;
		public WalksController(IWalkRepository walkRepository, IMapper mapper)
		{
			this.walkRepository = walkRepository;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllWalksAsync()
		{
			var walks = await walkRepository.GetAllAsync();

			//return DTO walks
			var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);


			return Ok(walksDTO);
		}

		[HttpGet]
		[Route("{id:guid}")]
		[ActionName("GetWalksAsync")]
		public async Task<IActionResult> GetWalksAsync(Guid id)
		{
			var walkDomain = await walkRepository.GetAsync(id);
			var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

			return Ok(walkDTO);
		}

		[HttpPost]
		public async Task<IActionResult> AddWalkAsync([FromBody]Models.DTO.AddWalkRequest addWalkRequest)
		{
			var walkDomain = new Models.Domain.Walk
			{
				Length = addWalkRequest.Length,
				Name = addWalkRequest.Name,
				RegionId = addWalkRequest.RegionId,
				WalkDifficultyId = addWalkRequest.WalkDifficultyId
			};

			walkDomain = await walkRepository.AddAsync(walkDomain);
			var walkDTO = new Models.DTO.Walk
			{
				Id = walkDomain.Id,
				Length = walkDomain.Length,
				Name = walkDomain.Name,
				RegionId = walkDomain.RegionId,
				WalkDifficultyId = walkDomain.WalkDifficultyId
			};

			return CreatedAtAction(nameof(GetWalksAsync), new { id = walkDTO.Id}, walkDTO);


		}
		[HttpPut]
		[Route("{id:guid}")]
		public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
		{


			var walkDomain = new Models.Domain.Walk
			{
				Length = updateWalkRequest.Length,
				Name = updateWalkRequest.Name,
				RegionId = updateWalkRequest.RegionId,
				WalkDifficultyId = updateWalkRequest.WalkDifficultyId
			};

			walkDomain = await walkRepository.UpdateAsync(id, walkDomain);
			if (walkDomain == null)
			{
				return NotFound();
			}

			var walkDTO = new Models.DTO.Walk {
				Id= walkDomain.Id,
				Length = walkDomain.Length,
				Name = walkDomain.Name,
				RegionId = walkDomain.RegionId,
				WalkDifficultyId = walkDomain.WalkDifficultyId

			};
			return Ok(walkDTO);
		}

		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> DeleteWalkAsync([FromRoute] Guid id)
		{ 
		var walkDomain = await walkRepository.DeleteAsync(id);
			if (walkDomain == null)
			{
				return NotFound();
			}

			var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

			return Ok(walkDTO);
		}
		

		}
}
