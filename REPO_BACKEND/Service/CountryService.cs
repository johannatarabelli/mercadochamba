using backnc.Common.DTOs.CountryDTOs;
using backnc.Data.Interface;
using backnc.Data.POCOEntities;
using backnc.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backnc.Service
{
	public class CountryService : ICountryService
	{
		private readonly IAppDbContext context;

        public CountryService(IAppDbContext context)
        {
            this.context = context;
        }

		public async Task<IEnumerable<Country>> GetAllCountries()
		{
			return await context.Countries.ToListAsync();
		}

		public async Task<Country> GetCountryById(int id)
		{
			return await context.Countries.FindAsync(id);
		}

		public async Task<Country> AddCountry(CreateCountryDTO createCountryDTO)
		{
			var country = new Country
			{
				Name = createCountryDTO.name
			};

			context.Countries.Add(country);
			await context.SaveChangesAsync();
			return country;
		}

		public async Task<Country> UpdateCountry(int id, EditCountryDTOs editCountryDTOs)
		{
			var country = await context.Countries.FindAsync(id);
			if (country == null)
			{
				return null;
			}

			country.Name = editCountryDTOs.name;
			await context.SaveChangesAsync();
			return country;
		}

		public async Task<bool> DeleteCountry(int id)
		{
			var country = await context.Countries.FindAsync(id);
			if (country == null)
			{
				return false;
			}

			context.Countries.Remove(country);
			await	context.SaveChangesAsync();
			return true;
		}
	}
}
