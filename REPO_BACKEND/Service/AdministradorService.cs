using backnc.Data.Interface;
using Microsoft.EntityFrameworkCore;
using backnc.Data.POCOEntities;
using backnc.Common.DTOs.AdministradorDTO;

namespace backnc.Service
{
	public class AdministradorService
	{
		private readonly IAppDbContext _appDbContext;
		public AdministradorService(IAppDbContext _appDbContext)
		{
			this._appDbContext = _appDbContext;
		}

		public async Task<IEnumerable<User>> GetAllAdminsAsync()
		{
			return await _appDbContext.Users
				.Include(u => u.UserRoles)
				.Where(u => u.UserRoles.Any(ur => ur.Role.Name == "Admin"))
				.ToListAsync();
		}

		public async Task<User> GetAdminByIdAsync(int id)
		{
			return await _appDbContext.Users
				.Include(u => u.UserRoles)
				.FirstOrDefaultAsync(u => u.Id == id && u.UserRoles.Any(ur => ur.Role.Name == "Admin"));
		}

		public async Task CreateAdminAsync(User user)
		{
			_appDbContext.Users.Add(user);
			await _appDbContext.SaveChangesAsync();
			
			var role = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
			var userRole = new UserRole
			{
				UserId = user.Id,
				RoleId = role.Id
			};

			_appDbContext.UserRoles.Add(userRole);
			await _appDbContext.SaveChangesAsync();
		}

		public async Task UpdateAdminAsync(User user)
		{
			_appDbContext.Users.Update(user);
			await _appDbContext.SaveChangesAsync();
		}

		public async Task DeleteAdminAsync(int id)
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
