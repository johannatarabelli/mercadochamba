using backnc.Data.Interface;
using backnc.Data.POCOEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace backnc.Data.Context
{
    public class AppDbContext : DbContext , IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			modelBuilder.Entity<Job>()
			   .HasOne(j => j.User)
			   .WithMany(u => u.Job)
			   .HasForeignKey(j => j.UserId)
			   .OnDelete(DeleteBehavior.Restrict);
			
			modelBuilder.Entity<ProfileCategory>()
				.HasKey(pc => new { pc.ProfileId, pc.CategoryId });
			
			modelBuilder.Entity<ProfileCategory>()
				.HasOne(pc => pc.Profile)
				.WithMany(p => p.ProfileCategories)
				.HasForeignKey(pc => pc.ProfileId);

			modelBuilder.Entity<ProfileCategory>()
				.HasOne(pc => pc.Category)
				.WithMany(c => c.ProfileCategories)
				.HasForeignKey(pc => pc.CategoryId);

		}
        public DbSet<TodoTest> TodoTests { get; set; }
        public  DbSet<User> Users { get; set; }
        public  DbSet<Role> Roles { get; set; }
        public  DbSet<UserRole> UserRoles { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Province> Provinces { get; set; }
		public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Profile> Profiles { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ProfileCategory> ProfileCategories { get; set; }

	}
}
