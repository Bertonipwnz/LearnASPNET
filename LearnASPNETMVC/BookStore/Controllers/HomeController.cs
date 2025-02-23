namespace BookStore.Controllers
{
	using BookStore.Models;
	using System.Collections.Generic;
	using System.Web.Mvc;

	/// <summary>
	/// Контроллер Home.
	/// </summary>
	public class HomeController : Controller
	{
		#region Private Fields

		/// <summary>
		/// Создает экземпляр контекста.
		/// </summary>
		private BookContext _bookContext = new BookContext();

		#endregion Private Fields

		#region Public Methods

		public ActionResult Index()
		{
			IEnumerable<Book> books = _bookContext.Books;
			ViewBag.Books = books;

			return View();
		}

		#endregion Public Methods
	}
}