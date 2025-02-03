namespace PrimaWebApi.Loggers
{
	public interface ICustomLogger
	{
		void WriteLog(string message);
	}
	public class CustomConsoleLogger : ICustomLogger
	{
		public void WriteLog(string message)
		{
			Console.WriteLine($"LOG {message}");
		}
	}
	public class CustomFileLogger : ICustomLogger
	{
		public void WriteLog(string message)
		{
			//File.AppendAllText(...);
		}
	}
}
