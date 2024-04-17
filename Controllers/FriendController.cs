using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_Project.DAL.Interface;
using Web_API_Project.Model;

namespace Web_API_Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FriendsController : ControllerBase
	{
		private readonly IFriendsService _friendsService;
		private readonly ILogger<FriendsController> _logger;

		
		public FriendsController(ILogger<FriendsController> logger, IFriendsService friendsService)
		{
			_logger = logger;
			_friendsService = friendsService;
		}


		[HttpPost]
		public IActionResult CreateFriend([FromBody] FriendsModel friend)
		{
			var createdFriend = _friendsService.CreateFriend(friend);
			return Ok(createdFriend);
		}

		[HttpGet("{friendId}")]
		public IActionResult GetFriendById(int friendId)
		{
			var friend = _friendsService.GetFriendById(friendId);
			if (friend == null)
			{
				return NotFound();
			}
			return Ok(friend);
		}

		[HttpPut("{friendId}")]
		public IActionResult UpdateFriend(int friendId, [FromBody] FriendsModel friend)
		{
			if (friendId != friend.FriendId)
			{
				return BadRequest();
			}

			var updatedFriend = _friendsService.UpdateFriend(friend);
			if (updatedFriend == null)
			{
				return NotFound();
			}
			return Ok(updatedFriend);
		}

		[HttpDelete("{friendId}")]
		public IActionResult DeleteFriend(int friendId)
		{
			var result = _friendsService.DeleteFriend(friendId);
			if (!result)
			{
				return NotFound();
			}
			return NoContent();
		}
	}

}
