namespace backnc.Data.POCOEntities
{
	public class Profile
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Specialty { get; set; }
		public string Experience { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }		
		public User User { get; set; }
		public ICollection<ProfileCategory> ProfileCategories { get; set; } = new List<ProfileCategory>();
	}
}
