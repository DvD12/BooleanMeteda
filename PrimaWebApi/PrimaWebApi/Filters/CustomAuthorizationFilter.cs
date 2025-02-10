using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PrimaWebApi.Filters
{

	public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			bool isAuthorized = DateTime.Now.Second < 30;
			if (!isAuthorized)
			{
				context.Result = new BadRequestResult();
			}
		}
	}
}
