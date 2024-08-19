using backnc.Common.DTOs;
using backnc.Data.POCOEntities;

namespace backnc.Common.DTOs.ProvinceDTO
{
	public class ProvinceDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<Neighborhood> Neighborhoods { get; set; }


	}
}
