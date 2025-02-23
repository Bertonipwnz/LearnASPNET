namespace LearnASPNETMVC.Controllers
{
	using LearnASPNETMVC.Models.Db;
	using Models;
	using System.Data.Entity;
	using System.Net.Http;
	using System.Threading.Tasks;
	using System.Web.Mvc;

	/// <summary>
	/// Контроллер представления view - home.
	/// </summary>
	public class HomeController : Controller
	{
		#region Public Methods

		public async Task<ActionResult> Index()
		{
			string userName = await GetUserNameAsync();

			User user = await GetUserFromNameAsync(userName);

			if(user == null)
			{
				user = await AddUserAsync(new User
				{
					UserName = userName,
				});
			}

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

		[HttpPost]
		public int CalculateSquare(int a, int h)
		{
			return CalculateTriangleSquare(a, h);
		}

		#endregion Public Methods

		#region Private Methods

		/// <summary>
		/// Добавление пользователя в таблицу.
		/// </summary>
		/// <param name="user">Модель пользователя.</param>
		/// <returns>Модель добавленного пользователя.</returns>
		private async Task<User> AddUserAsync(User user)
		{
			using (var appContext = new AppContext())
			{
				appContext.Users.Add(user);
				await appContext.SaveChangesAsync();
			}

			return user;
		}

		/// <summary>
		/// Получает имя пользователя.
		/// </summary>
		/// <returns>Имя пользователя.</returns>
		private async Task<string> GetUserNameAsync()
		{
			string userName = string.Empty;

			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new System.Uri("https://localhost:44330/");

				HttpResponseMessage response = await client.GetAsync("api/app/GetUserName");

				userName = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
			};

			return userName;
		}

		/// <summary>
		/// Получает пользователя из базы по имени.
		/// </summary>
		/// <param name="userName">Имя пользователя.</param>
		/// <returns>Модель пользователя.</returns>
		private async Task<User> GetUserFromNameAsync(string userName)
		{
			User userModel = null;

			using (var appContext = new AppContext())
			{
				userModel = await appContext.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
			}

			return userModel;
		}

		private int CalculateTriangleSquare(int a, int h)
		{
			return (a * h) / 2;
		}
			
		#endregion Private Methods
	}
}