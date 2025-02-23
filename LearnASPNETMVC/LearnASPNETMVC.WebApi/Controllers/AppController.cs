namespace LearnASPNETMVC.WebApi.Controllers
{
	using System.Web.Http;

	/// <summary>
	/// Контроллер приложения.
	/// </summary>
	public class AppController : ApiController
	{
		/// <summary>
		/// Получает имя пользователя.
		/// </summary>
		/// <returns>Строковое представления пользователя.</returns>
		[HttpGet]
		public string GetUserName()
		{
			return "Bertonipwnz";
		}
	}
}