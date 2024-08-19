using backnc.Common.DTOs.CountryDTOs;
using backnc.Common.DTOs.ProvinceDTO;
using backnc.Interfaces;
using backnc.Service;
using Microsoft.AspNetCore.Mvc;

namespace backnc.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProvinceController : ControllerBase
	{
		private readonly IProvinceSerivce provinceService;

		public ProvinceController(IProvinceSerivce provinceService)
		{
			this.provinceService = provinceService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProvinces()
		{
			var provinces = await provinceService.GetAllProvinces();
			return Ok(provinces);
		}

		[HttpGet("byCountry/{CountryId}")]
		public async Task<IActionResult> GetProvincesByCountryId(int countryId)
		{
			var provinces = await provinceService.GetProvincesByCountryId(countryId);
			return Ok(provinces);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProvinceById(int id)
		{
			var province = await provinceService.GetProvinceById(id);
			if (province == null)
			{
				return NotFound();
			}
			return Ok(province);
		}

		[HttpPost]
		public async Task<IActionResult> AddProvince([FromBody] CreateProvinceDTO createProvinceDTO)
		{
			var newProvince = await provinceService.AddProvince(createProvinceDTO);
			return CreatedAtAction(nameof(GetProvinceById), new { id = newProvince.Id }, newProvince);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProvince(int id, [FromBody] EditProvinceDTO editProvinceDTO)
		{
			var updatedProvince = await provinceService.UpdateProvince(id, editProvinceDTO);
			if (updatedProvince == null)
			{
				return NotFound();
			}
			return Ok(updatedProvince);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProvince(int id)
		{
			var result = await provinceService.DeleteProvince(id);
			if (!result)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
