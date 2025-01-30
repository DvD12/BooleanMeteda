using Microsoft.AspNetCore.Mvc;
using PrimaWebApi.Data;

namespace PrimaWebApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class PostsController : ControllerBase
	{
		[HttpGet]
		public List<Post> Get()
		{
			return PostRepository.GetAllPosts();
		}

		[HttpGet("{id}")]
		public Post GetPost(int id)
		{
			return PostRepository.GetPost(id);
		}
	}
}
