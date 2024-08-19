using System.Text.Json.Serialization;

namespace backnc.Data.POCOEntities
{
	public class Province
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CountryId { get; set; }		
		public Country Country { get; set; }
		public ICollection<Neighborhood> Neighborhoods { get; set; }
	}
}
