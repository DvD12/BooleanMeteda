using System.Reflection;

namespace Attributi
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			ClasseVecchia a = new ClasseVecchia();
			WowChecker();
		}

		public static void WowChecker()
		{
			MethodInfo[] methods = typeof(Lavoro).GetMethods();
			foreach (MethodInfo method in methods)
			{
				WowAttribute attributo = (WowAttribute)Attribute.GetCustomAttribute(method, typeof(WowAttribute));
				if (attributo != null)
				{
					for (int i = 0; i < attributo.Wows; i++)
					{
						Console.WriteLine($"{method.Name}: Wow {i}!");
					}
				}
			}
		}
	}
}
