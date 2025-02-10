using Microsoft.AspNetCore.Mvc.Filters;

namespace PrimaWebApi.Filters
{
	public class LogActionFilter : ActionFilterAttribute
	{
		public LogActionFilter()
		{
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			Console.WriteLine($"Sto eseguendo l'azione {context.ActionDescriptor.DisplayName} di {context.Controller}");
			base.OnActionExecuting(context);
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			Console.WriteLine($"Ho eseguito l'azione {context.ActionDescriptor.DisplayName} di {context.Controller}");
			base.OnActionExecuted(context);
		}
	}
}
