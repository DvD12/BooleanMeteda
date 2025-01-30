using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attributi
{
	[AttributeUsage(AttributeTargets.Method)]
	public class WowAttribute : Attribute
	{
		public int Wows { get; set; }

		public WowAttribute() { }

		public WowAttribute(int wows)
		{
			this.Wows = wows;
		}
	}
}
