namespace backnc.Common.DTOs.ProfileDTO
{
	public class EditProfileDTO
	{
		public string Specialty { get; set; }
		public string Experience { get; set; }
		public string Description { get; set; }
		public IFormFile Image { get; set; }

	}
}
