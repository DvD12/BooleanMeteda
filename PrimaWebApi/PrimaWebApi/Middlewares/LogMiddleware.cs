namespace PrimaWebApi.Middlewares
{

	// Request arrivata 1
	// Request arrivata 2
	// API!!!!!
	// Response inviata 2
	// Response inviata 1

	public class LogMiddleware
	{
		// Rappresenta il middleware successivo nella pipeline
		private readonly RequestDelegate _next;

		public LogMiddleware(RequestDelegate next)
		{
			this._next = next;
		}

		// Questo metodo è chiamato ogni volta che un client fa una richiesta al nostro server
		public async Task InvokeAsync(HttpContext context) // context ha info riguardanti la richiesta HTTP inoltrataci
		{
			// PRIMA di invocare il prossimo passo della pipeline, posso fare quello che voglio
			// Qui intercetto il contesto e la sua richiesta per loggarle in console
			Console.WriteLine($"Request arrivata (middleware 1): {context.Request.Method} {context.Request.Path}");

			// Questo è necessario!
			await _next(context); // Passo la richiesta al prossimo middleware passandogli anche il contesto

			// DOPO aver invocato il prossimo passo della pipeline, posso fare quello che voglio
			Console.WriteLine($"Response inviata (middleware 1): {context.Response.StatusCode}");
		}
	}
	public class LogMiddleware2
	{
		// Rappresenta il middleware successivo nella pipeline
		private readonly RequestDelegate _next;

		public LogMiddleware2(RequestDelegate next)
		{
			this._next = next;
		}

		// Questo metodo è chiamato ogni volta che un client fa una richiesta al nostro server
		public async Task InvokeAsync(HttpContext context) // context ha info riguardanti la richiesta HTTP inoltrataci
		{
			// PRIMA di invocare il prossimo passo della pipeline, posso fare quello che voglio
			// Qui intercetto il contesto e la sua richiesta per loggarle in console
			Console.WriteLine($"Request arrivata (middleware 2): {context.Request.Method} {context.Request.Path}");

			// Questo è necessario!
			await _next(context); // Passo la richiesta al prossimo middleware passandogli anche il contesto

			// DOPO aver invocato il prossimo passo della pipeline, posso fare quello che voglio
			Console.WriteLine($"Response inviata (middleware 2): {context.Response.StatusCode}");
		}
	}
}
