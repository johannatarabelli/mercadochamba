using backnc.Common;
using backnc.Common.DTOs.AdministradorDTO;
using backnc.Data.POCOEntities;
using backnc.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backnc.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdministradorController : ControllerBase
	{
		private readonly AdministradorService _administradorService;
        public AdministradorController(AdministradorService administradorService)
        {
			this._administradorService = administradorService;         
        }

		[HttpGet]
		public async Task<IActionResult> GetAllAdmins()
		{
			var clientes = await _administradorService.GetAllAdminsAsync();
			return Ok(clientes);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAdminById(int id)
		{
			var cliente = await _administradorService.GetAdminByIdAsync(id);
			if (cliente == null)
			{
				return NotFound("Administrador no encontrado");
			}
			return Ok(cliente);
		}

		

		[HttpPost]
		public async Task<IActionResult> CreateAdmin(CreateAdministradorDTO adminDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var hashedPassword = PasswordHasher.HashPassword(adminDto.Password);

			var user = new User
			{
				UserName = adminDto.Username,
				email = adminDto.Email,
				Password = hashedPassword
			};

			await _administradorService.CreateAdminAsync(user);
			return CreatedAtAction(nameof(GetAdminById), new { id = user.Id }, user);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAdmin(int id, CreateAdministradorDTO adminDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingUser = await _administradorService.GetAdminByIdAsync(id);
			if (existingUser == null)
			{
				return NotFound("Administrador no encontrado");
			}

			existingUser.UserName = adminDto.Username;
			existingUser.email = adminDto.Email;
			existingUser.Password = PasswordHasher.HashPassword(adminDto.Password);

			await _administradorService.UpdateAdminAsync(existingUser);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAdmin(int id)
		{
			var cliente = await _administradorService.GetAdminByIdAsync(id);
			if (cliente == null)
			{
				return NotFound("Administrador no encontrado");
			}

			await _administradorService.DeleteAdminAsync(id);
			return NoContent();
		}
	}
}
