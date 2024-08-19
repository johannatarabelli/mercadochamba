using backnc.Data.POCOEntities;

namespace backnc.Interfaces
{
	public interface IJobService
	{
		Task<IEnumerable<Job>> GetJobsByProfileId(int profileId);
		Task CreateJob(Job job);
		Task<string> SaveImageAsync(IFormFile image);
	}
}
