using backnc.Common.DTOs.CountryDTOs;
using backnc.Common.DTOs.ProvinceDTO;
using backnc.Data.POCOEntities;

namespace backnc.Interfaces
{
	public interface IProvinceSerivce
	{
		Task<IEnumerable<ProvinceDTO>> GetAllProvinces();
		Task<IEnumerable<ProvinceDTO>> GetProvincesByCountryId(int countryId);
		Task<ProvinceDTO> GetProvinceById(int id);
		Task<ProvinceDTO> AddProvince(CreateProvinceDTO createProvinceDTO);
		Task<ProvinceDTO> UpdateProvince(int id, EditProvinceDTO editProvinceDTO);
		Task<bool> DeleteProvince(int id);
	}
}
