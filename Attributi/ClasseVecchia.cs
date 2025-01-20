using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attributi
{
	// Avrei potuto scrivere [ObsoleteAttribute]
	[Obsolete("Usa la classe 'ClasseNuova'")] public class ClasseVecchia
	{
	}

	public class ClasseNuova
	{
		[Obsolete($"Usa la proprietà {nameof(ProprietaVecchia)}")] public int ProprietaVecchia { get; set; }
		public int ProprietaNuova { get; set; }
	}

	public class Lavoro
	{
		[WowAttribute] public void Lavora() { }

		[Wow(5)] public void LavoraIlDoppio() { }

		[Wow(10)] public void Programma() { }

		[Wow(20)] public void ProgrammaInCSharp() { }

		[Wow(-5)] public void ProgrammaInJavaScript() { }
	}
}
