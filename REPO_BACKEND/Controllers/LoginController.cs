using backnc.Common.Response;
using backnc.Interfaces;
using backnc.Models;
using Microsoft.AspNetCore.Mvc;

namespace backnc.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IUserService _userService;

		public LoginController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public async Task<ActionResult<BaseResponse>> Login(LoginUser userLogin)
		{
			return await _userService.Authenticate(userLogin);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
		{
			var result = await _userService.Register(registerUser);
			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPost("validate-token")]
		public async Task<IActionResult> ValidateToken([FromBody] string token)
		{
			var response = await _userService.ValidateToken(token);

			if (!response.IsSuccess)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpGet("validate-token")]
		public async Task<IActionResult> ValidateToken()
		{
			if (!Request.Headers.ContainsKey("Authorization"))
			{
				return BadRequest(new BaseResponse { 
					IsSuccess = false, 
					status = "error", 
					message = "El encabezado de autorización no está presente." });
			}

			var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
			var response = await _userService.ValidateToken(token);

			if (!response.IsSuccess)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
