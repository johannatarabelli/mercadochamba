using backnc.Common.DTOs;
using backnc.Common.DTOs.NeighborhoodDTO;
using backnc.Common.DTOs.ProvinceDTO;
using backnc.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backnc.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LocationsController : ControllerBase
	{

		private readonly IAppDbContext context;

        public LocationsController( IAppDbContext context)
        {
			this.context = context;
        }

		[HttpGet("countries")]
		public IActionResult GetCountries()
		{
			var countries = context.Countries.Select(c => new CountryDTO
			{
				id = c.Id,
				name = c.Name,
				Provinces = c.Provinces.ToList()
			}).ToList();
			return Ok(countries);
		}

		[HttpGet("countries/{countryId}/provinces")]
		public IActionResult GetProvinces(int countryId)
		{
			var provinces = context.Provinces
									.Where(p => p.CountryId == countryId)
									.Select(p => new CreateProvinceDTO
									{
										Name = p.Name,
										CountryId = p.CountryId
									}).ToList();
			return Ok(provinces);
		}

		[HttpGet("provinces/{provinceId}/neighborhoods")]
		public IActionResult GetNeighborhoods(int provinceId)
		{
			var neighborhoods = context.Neighborhoods
										.Where(n => n.ProvinceId == provinceId)
										.Select(n => new CreateNeighborhoodDTO
										{
											Name = n.Name,
											ProvinceId = n.ProvinceId
										}).ToList();
			return Ok(neighborhoods);
		}

	}
}
