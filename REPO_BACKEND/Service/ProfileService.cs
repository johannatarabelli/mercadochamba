using backnc.Common.DTOs.ProfileDTO;
using backnc.Data.Interface;
using backnc.Data.POCOEntities;
using backnc.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backnc.Service
{
	public class ProfileService 
	{
		private readonly IAppDbContext context;
		private readonly string rutaServidor;
		private readonly string rutaAlmacenamiento;
		public ProfileService(IAppDbContext context, IConfiguration configuration)
        {
            this.context = context;
			this.rutaAlmacenamiento = configuration["rutaImagenes"]!;
			this.rutaServidor = configuration["rutaServidor"]!;
		}


		public async Task<List<Profile>> GetAllProfiles()
		{
			return await context.Profiles
				.Include(p => p.ProfileCategories)
				.ThenInclude(pc => pc.Category)
				.Include(p => p.User)				
				.ToListAsync();
		}

		public async Task<List<Profile>> GetProfilesByCategory(int categoryId)
		{
			return await context.Profiles
								.Include(p => p.ProfileCategories)
								.ThenInclude(pc => pc.Category)
								.Include(p => p.User)
								.Where(p => p.ProfileCategories.Any(pc => pc.CategoryId == categoryId))
								.ToListAsync();
		}

		public async Task<Profile> GetProfileByUser(int userId)
		{

			return await context.Profiles.Include(p => p.ProfileCategories)
										 .ThenInclude(pc => pc.Category)
										 .Include(p => p.User)										 
										 .FirstOrDefaultAsync(p => p.UserId == userId);
		}

		public async Task<Profile> UpdateProfile(Profile profile)
		{
			var existingProfile = await context.Profiles.FirstOrDefaultAsync(p => p.Id == profile.Id);

			if (existingProfile != null)
			{
				existingProfile.Specialty = profile.Specialty;
				existingProfile.Experience = profile.Experience;
				existingProfile.Description = profile.Description;
				existingProfile.ImageUrl = profile.ImageUrl;
				await context.SaveChangesAsync();
			}

			return existingProfile;
		}

		public async Task UpdateProfileCategories(Profile profile, List<int> categoryIds)
		{
			var existingProfile = await context.Profiles
				.Include(p => p.ProfileCategories)
				.FirstOrDefaultAsync(p => p.Id == profile.Id);

			if (existingProfile != null)
			{				
				existingProfile.ProfileCategories.Clear();
				
				foreach (var categoryId in categoryIds)
				{
					var category = await context.Categories.FindAsync(categoryId);
					if (category != null)
					{
						existingProfile.ProfileCategories.Add(new ProfileCategory { ProfileId = profile.Id, CategoryId = categoryId });
					}
				}
				await context.SaveChangesAsync();
			}
		}

		


		public async Task<string> SaveImageAsync(IFormFile image)
		{

			string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
			
			string fullPath = Path.Combine(rutaAlmacenamiento, fileName);
			
			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				await image.CopyToAsync(stream);
			}			
			return $"{rutaServidor}/images/{fileName}";			
		}
	}
}
