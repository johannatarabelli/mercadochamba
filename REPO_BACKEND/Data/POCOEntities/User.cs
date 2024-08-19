namespace backnc.Data.POCOEntities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string email { get; set; }
		public string dni { get; set; }
		public string phoneNumber { get; set; }		
		public string address { get; set; }
		public string Password { get; set; }
		//public int CountryId { get; set; }
		//public int ProvinceId { get; set; }
		//public int NeighborhoodId { get; set; }

		public Profile Profile { get; set; }

		// Relación uno a muchos con Trabajos
		public ICollection<Job> Job { get; set; }

		//ROLES
		public ICollection<UserRole> UserRoles { get; set; }
    }
}
