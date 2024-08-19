using backnc.Common.Response;
using backnc.Data.Interface;
using backnc.Common.DTOs.ClientesDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using backnc.Data.POCOEntities;

namespace backnc.Service
{
	public class ClienteService
	{
        private readonly IAppDbContext _appDbContext;        
        public ClienteService(IAppDbContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }

		public async Task<IEnumerable<User>> GetAllClientesAsync()
		{
			return await _appDbContext.Users
				.Include(u => u.UserRoles)
				.Where(u => u.UserRoles.Any(ur => ur.Role.Name == "Cliente"))
				.ToListAsync();
		}

		public async Task<User> GetClienteByIdAsync(int id)
		{
			return await _appDbContext.Users
				.Include(u => u.UserRoles)
				.FirstOrDefaultAsync(u => u.Id == id && u.UserRoles.Any(ur => ur.Role.Name == "Cliente"));
		}

		public async Task CreateClienteAsync(User user)
		{
			_appDbContext.Users.Add(user);
			await _appDbContext.SaveChangesAsync();

			var profile = new Profile
			{
				UserId = user.Id,				
				Specialty = "",
				Experience = "",
				Description = "",
				ImageUrl = ""
			};
			_appDbContext.Profiles.Add(profile);
			await _appDbContext.SaveChangesAsync();

			var role = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Cliente");
			var userRole = new UserRole
			{
				UserId = user.Id,
				RoleId = role.Id
			};



			_appDbContext.UserRoles.Add(userRole);
			await _appDbContext.SaveChangesAsync();
		}

		public async Task UpdateClienteAsync(User user)
		{
			_appDbContext.Users.Update(user);
			await _appDbContext.SaveChangesAsync();
		}

		public async Task DeleteClienteAsync(int id)
		{
			var user = await _appDbContext.Users.FindAsync(id);
			if (user != null)
			{
				_appDbContext.Users.Remove(user);
				await _appDbContext.SaveChangesAsync();
			}
		}

	}
}
