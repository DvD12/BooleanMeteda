using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet
{
	public static class VideogameRepository
	{
		private static string stringaDiConnessione = "Data Source=localhost;Initial Catalog=MioDatabase2025;Integrated Security=True;TrustServerCertificate=True";

		// TODO dovremmo gestire i casi limite, e.g. quando uno dei due videogames non esistono
		public static void EditDescrizioneVideogames(long id1, long id2)
		{
			using (SqlConnection connessioneSql = new SqlConnection(stringaDiConnessione))
			{
				connessioneSql.Open();
				using (SqlCommand cmd = connessioneSql.CreateCommand())
				{
					using (SqlTransaction transazione = connessioneSql.BeginTransaction("Transazione1"))
					{
						cmd.Transaction = transazione;
						try
						{
							// Seleziono il videogame da cui estrarre la descrizione
							string query1 = "select overview from videogames where Id = @Id1";
							cmd.Parameters.Add(new SqlParameter("@Id1", id1));
							cmd.CommandText = query1;
							string descrizione = "";
							using (SqlDataReader reader = cmd.ExecuteReader())
							{
								if (reader.Read())
								{
									descrizione = reader.GetString(reader.GetOrdinal("overview"));
								}
							}

							// Sovrascrivo la descrizione del videogame di destinazione
							string query2 = "update videogames set overview = @Descrizione where Id = @Id2";
							cmd.Parameters.Clear();
							cmd.Parameters.Add(new SqlParameter("@Descrizione", descrizione));
							cmd.Parameters.Add(new SqlParameter("@Id2", id2));
							cmd.CommandText = query2;
							int righeModificate = cmd.ExecuteNonQuery();

							// Cancello la descrizione del videogame di origine
							string query3 = "update videogames set overview = '' where Id = @Id1";
							cmd.Parameters.Clear();
							cmd.Parameters.Add(new SqlParameter("@Id1", id1));
							cmd.CommandText = query3;
							cmd.ExecuteNonQuery();

							transazione.Commit();
						}
						catch (Exception ex)
						{
							transazione.Rollback();
						}
					}
				}
			}
		}

		public static Videogame GetVideogame(long id)
		{
			string queryId = $"select * from videogames where Id = {id}";
			try
			{
				using (SqlConnection connessioneSql = new SqlConnection(stringaDiConnessione)) // Devo specificare allo using un campo d'azione, uno scope, tramite le sue parentesi graffe
				{
					try
					{
						using (SqlCommand cmd = new SqlCommand(queryId, connessioneSql))
						{
							connessioneSql.Open(); // Se non apriamo la connessione, non possiamo eseguire il comando!
							SqlDataReader reader = cmd.ExecuteReader();
							if (reader.Read())
							{
								string name = reader.GetString(reader.GetOrdinal("name"));
								string overview = reader.GetString(reader.GetOrdinal("overview"));
								DateTime releaseDate = reader.GetDateTime(reader.GetOrdinal("release_date"));

								Videogame videogame1 = new Videogame();
								videogame1.Id = id;
								videogame1.Name = name;
								videogame1.Overview = overview;
								videogame1.ReleaseDate = releaseDate;

								return videogame1;
							}
						}
						int a = 2;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			return null;
		}
		public static Videogame GetVideogameConParametri(long id)
		{
			// Anziché scrivere
			// select * from videogames where Id = 1
			// Possiamo disaccoppiare il valore del parametro (1) dalla query
			// select * from videogames where Id = @MioId
			// e poi specificare il valore del parametro @MioId tramite un certo oggetto della mia libreria

			string queryId = $"select * from videogames where Id = @MioId";
			try
			{
				using (SqlConnection connessioneSql = new SqlConnection(stringaDiConnessione)) // Devo specificare allo using un campo d'azione, uno scope, tramite le sue parentesi graffe
				{
					try
					{
						using (SqlCommand cmd = new SqlCommand(queryId, connessioneSql))
						{
							cmd.Parameters.Add(new SqlParameter("@MioId", id)); // Aggiungo un parametro alla query
							connessioneSql.Open(); // Se non apriamo la connessione, non possiamo eseguire il comando!
							SqlDataReader reader = cmd.ExecuteReader();
							if (reader.Read())
							{
								string name = reader.GetString(reader.GetOrdinal("name"));
								string overview = reader.GetString(reader.GetOrdinal("overview"));
								DateTime releaseDate = reader.GetDateTime(reader.GetOrdinal("release_date"));

								Videogame videogame1 = new Videogame();
								videogame1.Id = id;
								videogame1.Name = name;
								videogame1.Overview = overview;
								videogame1.ReleaseDate = releaseDate;

								return videogame1;
							}
						}
						int a = 2;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			return null;
		}

		// Solo a scopo didattico mostriamo come scrivere gli using in maniera più breve
		private static Videogame GetVideogameShortUsing(long id)
		{
			string queryId = $"select * from videogames where Id = {id}";
			try
			{
				using SqlConnection connessioneSql = new SqlConnection(stringaDiConnessione);
				try
				{
					using SqlCommand cmd = new SqlCommand(queryId, connessioneSql);
					connessioneSql.Open(); // Se non apriamo la connessione, non possiamo eseguire il comando!
					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.Read())
					{
						string name = reader.GetString(reader.GetOrdinal("name"));
						string overview = reader.GetString(reader.GetOrdinal("overview"));
						DateTime releaseDate = reader.GetDateTime(reader.GetOrdinal("release_date"));

						Videogame videogame1 = new Videogame();
						videogame1.Id = id;
						videogame1.Name = name;
						videogame1.Overview = overview;
						videogame1.ReleaseDate = releaseDate;

						return videogame1;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			return null;
		}

		public static List<Videogame> GetFirst10Videogames()
		{
			List<Videogame> videogames = new List<Videogame>();

			// Popoliamo la lista videogames con i primi 10 videogiochi presenti nel database
			using (SqlConnection connessioneSql = new SqlConnection(stringaDiConnessione))
			{
				try
				{
					connessioneSql.Open();

					string queryVideogames = "select top 10 * from videogames order by Id";

					using (SqlCommand cmd = new SqlCommand(queryVideogames, connessioneSql)) // Creo un oggetto che rappresenta la query nel contesto della mia connession
					{
						// Eseguo la query ottenendo un oggetto che rappresenta il risultato con un cursore interno
						// che posso scorrere per estrarre di volta in volta i valori di ogni riga.
						SqlDataReader reader = cmd.ExecuteReader();
						while (reader.Read()) // Finché è vero che vi è un'altra riga nel risultato... (il metodo incrementa il cursore di una nuova riga e immagazzina dentro reader i dati della riga puntata dal cursorse di volta in volta)
						{
							// Estraiamo i dati della riga puntata dal cursore in questo i-esimo ciclo
							long id = reader.GetInt64(0); // 0 è l'indice della colonna "Id" nella query
							string name = reader.GetString(1); // 1 è l'indice della colonna "Name" nella query
							string overview = reader.GetString(2); // 2 è l'indice della colonna "Overview" nella query

							// Anziché passare "3", uso il metodo GetOrdinal("nome_colonna") per restituirmi l'indice di quella colonna (cioè 3 in questo caso)
							DateTime releaseDate = reader.GetDateTime(reader.GetOrdinal("release_date")); // Esempio di utilizzo di GetDateTime con il nome della colonna

							// Ora posso creare un oggetto Videogame a partire dalle informazioni, dai dati estratti (in questo caso da un DB)
							Videogame videogame = new Videogame();
							videogame.Id = id;
							videogame.Name = name;
							videogame.Overview = overview;
							videogame.ReleaseDate = releaseDate;

							// Aggiungo il Videogame appena creato alla lista di Videogame
							videogames.Add(videogame);
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}

			return videogames;
		}
	}
}
