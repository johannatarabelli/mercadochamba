using backnc.Data.POCOEntities;

namespace backnc.Common.DTOs.ProfileDTO
{
	public static class ProfileExtensions
	{
		public static ProfileDTO ToDto(this Profile profile)
		{
			return new ProfileDTO
			{
				Id = profile.Id,
				UserId = profile.UserId,
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
		}
	}
}
