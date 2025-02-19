using M32_ProgettoDaTestare;

namespace M32_Test
{
	public class TestStampatore
	{
		private Stampatore Istanza { get; set; }

		[SetUp]
		public void Setup()
		{
			//if (Istanza == null) // se avessi questo controllo, un eventuale stato interno della mia istanza
			// potrebbe essere influenzato da uno dei miei test -> il successo/la verifica nei miei test
			// DIPENDEREBBE dall'ordine di esecuzione dei test.

			// Imponendo, invece, la creazione ex-novo del mio oggetto a ogni esecuzione di ogni test, mi assicuro
			// di ripartire dalle STESSE BASI DI PARTENZA per ogni test, indipendentemente dall'ordine di esecuzione.
			{
				Istanza = new Stampatore();
			}
		}

		[TestCase("CIAO", "CIAO")]
		[TestCase("Buongiorno", "Buongiorno")]
		[Test]
		public void TestStampa(string input, string expectedOutput)
		{
			string output = Istanza.Stampa(input);
			Assert.IsTrue(Istanza.MessaggiStampati == 1, $"Output atteso: 1, output effettivo: {Istanza.MessaggiStampati}"); // Tale messaggio verrà scritto nel test explorer in caso di fallimento
			Assert.IsTrue(output == expectedOutput);
		}

		[TestCase("Buongiorno", "onroignouB")]
		[TestCase("", "")]
		[Test]
		public void TestStampaAlContrario(string input, string expectedOutput)
		{
			string output = Istanza.StampaAlContrario(input);
			Assert.IsTrue(Istanza.MessaggiStampati == 1, $"Output atteso: 1, output effettivo: {Istanza.MessaggiStampati}");
			Assert.IsTrue(output == expectedOutput);
		}

		[TestCase("Ciao", "Buongiorno", "Ciao Buongiorno")]
		[TestCase("Ciao", "", "Ciao")]
		[Test]
		public void TestStampaDueParole(string primaParola, string secondaParola, string expectedOutput)
		{
			string output = Istanza.StampaDueParole(primaParola, secondaParola);
			Assert.IsTrue(Istanza.MessaggiStampati == 1, $"Output atteso: 1, output effettivo: {Istanza.MessaggiStampati}");
			Assert.IsTrue(output == expectedOutput);
		}
	}
}