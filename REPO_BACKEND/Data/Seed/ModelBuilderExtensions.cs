using backnc.Data.Context;
using backnc.Data.POCOEntities;
using backnc.Models;
using Microsoft.EntityFrameworkCore;

namespace backnc.Data.Seed
{
	public static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>().HasData(
				new Role { 
					Id = 1, 
					Name = "Admin" },
				new Role { 
					Id = 2, 
					Name = "Cliente" }
			);

			modelBuilder.Entity<RegisterUser>().HasData(
				new User { 
					Id = 1, 
					firstName = "administrador",
					lastName = "administrador",
					email = "admin@gmail.com",					
					UserName = "admin", 
					Password = "Admin123!"
				},
				new User { 
					Id = 2, 
					UserName = "user",
					firstName = "user",
					lastName = "user",
					email = "user@gmail.com",
					Password = "User123!"
				}
			);

			modelBuilder.Entity<UserRole>().HasData(
				new UserRole { 
					UserId = 1, 
					RoleId = 1 },
				new UserRole { 
					UserId = 2,
					RoleId = 2 }
			);
		}		
	}
}
