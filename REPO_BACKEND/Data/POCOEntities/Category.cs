namespace backnc.Data.POCOEntities
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }		
		public ICollection<ProfileCategory> ProfileCategories { get; set; }
	}
}
