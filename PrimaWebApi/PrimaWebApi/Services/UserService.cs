using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using PrimaWebApi.Data;

namespace PrimaWebApi.Services
{
	public class UserService
	{
		public const string CONNECTION_STRING = "Data Source=localhost;Initial Catalog=BlogMeteda;Integrated Security=True;Trust Server Certificate=True";

		private readonly IPasswordHasher<UserModel> _pswHasher;

		public UserService(IPasswordHasher<UserModel> pswHasher)
		{
			_pswHasher = pswHasher;
		}

		public async Task<bool> RegisterAsync(UserModel user)
		{
			var passwordHash = _pswHasher.HashPassword(user, user.Password);

			using var conn = new SqlConnection(CONNECTION_STRING);
			await conn.OpenAsync();

			var query = "INSERT INTO Users (Email, PasswordHash) VALUES (@Email, @PasswordHash)";
			using (SqlCommand cmd = new SqlCommand(query, conn))
			{
				cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
				cmd.Parameters.Add(new SqlParameter("@PasswordHash", passwordHash));
				return await cmd.ExecuteNonQueryAsync() > 0; // Ritornerà 1 se l'utente è stato inserito (cioè se la registrazione ha avuto successo)
			}
		}
		public async Task<User> AuthenticateAsync(string email, string password) // la password in input è data dal client: è in chiaro
		{
			var query = "SELECT * FROM Users WHERE Email = @Email";

			using var conn = new SqlConnection(CONNECTION_STRING);
			await conn.OpenAsync();
			using SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.Add(new SqlParameter("@Email", email));
			using SqlDataReader reader = await cmd.ExecuteReaderAsync();

			if (await reader.ReadAsync())
			{
				var id = reader.GetInt32(reader.GetOrdinal("Id"));
				var passwordHash = reader.GetString(reader.GetOrdinal("PasswordHash")); // l'hash che noi abbiamo precedentemente salvato in DB
				var model = new UserModel() { Email = email, Password = password };
				// VerifyHashedPassword verifica che l'hash che noi abbiamo salvato in DB corrisponda all'hash della password in input (che è in chiaro)
				// Per esempio: la password hash salvata è XYZWC
				// La password in chiaro è 12345
				// Questa funzione tenterà di hashare 12345 e verificare che il risultato sia proprio XYZWC, cioè il dato che noi abbiamo salvato in DB
				if (_pswHasher.VerifyHashedPassword(model, passwordHash, password) != PasswordVerificationResult.Success)
				{
					return null;
				}
				return new User() { Id = id, Email = email };
			}
			return null;
		}

		public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
		{
			var roles = new List<string>();

			using (var connection = new SqlConnection(CONNECTION_STRING))
			{
				await connection.OpenAsync();

				var command = new SqlCommand(
				"SELECT r.Name " +
				"FROM Roles r " +
				"INNER JOIN UserRoles ur ON r.Id = ur.RoleId " +
				"WHERE ur.UserId = @UserId", connection);
				command.Parameters.AddWithValue("@UserId", userId);
				var reader = await command.ExecuteReaderAsync();

				while (await reader.ReadAsync())
				{
					roles.Add(reader.GetString(0));
				}
			}

			return roles;
		}
	}
}
