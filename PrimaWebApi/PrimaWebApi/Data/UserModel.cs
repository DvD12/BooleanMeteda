using System.ComponentModel.DataAnnotations;

namespace PrimaWebApi.Data
{

	public class UserModel
	{
		[EmailAddress]
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
