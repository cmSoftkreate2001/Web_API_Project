using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_Project.DAL.Interface;
using Web_API_Project.Model;

namespace Web_API_Project.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		
		private readonly IUserService _userService;

		private readonly ILogger<UserController> _logger;


		public UserController(ILogger<UserController> logger, IUserService userService)
		{
			_logger = logger;
			_userService = userService;
		}


		[HttpPost]
		public IActionResult CreateUser([FromBody] UserModel user)
		{
			var createdUser = _userService.CreateUser(user);
			return Ok(createdUser);
		}

		[HttpGet("{userId}")]
		public IActionResult GetUserById(int userId)
		{
			var user = _userService.GetUserById(userId);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		[HttpPut("{userId}")]
		public IActionResult UpdateUser(int userId, [FromBody] UserModel user)
		{
			if (userId != user.UserId)
			{
				return BadRequest();
			}

			var updatedUser = _userService.UpdateUser(user);
			if (updatedUser == null)
			{
				return NotFound();
			}
			return Ok(updatedUser);
		}

		[HttpDelete("{userId}")]
		public IActionResult DeleteUser(int userId)
		{
			var result = _userService.DeleteUser(userId);
			if (!result)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
