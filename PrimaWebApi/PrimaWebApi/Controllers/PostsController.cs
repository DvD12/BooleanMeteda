using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimaWebApi.Data;
using PrimaWebApi.Filters;
using PrimaWebApi.Loggers;

namespace PrimaWebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PostsController : ControllerBase
	{
		private PostRepository PostRepository { get; set; }
		private ICustomLogger _logger { get; set; }

		// Dependency injection! Qualcuno di esterno (il nostro framework ASP.NET
		// passerà un'istanziazione concreta dell'interfaccia al costruttore di questa classe
		public PostsController(PostRepository p, ICustomLogger l)
		{
			PostRepository = p; // dependency injection: "p" viene passata al costruttore dal nostro framework)
			// _logger = new CustomFileLogger(); // Non è più responsabilità diretta di questa classe!
			_logger = l;
			//Console.WriteLine(_logger.GetHashCode());
		}

		[HttpGet]
		[LogActionFilter]
		//[CustomAuthorization]
		[CustomExceptionFilter]
		[CustomResultFilter]
		public async Task<IActionResult> Get(string? title)
		{
			_logger.WriteLog("SONO APPENA ENTRATO NELL'API (richiesta GET)");
			//throw new Exception("ECCEZIONE!!!1!11!!"); // solo per testare CustomExceptionFilter
			try
			{
				if (title == null)
				{
					// Se volessi ritornare un sottoinsieme delle mie colonne/proprietà della mia tabella/classe Post, posso mappare ogni Post della
					// mia lista in un tipo di dato anonimo che contiene solo le proprietà che voglio io
					//return Ok((await PostRepository.GetAllPosts()).Select(x => new { x.Title, x.Author }));
					
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
		[Authorize]
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
		[Authorize(Roles = "Admin")]
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
