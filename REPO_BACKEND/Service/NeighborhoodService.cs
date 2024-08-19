using backnc.Common.DTOs.CountryDTOs;
using backnc.Common.DTOs.NeighborhoodDTO;
using backnc.Data.Interface;
using backnc.Data.POCOEntities;
using backnc.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backnc.Service
{
	public class NeighborhoodService : INeighborhoodService
	{
		private readonly IAppDbContext context;

		public NeighborhoodService(IAppDbContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<NeighborhoodDTO>> GetAllNeighborhoods()
		{
			return await context.Neighborhoods
				.Select(n => new NeighborhoodDTO
				{
					Id = n.Id,
					Name = n.Name
				}).ToListAsync();
		}

		public async Task<IEnumerable<NeighborhoodDTO>> GetNeighborhoodsByProvinceId(int provinceId)
		{
			return await context.Neighborhoods
				.Where(n => n.ProvinceId == provinceId)
				.Select(n => new NeighborhoodDTO
				{
					Id = n.Id,
					Name = n.Name
				}).ToListAsync();
		}

		public async Task<NeighborhoodDTO> GetNeighborhoodById(int id)
		{
			var neighborhood = await context.Neighborhoods.FindAsync(id);
			if (neighborhood == null) return null;

			return new NeighborhoodDTO
			{
				Id = neighborhood.Id,
				Name = neighborhood.Name
			};
		}

		public async Task<NeighborhoodDTO> AddNeighborhood(CreateNeighborhoodDTO createNeighborhoodDTO)
		{
			var neighborhood = new Neighborhood
			{
				Name = createNeighborhoodDTO.Name,
				ProvinceId = createNeighborhoodDTO.ProvinceId
			};
			context.Neighborhoods.Add(neighborhood);
			await context.SaveChangesAsync();

			return new NeighborhoodDTO { Id = neighborhood.Id,	Name = neighborhood.Name };
		}

		public async Task<NeighborhoodDTO> UpdateNeighborhood(int id, EditNeighborhoodDTO editNeighborhoodDTO)
		{
			var neighborhood = await context.Neighborhoods.FindAsync(id);
			if (neighborhood == null) return null;

			neighborhood.Name = editNeighborhoodDTO.Name;
			await context.SaveChangesAsync();

			return new NeighborhoodDTO { Id = neighborhood.Id, Name = neighborhood.Name };
		}

		public async Task<bool> DeleteNeighborhood(int id)
		{
			var neighborhood = await context.Neighborhoods.FindAsync(id);
			if (neighborhood == null) return false;

			context.Neighborhoods.Remove(neighborhood);
			await context.SaveChangesAsync();
			return true;
		}
	}
}
