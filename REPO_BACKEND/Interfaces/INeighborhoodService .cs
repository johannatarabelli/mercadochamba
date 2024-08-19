using backnc.Common.DTOs.CountryDTOs;
using backnc.Common.DTOs.NeighborhoodDTO;
using backnc.Data.POCOEntities;

namespace backnc.Interfaces
{
	public interface INeighborhoodService
	{
		Task<IEnumerable<NeighborhoodDTO>> GetAllNeighborhoods();
		Task<IEnumerable<NeighborhoodDTO>> GetNeighborhoodsByProvinceId(int provinceId);
		Task<NeighborhoodDTO> GetNeighborhoodById(int id);
		Task<NeighborhoodDTO> AddNeighborhood(CreateNeighborhoodDTO createNeighborhoodDTO);
		Task<NeighborhoodDTO> UpdateNeighborhood(int id, EditNeighborhoodDTO editNeighborhoodDTO);
		Task<bool> DeleteNeighborhood(int id);



	}
}
