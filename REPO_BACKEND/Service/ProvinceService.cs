using backnc.Common.DTOs;
using backnc.Common.DTOs.NeighborhoodDTO;
using backnc.Common.DTOs.ProvinceDTO;
using backnc.Data.Interface;
using backnc.Data.POCOEntities;
using backnc.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backnc.Service
{
	public class ProvinceService : IProvinceSerivce
	{
		private readonly IAppDbContext context;

		public ProvinceService(IAppDbContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<ProvinceDTO>> GetAllProvinces()
		{
			return await context.Provinces
				.Include(p => p.Neighborhoods)
				.Select(p => new ProvinceDTO
				{
					Id = p.Id,
					Name = p.Name,
					Neighborhoods = p.Neighborhoods.Select(n => new Neighborhood
					{
						Id = n.Id,
						Name = n.Name
					}).ToList()
				}).ToListAsync();
		}

		public async Task<IEnumerable<ProvinceDTO>> GetProvincesByCountryId(int countryId)
		{
			return await context.Provinces
				.Where(p => p.CountryId == countryId)
				.Include(p => p.Neighborhoods)
				.Select(p => new ProvinceDTO
				{
					Id = p.Id,
					Name = p.Name,
					Neighborhoods = p.Neighborhoods.Select(n => new Neighborhood
					{
						Id = n.Id,
						Name = n.Name
					}).ToList()
				}).ToListAsync();
		}

		public async Task<ProvinceDTO> GetProvinceById(int id)
		{
			var province = await context.Provinces
				.Include(p => p.Neighborhoods)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (province == null) return null;

			return new ProvinceDTO
			{
				Id = province.Id,
				Name = province.Name,
				Neighborhoods = province.Neighborhoods.Select(n => new Neighborhood
				{
					Id = n.Id,
					Name = n.Name
				}).ToList()
			};
		}

		public async Task<ProvinceDTO> AddProvince(CreateProvinceDTO createProvinceDTO)
		{
			var province = new Province
			{
				Name = createProvinceDTO.Name,
				CountryId = createProvinceDTO.CountryId
			};
			context.Provinces.Add(province);
			await context.SaveChangesAsync();

			return new ProvinceDTO { Id = province.Id, Name = province.Name };
		}

		public async Task<ProvinceDTO> UpdateProvince(int id, EditProvinceDTO editProvinceDTO)
		{
			var province = await context.Provinces.FindAsync(id);
			if (province == null) return null;

			province.Name = editProvinceDTO.Name;
			await context.SaveChangesAsync();

			return new ProvinceDTO { Id = province.Id, Name = province.Name };
		}

		public async Task<bool> DeleteProvince(int id)
		{
			var province = await context.Provinces.FindAsync(id);
			if (province == null) return false;

			context.Provinces.Remove(province);
			await context.SaveChangesAsync();
			return true;
		}
	}
}
