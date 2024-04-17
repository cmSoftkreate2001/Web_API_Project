using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_Project.DAL.Interface;
using Web_API_Project.Model;

namespace Web_API_Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostDataController : ControllerBase
	{

		private readonly IPostDataService _postDataService;

		private readonly ILogger<PostDataController> _logger;


		public PostDataController(ILogger<PostDataController> logger, IPostDataService postDataService)
		{
			_logger = logger;
			_postDataService = postDataService;
		}


		[HttpPost]
		public IActionResult CreatePostData([FromBody] PostDataModel postData)
		{
			var createdPostData = _postDataService.CreatePostData(postData);
			return Ok(createdPostData);
		}

		[HttpGet("{postId}")]
		public IActionResult GetUserById(int postId)
		{
			var postData = _postDataService.GetPostDataById(postId);
			if (postData == null)
			{
				return NotFound();
			}
			return Ok(postData);
		}

		[HttpPut("{postId}")]
		public IActionResult UpdateUser(int postId, [FromBody] PostDataModel postData)
		{
			if (postId != postData.PostId)
			{
				return BadRequest();
			}

			var updatedPostData = _postDataService.UpdatePostData(postData);
			if (updatedPostData == null)
			{
				return NotFound();
			}
			return Ok(updatedPostData);
		}

		[HttpDelete("{postId}")]
		public IActionResult DeletePostData(int postId)
		{
			var result = _postDataService.DeletePostData(postId);
			if (!result)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
