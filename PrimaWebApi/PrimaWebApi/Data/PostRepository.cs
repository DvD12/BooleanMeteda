using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace PrimaWebApi.Data
{
	public class PostRepository
	{
		private string ConnectionString = "Data Source=localhost;Initial Catalog=BlogMeteda;Integrated Security=True;Trust Server Certificate=True";
		public async Task<List<Post>> GetAllPosts()
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

		public Post ReadPost(SqlDataReader r)
		{
			Post p = new();
			p.Id = r.GetInt32(r.GetOrdinal("Id"));
			//p.Title = r.GetString(r.GetOrdinal("Title"));
			p.Content = r.GetString(r.GetOrdinal("Content"));
			p.Author = r.GetString(r.GetOrdinal("Author"));
			return p;
		}

		public async Task<Post> GetPost(int id)
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

		public async Task<List<Post>> GetPostsByTitle(string title)
		{
			List<Post> posts = new List<Post>();
			string query = "SELECT * FROM Posts WHERE Title LIKE @Title";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Title", $"%{title}%");
					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							posts.Add(ReadPost(reader));
						}
					}
				}
			}
			return posts;
		}

		public async Task<int> CreatePost(Post p)
		{
			string query = "INSERT INTO Posts (Title, Content, Author) VALUES (@Title, @Content, @Author)";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Title", p.Title);
					command.Parameters.AddWithValue("@Content", p.Content);
					command.Parameters.AddWithValue("@Author", p.Author ?? ""); // ?? è un operatore di null-coalescing: se il valore alla sinistra è null, restituisce il valore alla destra; altrimenti restituisce il valore alla sinistra
					return await command.ExecuteNonQueryAsync();
				}
			}
		}

		/* Se voglio usare una tupla per restituire sia il numero di righe coinvolte che il post appena creato:
		public async Task<(int AffectedRows, Post newPost)> CreatePost(Post p)
		{
			string query = "INSERT INTO Posts (Title, Content, Author) VALUES (@Title, @Content, @Author)";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Title", p.Title);
					command.Parameters.AddWithValue("@Content", p.Content);
					command.Parameters.AddWithValue("@Author", p.Author ?? ""); // ?? è un operatore di null-coalescing: se il valore alla sinistra è null, restituisce il valore alla destra; altrimenti restituisce il valore alla sinistra
					return (await command.ExecuteNonQueryAsync(), p);
				}
			}
		}
		*/

		public async Task<int> UpdatePost(int id, Post p)
		{
			string query = "UPDATE Posts SET Title = @Title, Content = @Content, Author = @Author WHERE Id = @Id";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					command.Parameters.AddWithValue("@Title", p.Title);
					command.Parameters.AddWithValue("@Content", p.Content);
					command.Parameters.AddWithValue("@Author", p.Author ?? "");
					return await command.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task<int> DeletePost(int id)
		{
			string query = "DELETE FROM Posts WHERE Id = @Id";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					return await command.ExecuteNonQueryAsync();
				}
			}
		}
	}
}
