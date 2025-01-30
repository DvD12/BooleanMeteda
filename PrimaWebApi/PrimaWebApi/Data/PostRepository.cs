using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace PrimaWebApi.Data
{
	public static class PostRepository
	{
		private static string ConnectionString = "Data Source=localhost;Initial Catalog=BlogMeteda;Integrated Security=True;Trust Server Certificate=True";
		public static async Task<List<Post>> GetAllPosts()
		{
			List<Post> TuttiIPost = new List<Post>();

			string query = "SELECT * FROM Posts";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							Post p = ReadPost(reader);
							TuttiIPost.Add(p);

							// TuttiIPost.Add(ReadPost(reader)); // In una singola riga
						}
					}
				}
			}

			return TuttiIPost;
		}

		public static Post ReadPost(SqlDataReader r)
		{
			Post p = new();
			p.Id = r.GetInt32(r.GetOrdinal("Id"));
			p.Title = r.GetString(r.GetOrdinal("Title"));
			p.Content = r.GetString(r.GetOrdinal("Content"));
			p.Author = r.GetString(r.GetOrdinal("Author"));
			return p;
		}

		public static async Task<Post> GetPost(int id)
		{
			Post p = null;

			string query = "SELECT * FROM Posts WHERE Id = @Id";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						if (reader.Read())
						{
							p = ReadPost(reader);
						}
					}
				}
			}

			return p;
		}
	}
}
