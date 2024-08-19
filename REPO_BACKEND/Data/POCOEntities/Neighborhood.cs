namespace backnc.Data.POCOEntities
{
	public class Neighborhood
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ProvinceId { get; set; }
		public Province Province { get; set; }
	}
}
