using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Runtime;
using System.Runtime.Intrinsics.Arm;

namespace NZWalks.API.Controllers
{
	[ApiController]
	//[Route("Regions")]
	[Route("[controller]")]

	public class RegionsController : Controller
	{
		private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;
		public RegionsController(IRegionRepository regionRepository, IMapper mapper)
		{
			this.regionRepository = regionRepository;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllRegionsAsync()
		{
			var regions = await regionRepository.GetAllAsync();

			//return DTO regions
			var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);


			return Ok(regionsDTO);
		}

		[HttpGet]
		[Route("{id:guid}")]
		[ActionName("GetRegionAsync")]
		public async Task<IActionResult> GetRegionAsync(Guid id)
		{
			var region = await regionRepository.GetAsync(id);

			if (region == null)
			{
				return NotFound();
			}
			//return DTO regions
			var regionsDTO = mapper.Map<Models.DTO.Region>(region);


			return Ok(regionsDTO);
		}


		[HttpPost]
		public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
		{
			var region = new Models.Domain.Region()
			{
				Code = addRegionRequest.Code,
				Area = addRegionRequest.Area,
				Lat = addRegionRequest.Lat,
				Long = addRegionRequest.Long,
				Name = addRegionRequest.Name,
				Population = addRegionRequest.Population


			};
			region = await regionRepository.AddRegionAsync(region);

			var regionDTO = new Models.DTO.Region()
			{
				Id = region.Id,
				Code = region.Code,
				Area = region.Area,
				Lat = region.Lat,
				Long = region.Long,
				Name = region.Name,
				Population = region.Population
			};
			return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);

		}

		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> DeleteRegionAsync(Guid id)
		{
			var region = await regionRepository.DeleteRegionAsync(id);
			if (region == null)
			{
				return NotFound();
			}
			var regionDTO = new Models.DTO.Region()
			{
				Id = region.Id,
				Code = region.Code,
				Area = region.Area,
				Lat = region.Lat,
				Long = region.Long,
				Name = region.Name,
				Population = region.Population
			};
			return Ok(regionDTO);
		}

		[HttpPut]
		[Route("{id:guid}")]
		public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id, [FromBody]UpdateRegionRequest updateRegionRequest)
		{
			var region = new Models.Domain.Region()
			{
				Code = updateRegionRequest.Code,
				Area = updateRegionRequest.Area,
				Lat = updateRegionRequest.Lat,
				Long = updateRegionRequest.Long,
				Name = updateRegionRequest.Name,
				Population = updateRegionRequest.Population
			};

			region = await regionRepository.UpdateRegionAsync(id, region);

			if (region == null)
			{
				return NotFound();
			}

			var regionDTO = new Models.Domain.Region()
			{
				Id = region.Id,
				Code = region.Code,
				Area = region.Area,
				Lat = region.Lat,
				Long = region.Long,
				Name = region.Name,
				Population = region.Population
			};

			return Ok(regionDTO);
		}
	}	
}