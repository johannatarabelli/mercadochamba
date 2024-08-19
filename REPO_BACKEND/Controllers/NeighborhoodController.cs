using backnc.Common.DTOs.CountryDTOs;
using backnc.Common.DTOs.NeighborhoodDTO;
using backnc.Data.Context;
using backnc.Interfaces;
using backnc.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backnc.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NeighborhoodController : ControllerBase
	{
		private readonly INeighborhoodService neighborhoodService;
		private readonly AppDbContext _context;

        public NeighborhoodController(INeighborhoodService neighborhoodService, AppDbContext context)
        {
			this.neighborhoodService = neighborhoodService;
			this._context = context;
        }

		//[HttpGet("{provinceId}")]
		//public IActionResult GetNeighborhoods(int provinceId)
		//{
		//	var neighborhoods = _context.Neighborhoods
		//								.Where(n => n.ProvinceId == provinceId)
		//								.Select(n => new CreateNeighborhoodDTO
		//								{
		//									Name = n.Name,
		//									ProvinceId = n.ProvinceId
		//								}).ToList();
		//	return Ok(neighborhoods);
		//}

		[HttpGet]
		public async Task<IActionResult> GetAllNeighborhoods()
		{
			var countries = await neighborhoodService.GetAllNeighborhoods();
			return Ok(countries);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetNeighborhoodById(int id)
		{
			var country = await neighborhoodService.GetNeighborhoodById(id);
			if (country == null)
			{
				return NotFound();
			}
			return Ok(country);
		}

		[HttpPost]
		public async Task<IActionResult> AddNeighborhood([FromBody] CreateNeighborhoodDTO createNeighborhoodDTO)
		{
			var newCountry = await neighborhoodService.AddNeighborhood(createNeighborhoodDTO);
			
			return Ok(newCountry);
		}
	}
}
