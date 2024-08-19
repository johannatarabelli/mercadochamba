using backnc.Common.DTOs.CountryDTOs;
using backnc.Data.POCOEntities;
using backnc.Interfaces;
using backnc.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backnc.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : ControllerBase
	{
		private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
			this.countryService = countryService;
        }
		[HttpGet]
		public async Task<IActionResult> GetAllCountries()
		{
			var countries = await countryService.GetAllCountries();
			return Ok(countries);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCountryById(int id)
		{
			var country = await countryService.GetCountryById(id);
			if (country == null)
			{
				return NotFound();
			}
			return Ok(country);
		}

		[HttpPost]
		public async Task<IActionResult> AddCountry([FromBody] CreateCountryDTO createCountryDTO)
		{
			var newCountry = await countryService.AddCountry(createCountryDTO);
			return CreatedAtAction(nameof(GetCountryById), new { id = newCountry.Id }, newCountry);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCountry(int id, [FromBody] EditCountryDTOs editCountryDTOs)
		{
			var updatedCountry = await countryService.UpdateCountry(id, editCountryDTOs);
			if (updatedCountry == null)
			{
				return NotFound();
			}
			return Ok(updatedCountry);
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCountry(int id)
		{
			var result = await countryService.DeleteCountry(id);
			if (!result)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
