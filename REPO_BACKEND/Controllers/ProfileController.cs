using AutoMapper;
using backnc.Common.DTOs.CategoryDTO;
using backnc.Common.DTOs.ProfileDTO;
using backnc.Common.Response;
using backnc.Data.Context;
using backnc.Data.POCOEntities;
using backnc.Interfaces;
using backnc.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CategoryDTO = backnc.Common.DTOs.ProfileDTO.CategoryDTO;

namespace backnc.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProfileController : ControllerBase
	{
		private readonly ProfileService _profileService;
		private readonly AppDbContext _context;

		public ProfileController(ProfileService profileService, AppDbContext context)
		{
			_profileService = profileService;
			_context = context;
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> GetProfileByUser(int userId)
		{
			try
			{
				var profile = await _profileService.GetProfileByUser(userId);
				if (profile == null)
				{
					return NotFound(new BaseResponse("Perfil no encontrado."));
				}
				return Ok(new BaseResponse(profile));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new BaseResponse("Error al obtener el perfil.", ex.Message, true));
			}
		}


		[Authorize]
		[HttpPut]
		public async Task<IActionResult> UpdateProfile([FromForm] CreateProfileDTO createProfileDTO)
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var profile = await _profileService.GetProfileByUser(userId);

			if (profile == null)
			{
				return NotFound(new BaseResponse("Perfil no encontrado."));
			}

			profile.Specialty = createProfileDTO.Specialty;
			profile.Experience = createProfileDTO.Experience;
			profile.Description = createProfileDTO.Description;

			// Actualizar las categorías del perfil
			var currentCategoryIds = profile.ProfileCategories.Select(pc => pc.CategoryId).ToList();
			var newCategoryIds = createProfileDTO.CategoryIds ?? new List<int>();

			// Eliminar categorías que ya no están en la nueva lista
			var categoriesToRemove = profile.ProfileCategories.Where(pc => !newCategoryIds.Contains(pc.CategoryId)).ToList();
			foreach (var categoryToRemove in categoriesToRemove)
			{
				profile.ProfileCategories.Remove(categoryToRemove);
			}

			// Añadir nuevas categorías que no están en las actuales
			var categoriesToAdd = newCategoryIds.Where(id => !currentCategoryIds.Contains(id)).ToList();
			foreach (var categoryId in categoriesToAdd)
			{
				var category = await _context.Categories.FindAsync(categoryId);
				if (category != null)
				{
					var profileCategory = new ProfileCategory
					{
						ProfileId = profile.Id,
						CategoryId = category.Id
					};
					profile.ProfileCategories.Add(profileCategory);
				}
			}

			// Actualizar la imagen del perfil si se proporciona una nueva imagen
			if (createProfileDTO.Image != null)
			{
				try
				{
					var imageUrl = await _profileService.SaveImageAsync(createProfileDTO.Image);
					profile.ImageUrl = imageUrl;
				}
				catch (Exception ex)
				{
					return StatusCode(500, new BaseResponse("Error al guardar la imagen.", ex.Message, true));
				}
			}

			try
			{
				await _profileService.UpdateProfile(profile);
				return Ok(new BaseResponse("Perfil actualizado correctamente."));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new BaseResponse("Error al actualizar el perfil.", ex.Message, true));
			}
		}


		//[HttpGet("category/{categoryId}")]
		//public async Task<IActionResult> GetProfilesByCategory(int categoryId)
		//{
		//	var profiles = await _profileService.GetProfilesByCategory(categoryId);
		//	if (profiles == null || !profiles.Any())
		//	{
		//		return NotFound(new BaseResponse("No se encontraron perfiles para esta categoría."));
		//	}


		//	var profileDtos = profiles.Select(p => p.ToDto()).ToList();
		//	return Ok(new BaseResponse( profileDtos));
		//}

		[HttpGet("category/{categoryId}")]
		public async Task<IActionResult> GetProfilesByCategory(int categoryId)
		{
			var profiles = await _profileService.GetProfilesByCategory(categoryId);
			if (profiles == null || !profiles.Any())
			{
				return NotFound(new BaseResponse("No se encontraron perfiles para esta categoría."));
			}

			var profileDtos = profiles.Select(profile => new ProfileDTO
			{
				Id = profile.Id,
				UserId = profile.UserId,
				UserName = profile.User.UserName,  // Mapear el nombre del usuario				 
				phoneNumber = profile.User.phoneNumber,
				Specialty = profile.Specialty,
				Experience = profile.Experience,
				Description = profile.Description,
				ImageUrl = profile.ImageUrl,
				Categories = profile.ProfileCategories.Select(pc => new CategoryDTO
				{
					Id = pc.CategoryId,
					Name = pc.Category.Name
				}).ToList()
			}).ToList();

			return Ok(new BaseResponse("Perfiles encontrados ", profileDtos));
		}

		[HttpGet("CurrentUserProfile")]
		[Authorize]
		public async Task<IActionResult> GetCurrentUserProfile()
		{
			try
			{
				var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
				var profile = await _profileService.GetProfileByUser(userId);
				if (profile == null)
				{
					return NotFound(new BaseResponse("Perfil no encontrado."));
				}
				var profileDto = profile.ToDto();
				return Ok(new BaseResponse(profileDto));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new BaseResponse("Error al obtener el perfil.", ex.Message, true));
			}
		}

		[HttpGet("ByUserId/{userId}")]
		public async Task<IActionResult> GetProfileByUserId(int userId)
		{
			try
			{
				var profile = await _profileService.GetProfileByUser(userId);
				if (profile == null)
				{
					return NotFound(new BaseResponse("Perfil no encontrado."));
				}

				var profileDto = new ProfileDTO
				{
					Id = profile.Id,
					UserId = profile.UserId,
					UserName = profile.User.UserName,					
					phoneNumber = profile.User.phoneNumber,
					Specialty = profile.Specialty,
					Experience = profile.Experience,
					Description = profile.Description,
					ImageUrl = profile.ImageUrl,
					Categories = profile.ProfileCategories.Select(pc => new CategoryDTO
					{
						Id = pc.CategoryId,
						Name = pc.Category.Name
					}).ToList()
				};

				return Ok(new BaseResponse("Encontrado con éxito",profileDto));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new BaseResponse("Error al obtener el perfil.", ex.Message, true));
			}
		}
		

		[HttpGet("AllProfiles")]
		public async Task<IActionResult> GetAllProfiles()
		{
			try
			{
				var profiles = await _profileService.GetAllProfiles();
				var profileDtos = profiles.Select(profile => new ProfileDTO
				{
					Id = profile.Id,
					UserId = profile.UserId,
					UserName = profile.User.UserName,  			
					phoneNumber = profile.User.phoneNumber,
					Specialty = profile.Specialty,
					Experience = profile.Experience,
					Description = profile.Description,
					ImageUrl = profile.ImageUrl,
					Categories = profile.ProfileCategories.Select(pc => new CategoryDTO
					{
						Id = pc.CategoryId,
						Name = pc.Category.Name
					}).ToList()
				}).ToList();

				return Ok(new BaseResponse("perfiles encontrados",profileDtos));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new BaseResponse("Error al obtener todos los perfiles.", ex.Message, true));
			}			
		}
	}
}
