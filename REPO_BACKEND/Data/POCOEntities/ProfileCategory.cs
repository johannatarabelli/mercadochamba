namespace backnc.Data.POCOEntities
{
	public class ProfileCategory
	{
		public int ProfileId { get; set; }
		public Profile Profile { get; set; }

		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
