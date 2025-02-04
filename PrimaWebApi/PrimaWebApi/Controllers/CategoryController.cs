using Microsoft.AspNetCore.Mvc;
using PrimaWebApi.Data;

namespace PrimaWebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		private CategoryRepository CategoryRepository { get; set; }

		public CategoryController(CategoryRepository c)
		{
			CategoryRepository = c;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string? title)
		{
			try
			{
				return Ok(await CategoryRepository.GetAllCategories()); // Senza await non ritornerebbe la List<Category>, ma tenterebbe di ritornare il task (che potrebbe non aver ancora finito). Con await "scartiamo" il task
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
