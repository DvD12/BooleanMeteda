using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M017_AsyncAwait
{
	public static class BreakfastExample
	{
		private static System.Random Rng = new System.Random();
		private static async Task<bool> PreparaCaffe()
		{
			Extensions.WriteDebugLine("CAFFÈ: Preparo la caffettiera col caffè");
			Extensions.WriteDebugLine("CAFFÈ: Scaldo la caffettiera...");

			Extensions.WriteDebugLine("CAFFÈ: Attendo...");
			await Task.Delay(Rng.Next(3000, 6000));

			Extensions.WriteDebugLine("CAFFÈ: Spengo e verso il caffè");
			Extensions.WriteDebugLine("CAFFÈ: FATTO!");

			return true;
		}
		private static async Task<bool> PreparaPane()
		{
			Extensions.WriteDebugLine("PANE: Accendo il tostapane");
			Extensions.WriteDebugLine("PANE: Inserisco il pane nel tostapane...");

			Extensions.WriteDebugLine("PANE: Attendo...");
			await Task.Delay(Rng.Next(3000, 6000));

			Extensions.WriteDebugLine("PANE: Spengo ed estraggo il pane");
			Extensions.WriteDebugLine("PANE: FATTO!");

			return true;
		}

		private static async Task<bool> PreparaUova()
		{
			Extensions.WriteDebugLine("UOVA: Accendo il fuoco");
			Extensions.WriteDebugLine("UOVA: Verso le uova sul tegamino...");

			Extensions.WriteDebugLine("UOVA: Attendo...");
			await Task.Delay(Rng.Next(3000, 6000));

			Extensions.WriteDebugLine("UOVA: Spengo il fuoco e verso le uova sul piatto");
			Extensions.WriteDebugLine("UOVA: FATTO!");

			return true;
		}

		public static async Task PreparaColazione() // era void -> async Task
		{
			Extensions.WriteDebugLine("Inizio preparazione colazione...");
			Task<bool> caffè = PreparaCaffe();
			Task<bool> pane = PreparaPane();
			Task<bool> uova = PreparaUova();

			Extensions.WriteDebugLine("ATTENDO la preparazione di tutto...");
			bool t1 = await caffè;
			bool t2 = await pane;
			bool t3 = await uova;
		}

		public static async Task PreparaColazioneStretta()
		{
			Extensions.WriteDebugLine("Inizio preparazione colazione...");
			await PreparaCaffe();
			await PreparaPane();
			await PreparaUova();
		}
	}
}
