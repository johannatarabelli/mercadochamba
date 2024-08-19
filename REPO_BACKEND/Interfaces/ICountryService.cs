using backnc.Common.DTOs.CountryDTOs;
using backnc.Data.POCOEntities;

namespace backnc.Interfaces
{
	public interface ICountryService
	{
		Task<IEnumerable<Country>> GetAllCountries();
		Task<Country> GetCountryById(int id);
		Task<Country> AddCountry(CreateCountryDTO createCountryDTO);
		Task<Country> UpdateCountry(int id, EditCountryDTOs editCountryDTOs);
		Task<bool> DeleteCountry(int id);
	}
}
