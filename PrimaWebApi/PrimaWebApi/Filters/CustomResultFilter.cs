using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PrimaWebApi.Filters
{
	public class CustomResultFilter : Attribute, IResultFilter
	{
		public void OnResultExecuting(ResultExecutingContext context)
		{
			if (context.Result is ObjectResult objectResult)
			{
				objectResult.Value = JsonConvert.SerializeObject(new { Message = "HELLO WORLD" });
				Console.WriteLine($"Il risultato sarà {JsonConvert.SerializeObject(objectResult.Value)}");
			}
		}
		public void OnResultExecuted(ResultExecutedContext context)
		{
			Console.WriteLine($"Il risultato è {JsonConvert.SerializeObject(context.Result)}");
		}
	}
}
