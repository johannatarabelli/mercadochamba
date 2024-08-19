using backnc.Data.POCOEntities;

namespace backnc.Common.DTOs;

public class CountryDTO
{
	public int id { get; set; }
	public string name { get; set; }
	public ICollection<Province> Provinces { get; set; }

}
