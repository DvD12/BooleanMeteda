using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M009_GestioneFile
{
	public class ClasseDisposable : IDisposable
	{
		public string Nome { get; set; }

		public ClasseDisposable() { }
		public ClasseDisposable(string nome)
		{
			this.Nome = nome;
		}

		~ClasseDisposable()
		{
			Console.WriteLine($"Io, {Nome}, sono stato distrutto dal distruttore!");
		}

		public void Dispose()
		{
			Console.WriteLine($"Io, {Nome}, sono stato cancellato!");
		}
	}
}
