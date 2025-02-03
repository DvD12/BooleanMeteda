using System.Diagnostics;
using BlogMetedaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogMetedaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Post p = new Post();
            p.Title = "Pinco Pallo IV";
            p.Content = "Ma quant'è bello MVC";

			// Default: cerca una view chiamata "Index.cshtml" nella cartella  "Views/Home"
            // (così come si chiama l'action e il controller)
			return View(p); // Passo il modello alla view
		}

		[HttpGet]
		public IActionResult CreatePost()
		{
			return View();
		}

        [HttpPost]
        public IActionResult CreatePost(Post p) // Il dato viene dalla form
        {
            // Posso usare il modello fornitomi dalla form data da GET CreatePost()
            // per fare quello che voglio, come salvare i dati in un database
            Console.WriteLine($"{p.Title} {p.Content}");

            // Devo restituire una vista: posso restituire quella che verrebbe associata a questo nome (CreatePost) OPPURE effettuo un redirect: svolgo il codice di un'action diversa
            // return View();
            return RedirectToAction("Index");
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
