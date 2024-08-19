namespace backnc.Data.POCOEntities
{
	public class Job
	{
		public int Id { get; set; }
		public int ProfileId { get; set; }
		public int UserId { get; set; }  
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public DateTime CreatedAt { get; set; }		
		public Profile Profile { get; set; }
		public User User { get; set; } // Agrega esta propiedad


	}
}
