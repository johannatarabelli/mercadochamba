using AutoMapper;
using backnc.Common.DTOs.ProfileDTO;

namespace backnc.Interfaces
{
	public interface IProfileService
	{
		Task<Profile> GetProfileByUserAsync(int userId);
		Task<CreateProfileDTO> CreateProfileAsync(CreateProfileDTO createProfileDTO);
		Task<CreateProfileDTO> UpdateProfileAsync(CreateProfileDTO createProfileDTO);
		Task<string> SaveImageAsync(IFormFile image);
	}
}
