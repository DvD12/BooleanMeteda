using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PrimaWebApi.Filters
{
	public class CustomExceptionFilter : Attribute, IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			Console.WriteLine(context.Exception.Message);
			context.Result = new JsonResult(new
			{
				Message = $"Abbiamo riscontrato un errore",
				Detail = context.Exception.Message
			})
			{
				StatusCode = 500
			};
		}
	}
}
