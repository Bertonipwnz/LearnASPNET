namespace LearnASPNETMVC.Controllers
{
	using Models;
	using System.Net.Http;
	using System.Threading.Tasks;
	using System.Web;
	using System.Web.Mvc;

	/// <summary>
	/// Контроллер представления view - home.
	/// </summary>
	public class HomeController : Controller
	{
		#region Public Methods

		public async Task<ActionResult> Index()
		{
			User user = await GetUserAsync();

			return View(user);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		#endregion Public Methods

		#region Private Methods

		/// <summary>
		/// Получает экземпляр пользователя.
		/// </summary>
		/// <returns>Модель пользователя.</returns>
		private async Task<User> GetUserAsync()
		{
			User user = new User();

			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new System.Uri("https://localhost:44330/");

				HttpResponseMessage response = await client.GetAsync("api/app/GetUserName");

				user.UserName = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
			};

			return user;
		}
			
		#endregion Private Methods
	}
}