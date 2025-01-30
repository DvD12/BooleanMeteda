using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace M010_Generics
{
	public class NodeInt // Questa classe è solo per gli interi!
	{
		public int Element { get; set; }
		public NodeInt Next { get; set; }

		public NodeInt() { }

		public NodeInt(int element)
		{
			this.Element = element;
		}
	}
	public class NodeString // Questa classe è solo per le stringhe!
	{
		public string Element { get; set; }
		public NodeString Next { get; set; }

		public NodeString() { }

		public NodeString(string element)
		{
			this.Element = element;
		}
	}
	public class Node<T> // Questa classe astrae dal tipo di dato int, string etc.: diventa il generico tipo PIPPO
	{
		public T Element { get; set; }
		public Node<T> Next { get; set; }

		public Node() { }

		public Node(T element)
		{
			this.Element = element;
			//Element++; // Non si può fare! Non ho abbastanza informazioni sul tipo T da poter dedurre che sia un numero!
		}
	}

	// Tentiamo di restringere il campo ai soli T che implementano una certa interfaccia
	public class NodeDiNumeri<T> where T : INumber<T>
	{
		public T Element { get; set; }
		public NodeDiNumeri<T> Next { get; set; }

		public NodeDiNumeri() { }

		public NodeDiNumeri(T element)
		{
			this.Element = element;
		}

		public void Incrementa()
		{
			Element = Element++;
		}

		public void Aggiungi(T numero2)
		{
			Element = Element + numero2;
		}
	}
}
