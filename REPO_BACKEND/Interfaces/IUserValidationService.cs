using System.Threading.Tasks;
using backnc.Data;
using backnc.Data.Context;
using Microsoft.EntityFrameworkCore;

public interface IUserValidationService
{
	Task<bool> IsUserNameTaken(string userName);
	Task<bool> IsEmailTaken(string email);
	Task<bool> IsDniTaken(string dni);
}

public class UserValidationService : IUserValidationService
{
	private readonly AppDbContext _context;

	public UserValidationService(AppDbContext context)
	{
		_context = context;
	}

	public async Task<bool> IsUserNameTaken(string userName)
	{
		return await _context.Users.AnyAsync(u => u.UserName == userName);
	}

	public async Task<bool> IsEmailTaken(string email)
	{
		return await _context.Users.AnyAsync(u => u.email == email);
	}

	public async Task<bool> IsDniTaken(string dni)
	{
		return await _context.Users.AnyAsync(u => u.dni == dni);
	}
}
