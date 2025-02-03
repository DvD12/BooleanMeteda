using Microsoft.AspNetCore.Mvc;
using PrimaWebApi.Data;
using PrimaWebApi.Loggers;

namespace PrimaWebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PostsController : ControllerBase
	{
		private PostRepository PostRepository { get; set; }
		private ICustomLogger _logger { get; set; }
		private ICustomLogger _logger2 { get; set; }

		// Dependency injection! Qualcuno di esterno (il nostro framework ASP.NET
		// passerà un'istanziazione concreta dell'interfaccia al costruttore di questa classe
		public PostsController(ICustomLogger l)
		{
			PostRepository = new PostRepository();
			// _logger = new CustomFileLogger(); // Non è più responsabilità diretta di questa classe!
			_logger = l;
			Console.WriteLine(_logger.GetHashCode());
		}

		[HttpGet]
		public async Task<IActionResult> Get(string? title)
		{
			_logger.WriteLog("Richiesta GET");
			try
			{
				if (title == null)
				{
					return Ok(await PostRepository.GetAllPosts());
				}
				else
				{
					return Ok(await PostRepository.GetPostsByTitle(title));
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPost(int id)
		{
			try
			{
				Post p = await PostRepository.GetPost(id);
				if (p == null)
				{
					return NotFound();
				}
				else
				{
					return Ok(p);
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// Metodo per creare un nuovo post.
		/// </summary>
		/// <param name="newPost">Il nuovo post da creare</param>
		/// <returns>IActionResult avente al suo interno il numero di righe DB coinvolte</returns>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Post newPost)
		{
			try
			{
				if (ModelState.IsValid == false)
				{
					return BadRequest($"Dati non validi: {ModelState.Values}");
				}
				int affectedRows = await PostRepository.CreatePost(newPost);
				return Ok(affectedRows);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] Post updatedPost)
		{
			try
			{
				if (ModelState.IsValid == false)
				{
					return BadRequest($"Dati non validi: {ModelState.Values}");
				}
				int affectedRows = await PostRepository.UpdatePost(id, updatedPost);
				if (affectedRows == 0)
				{
					return NotFound();
				}
				return Ok(affectedRows);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				int affectedRows = await PostRepository.DeletePost(id);
				if (affectedRows == 0)
				{
					return NotFound();
				}
				return Ok(affectedRows);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
