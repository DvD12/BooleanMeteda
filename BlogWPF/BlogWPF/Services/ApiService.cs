using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlogWPF.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BlogWPF.Services
{
	public enum ApiServiceResultType
	{
		Success,
		Error
	}
	public static class ApiService
	{
		public const string API_URL = "https://localhost:7190";
		public static string Email { get; set; }
		public static string Password { get; set; }
		public static async Task<ApiServiceResult<bool>> Register()
		{
			try
			{
				using HttpClient client = new HttpClient();
				var httpResult = await client.PostAsync($"{API_URL}/Account/Register",
					JsonContent.Create(new { Email = Email, Password = Password }));
				var resultBody = await httpResult.Content.ReadAsStringAsync();
				var data = httpResult.IsSuccessStatusCode;
				return new ApiServiceResult<bool>(data);
			}
			catch (Exception e)
			{
				return new ApiServiceResult<bool>(e);
			}
		}

		public static async Task<ApiServiceResult<Jwt>> GetJwtToken()
		{
			try
			{
				using HttpClient client = new HttpClient();
				// PostAsync() prende due parametri: URL e body della richesta
				var httpResult = await client.PostAsync($"{API_URL}/Account/Login", JsonContent.Create(new { Email = Email, Password = Password }));
				// Se la chiamata ha successo avremo in resultBody il JSON del body della risposta HTTP: in questo caso { token: ..., expirationUtc: ... }
				var resultBody = await httpResult.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Jwt>(resultBody);
				if (data.Token == null)
				{
					return new ApiServiceResult<Jwt>(new Exception("Login fallito"));
				}
				AddRolesToJwt(data);
				return new ApiServiceResult<Jwt>(data);
			}
			catch (Exception e)
			{
				return new ApiServiceResult<Jwt>(e);
			}
		}
		private static void AddRolesToJwt(Jwt jwt)
		{
			try
			{
				// Decodifichiamo il JWT
				var handler = new JwtSecurityTokenHandler();
				var jwtToken = handler.ReadJwtToken(jwt.Token);

				// Vediamo se ci sono ruoli nel JWT
				var roles = jwtToken.Claims
					.Where((Claim c) => c.Type == "role")
					.Select(c => c.Value).ToList();

				// Aggiungiamoli nel nostro DTO (data transfer object) rappresentante il JWT
				jwt.Roles = roles;
			}
			catch { } // Se succede qualcosa non facciamo nulla
		}

		public static bool MiaFunzione(int x)
		{
			return x > 5;
		}

		public static async Task<ApiServiceResult<List<Post>>> GetPosts()
		{
			try
			{
				using HttpClient client = new HttpClient();
				var httpResult = await client.GetAsync($"{API_URL}/Posts");
				var resultBody = await httpResult.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<List<Post>>(resultBody);
				return new ApiServiceResult<List<Post>>(data);
			}
			catch (Exception e)
			{
				return new ApiServiceResult<List<Post>>(e);
			}
		}

		/// <summary>
		/// Richiama l'API per creare un post e ne ritorna, in caso di successo, un interno che rappresenta l'ID del nuovo post
		/// </summary>
		/// <param name="newPost"></param>
		/// <param name="jwt"></param>
		/// <returns></returns>
		public static async Task<ApiServiceResult<int>> CreatePost(Post newPost, Jwt jwt)
		{
			try
			{
				using HttpClient httpClient = new HttpClient();
				// Devo aggiungere il JWT
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Token);

				var httpResult = await httpClient.PostAsync($"{API_URL}/Posts", JsonContent.Create(newPost));
				var resultBody = await httpResult.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<int>(resultBody); // L'API ritorna un intero che rappresenta il nuovo ID del post
				return new ApiServiceResult<int>(data);
			}
			catch (Exception e)
			{
				return new ApiServiceResult<int>(e);
			}
		}
		/// <summary>
		/// Richiamo l'API per aggiornare un post. Ritorna il numero di righe interessate
		/// </summary>
		public static async Task<ApiServiceResult<int>> UpdatePost(Post post, Jwt token)
		{
			try
			{
				using HttpClient httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

				var httpResult = await httpClient.PutAsync($"{API_URL}/Posts/{post.Id}", JsonContent.Create(post));
				var resultBody = await httpResult.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<int>(resultBody);
				return new ApiServiceResult<int>(data);
			}
			catch (Exception e)
			{
				return new ApiServiceResult<int>(e);
			}
		}
		public static async Task<ApiServiceResult<int>> DeletePost(int postId, Jwt token)
		{
			try
			{
				using HttpClient httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

				var httpResult = await httpClient.DeleteAsync($"{API_URL}/Posts/{postId}");
				var resultBody = await httpResult.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<int>(resultBody);
				return new ApiServiceResult<int>(data);
			}
			catch (Exception e)
			{
				return new ApiServiceResult<int>(e);
			}
		}
	}
}
