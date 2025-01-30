using System.Reflection;
using AdoNet;

namespace Attributi
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//ClassePerIlMondoEsterno c = new ClassePerIlMondoEsterno(); // Questa classe è internal, quindi si riferisce solo al suo assembly, al suo compilato (il suo progetto), nonostante abbiamo il riferimento al suo progetto
			//c.ProprietaPubblica = 1234;
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
