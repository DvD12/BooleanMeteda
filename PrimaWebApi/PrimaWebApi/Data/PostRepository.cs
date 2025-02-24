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
			Dictionary<int, Post> posts = new Dictionary<int, Post>();

			string query = @"SELECT p.*, c.Id AS CategoryId, c.Name AS CategoryName, t.Id AS TagId, t.Name AS TagName
							 FROM Posts p
						     LEFT JOIN Categories c ON p.CategoryId = c.Id
							 LEFT JOIN PostTag pt ON p.Id = pt.PostId
						     LEFT JOIN Tags t ON pt.TagId = t.Id";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							ReadPost(reader, posts);
						}
					}
				}
			}
			foreach (Post p in posts.Values)
			{
				TuttiIPost.Add(p);
			}
			return TuttiIPost;
		}

		public void ReadPost(SqlDataReader r, Dictionary<int, Post> posts)
		{
			try
			{
				int id = r.GetInt32(r.GetOrdinal("Id"));

				Post p = new Post();
				if (posts.ContainsKey(id) == false)
				{
					p.Id = id;
					p.Title = r.GetString(r.GetOrdinal("Title"));
					p.Content = r.GetString(r.GetOrdinal("Content"));
					p.Author = r.GetString(r.GetOrdinal("Author"));
					if (r.IsDBNull(r.GetOrdinal("CategoryId")) == false)
					{
						int categoryId = r.GetInt32(r.GetOrdinal("CategoryId"));
						string categoryName = r.GetString(r.GetOrdinal("CategoryName"));
						Category c = new Category();
						c.Id = categoryId;
						c.Name = categoryName;
						p.CategoryId = c.Id;
						p.Category = c;
					}
					posts.Add(id, p);
				}
				p = posts[id];
				int tagId = r.GetInt32(r.GetOrdinal("TagId"));
				p.TagIds.Add(tagId);
				Tag t = new Tag();
				t.Id = tagId;
				t.Name = r.GetString(r.GetOrdinal("TagName"));
				p.Tags.Add(t);
			}
			catch (Exception e)
			{
				
			}
		}

		public async Task<Post> GetPost(int id)
		{
			Dictionary<int, Post> post = new Dictionary<int, Post>();

			string query = @"SELECT p.*, c.Id AS CategoryId, c.Name AS CategoryName, t.Id AS TagId, t.Name AS TagName
							 FROM Posts p
						     LEFT JOIN Categories c ON p.CategoryId = c.Id
							 LEFT JOIN PostTag pt ON p.Id = pt.PostId
						     LEFT JOIN Tags t ON pt.TagId = t.Id
							 WHERE p.Id = @Id";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						while (reader.Read())
						{
							ReadPost(reader, post);
						}
					}
				}
			}

			return post.Values.FirstOrDefault();
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
							//posts.Add(ReadPost(reader)); // TODO: da sistemare
						}
					}
				}
			}
			return posts;
		}

		/// <summary>
		/// Crea un post e restituisci l'ID del post appena inserito
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public async Task<int> CreatePost(Post p)
		{
			string query = @"INSERT INTO Posts (Title, Content, Author, CategoryId) VALUES (@Title, @Content, @Author, @CategoryId);
						     SELECT SCOPE_IDENTITY();";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					string pluto = "";

					object pippo = pluto ?? "asd";

					command.Parameters.AddWithValue("@Title", p.Title);
					command.Parameters.AddWithValue("@Content", p.Content);
					command.Parameters.AddWithValue("@Author", p.Author ?? ""); // ?? è un operatore di null-coalescing: se il valore alla sinistra è null, restituisce il valore alla destra; altrimenti restituisce il valore alla sinistra
					command.Parameters.AddWithValue("@CategoryId", p.CategoryId ?? (object)DBNull.Value);
					int postId = Convert.ToInt32(await command.ExecuteScalarAsync());

					await HandleTags(p.TagIds, postId);

					return postId;
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
					int rowsAffected = await command.ExecuteNonQueryAsync();

					await HandleTags(p.TagIds, id);

					return rowsAffected;
				}
			}
		}

		public async Task<int> DeletePost(int id)
		{
			await ClearPostTags(id); // Prima di eliminare il post, rimuoviamo i tag associati
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

		public async Task<int> OnCategoryDelete(int categoryId)
		{
			string query = "UPDATE Posts SET CategoryId = NULL WHERE CategoryId = @CategoryId";
			using (var connection = new SqlConnection(ConnectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@CategoryId", categoryId);
					return await command.ExecuteNonQueryAsync();
				}
			}
		}

		private async Task<int> ClearPostTags(int postId)
		{
			using var conn = new SqlConnection(ConnectionString);
			await conn.OpenAsync();

			var query = $"DELETE FROM PostTag WHERE PostId = @id";
			using (SqlCommand cmd = new SqlCommand(query, conn))
			{
				cmd.Parameters.Add(new SqlParameter("@id", postId));
				return await cmd.ExecuteNonQueryAsync();
			}
		}

		private async Task<int> AddPostTags(int postId, List<int> tags)
		{
			using var conn = new SqlConnection(ConnectionString);
			await conn.OpenAsync();
			int inserted = 0;
			foreach (int tagId in tags)
			{
				var insertTagQuery
					= $"INSERT INTO PostTag (PostId, TagId) "
					+ $"VALUES (@postId, @tagId)";
				using (SqlCommand cmd =
					new SqlCommand(insertTagQuery, conn))
				{
					cmd.Parameters.Add(new SqlParameter("@postId", postId));
					cmd.Parameters.Add(new SqlParameter("@tagId", tagId));
					inserted += await cmd.ExecuteNonQueryAsync();
				}
			}
			return inserted;
		}
		private async Task HandleTags(List<int> tags, int postId)
		{
			if (tags == null)
				return;

			// Rimuoviamo i tag relativi a questo post
			await ClearPostTags(postId);

			// Inseriamo i nuovi tag
			await AddPostTags(postId, tags);
		}
	}
}
