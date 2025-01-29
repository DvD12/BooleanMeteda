using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M017_AsyncAwait
{
	public static class Extensions
	{
		public static string WriteDebugLine(string text)
		{
			text = $"{DateTime.Now} {Thread.CurrentThread.ManagedThreadId} {text}";
			Console.WriteLine(text);
			return text;
		}
	}
}
