using backnc.Data.POCOEntities;
using Microsoft.EntityFrameworkCore;

namespace backnc.Data.Interface
{
    public interface IAppDbContext : IDisposable
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<TodoTest> TodoTests { get; }
		DbSet<Country> Countries { get; set; }
		DbSet<Province> Provinces { get; set; }
		DbSet<Neighborhood> Neighborhoods { get; set; }
		DbSet<Job> Jobs { get; set; }
		DbSet<Profile> Profiles { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ProfileCategory> ProfileCategories { get; set; }


		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
