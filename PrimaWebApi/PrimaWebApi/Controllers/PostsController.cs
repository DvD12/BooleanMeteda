using Microsoft.AspNetCore.Mvc;
using PrimaWebApi.Data;

namespace PrimaWebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PostsController : ControllerBase
	{
		[HttpGet]
		public async Task<List<Post>> Get()
		{
			return await PostRepository.GetAllPosts();
		}

		[HttpGet("{id}")]
		public async Task<Post> GetPost(int id)
		{
			return await PostRepository.GetPost(id);
		}
	}
}
