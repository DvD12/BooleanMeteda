using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M017_AsyncAwait
{
	public static class BreakfastSlowExample
	{
		private static System.Random Rng = new System.Random();

		private static bool PreparaCaffe()
		{
			Extensions.WriteDebugLine("CAFFÈ: Preparo la caffettiera col caffè");
			Extensions.WriteDebugLine("CAFFÈ: Scaldo la caffettiera...");

			Extensions.WriteDebugLine("CAFFÈ: Attendo...");
			Thread.Sleep(Rng.Next(3000, 6000));

			Extensions.WriteDebugLine("CAFFÈ: Spengo e verso il caffè");
			Extensions.WriteDebugLine("CAFFÈ: FATTO!");

			return true;
		}

		private static bool PreparaPane()
		{
			Extensions.WriteDebugLine("PANE: Accendo il tostapane");
			Extensions.WriteDebugLine("PANE: Inserisco il pane nel tostapane...");

			Extensions.WriteDebugLine("PANE: Attendo...");
			Thread.Sleep(Rng.Next(3000, 6000));

			Extensions.WriteDebugLine("PANE: Spengo ed estraggo il pane");
			Extensions.WriteDebugLine("PANE: FATTO!");

			return true;
		}

		private static bool PreparaUova()
		{
			Extensions.WriteDebugLine("UOVA: Accendo il fuoco");
			Extensions.WriteDebugLine("UOVA: Verso le uova sul tegamino...");

			Extensions.WriteDebugLine("UOVA: Attendo...");
			Thread.Sleep(Rng.Next(3000, 6000));

			Extensions.WriteDebugLine("UOVA: Spengo il fuoco e verso le uova sul piatto");
			Extensions.WriteDebugLine("UOVA: FATTO!");

			return true;
		}

		public static void PreparaColazione()
		{
			bool t1 = PreparaCaffe();
			bool t2 = PreparaPane();
			bool t3 = PreparaUova();
		}
	}
}
