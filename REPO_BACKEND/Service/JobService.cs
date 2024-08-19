using backnc.Data.Interface;
using backnc.Data.POCOEntities;
using Microsoft.EntityFrameworkCore;

namespace backnc.Service
{
	public class JobService
	{
        private readonly IAppDbContext context;
		private readonly string rutaServidor;
		private readonly string rutaAlmacenamiento;
		public JobService(IAppDbContext context,IConfiguration configuration)
        {
            this.context = context;
			this.rutaAlmacenamiento = configuration["rutaImagenes"]!;
			this.rutaServidor = configuration["rutaServidor"]!;
		}

		public async Task<IEnumerable<Job>> GetJobsByProfileId(int profileId)
		{
			return await context.Jobs
				.Where(j => j.ProfileId == profileId)
				.Include(j => j.Profile) 
				.ToListAsync();
		}

		public async Task<Job> GetJobByIdAsync(int jobId)
		{
			return await context.Jobs.FindAsync(jobId);
		}

		public async Task<IEnumerable<Job>> GetAllJobs()
		{
			return await context.Jobs.ToListAsync();
		}

		public async Task<IEnumerable<Job>> GetJobsByUserId(int userId)
		{
			return await context.Jobs.Where(j => j.Profile.UserId == userId).ToListAsync();
		}

		public async Task DeleteJobAsync(int jobId)
		{
			var job = await context.Jobs.FindAsync(jobId);
			if (job != null)
			{
				context.Jobs.Remove(job);
				await context.SaveChangesAsync();
			}
		}
		public async Task CreateJob(Job job)
		{
			context.Jobs.Add(job);
			await context.SaveChangesAsync();
		}
		public async Task<string> SaveImageAsync(IFormFile image)
		{
			// Generar un nombre de archivo único
			string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
			// Ruta completa para almacenar la imagen
			string fullPath = Path.Combine(rutaAlmacenamiento, fileName);

			// Guardar la imagen en el sistema de archivos
			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				await image.CopyToAsync(stream);
			}
			// Devolver la URL relativa de la imagen
			return $"{rutaServidor}/images/{fileName}";

		}
	}
}
