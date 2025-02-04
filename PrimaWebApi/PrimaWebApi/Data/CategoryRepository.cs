using Microsoft.Data.SqlClient;

namespace PrimaWebApi.Data
{
	public class CategoryRepository
	{
		private string CONNECTION_STRING = "Data Source=localhost;Initial Catalog=BlogMeteda;Integrated Security=True;Trust Server Certificate=True";
		private PostRepository PostRepository { get; set; }

		public CategoryRepository(PostRepository postRepository)
		{
			PostRepository = postRepository;
		}

		public async Task<List<Category>> GetAllCategories()
		{
			var query = "SELECT * FROM Categories";
			using var conn = new SqlConnection(CONNECTION_STRING);
			await conn.OpenAsync();

			List<Category> Categories = new List<Category>();
			using (SqlCommand cmd = new SqlCommand(query, conn))
			{
				using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						Categories.Add(GetCategoryFromData(reader));
					}
				}
			}

			return Categories;
		}

		public async Task<int> DeleteCategory(int id)
		{
			// Step 1: Rimuovi tutte le dipendenze da questa categoria
			// in modo che l'eliminazione non violi le chiavi esterne e abbia successo
			await PostRepository.OnCategoryDelete(id);

			// Step 2: Elimina la categoria da DB
			var query = "DELETE FROM Categories WHERE Id = @id";
			using var conn = new SqlConnection(CONNECTION_STRING);
			await conn.OpenAsync();
			using (SqlCommand cmd = new SqlCommand(query, conn))
			{
				cmd.Parameters.AddWithValue("@id", id);
				return await cmd.ExecuteNonQueryAsync();
			}
		}

		private Category GetCategoryFromData(SqlDataReader reader)
		{
			var id = reader.GetInt32(reader.GetOrdinal("Id"));
			var name = reader.GetString(reader.GetOrdinal("Name"));
			var Category = new Category(id, name);
			return Category;
		}
	}
}
