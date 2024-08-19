using backnc.Entities;
using backnc.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backnc.Services
{
	public class TokenService
	{
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        public TokenService(IConfiguration _configuration, UserManager<Users> _userManager)
        {
            this._configuration = _configuration;
            this._userManager = _userManager;
        }

        public async Task<RtaAuth> CrearToken(string email, int diasExp)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(usuario);
			var claims = new List<Claim>() 
            {
				new Claim("mail", email)
			};
			foreach (var role in roles) 
			{
				claims.Add(new Claim(ClaimTypes.Role, role)); 
			}
			
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiration = DateTime.UtcNow.AddDays(diasExp);

			var token = new JwtSecurityToken(
				issuer: null,
				audience: null,
				claims: claims,
				signingCredentials: creds
				);

			var rtaTkoen = new JwtSecurityTokenHandler().WriteToken(token);
			return new RtaAuth(token: rtaTkoen, esAdmin: usuario.isAdmin);


		}
    }
}
