using Microsoft.AspNetCore.Mvc;
using PrimaWebApi.Data;
using PrimaWebApi.Services;

namespace PrimaWebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly JwtAuthenticationService _jwt;
		private readonly UserService _userService;

		public AccountController(JwtAuthenticationService jwt, UserService userService)
		{
			this._jwt = jwt;
			this._userService = userService;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] UserModel user)
		{
			var result = await _userService.RegisterAsync(user);
			if (!result)
			{
				return BadRequest(new { Message = "Registrazione fallita!" });
			}

			return Ok(new { Message = "Registrazione avvenuta con successo!" });
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] UserModel user)
		{
			var token = await _jwt.Authenticate(user.Email, user.Password);

			if (token == null)
			{
				return Unauthorized();
			}

			return Ok(new
			{
				Token = token,
				ExpirationUtc = DateTime.UtcNow.AddMinutes(_jwt.Settings.DurationInMinutes)
			});
		}
	}
}
