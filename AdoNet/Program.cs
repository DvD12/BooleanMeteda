using System.Data.SqlClient;

namespace AdoNet
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<Videogame> videogames = VideogameRepository.GetFirst10Videogames();
			Videogame videogame1 = VideogameRepository.GetVideogame(1);
			Videogame videogameNonEsistente = VideogameRepository.GetVideogame(5001);

			VideogameRepository.EditDescrizioneVideogames(1, 2); // 1 -> Mollitia illum, 2 -> Atque placeat
		}
	}

	internal class ClassePerIlMondoEsterno // è internal, quindi non può essere vista al di fuori di questo progetto
	{
		public int ProprietaPubblica { get; set; }
	}
}
