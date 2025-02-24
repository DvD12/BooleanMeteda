using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWPF.Services
{
	public class ApiServiceResult<T>
	{
		public T Data { get; set; }
		public bool IsConnectionSuccess { get; set; }
		public string ErrorMessage { get; set; }

		public ApiServiceResult(T data)
		{
			IsConnectionSuccess = true;
			Data = data;
		}
		public ApiServiceResult(Exception e)
		{
			IsConnectionSuccess = false;
			ErrorMessage = e.Message;
		}
	}
}
