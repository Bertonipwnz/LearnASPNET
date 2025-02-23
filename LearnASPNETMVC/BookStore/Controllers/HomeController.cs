namespace BookStore.Controllers
{
	using BookStore.Models;
	using BookStore.Util;
	using System;
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

		/// <summary>
		/// Отображает страницу.
		/// </summary>
		public ActionResult Index()
		{
			// Извлекаем все книги из контекста базы данных
			IEnumerable<Book> books = _bookContext.Books;

			// Передаем список книг в представление через ViewBag
			ViewBag.Books = books;

			return View();
		}

		/// <summary>
		/// Метод действия по получению HTML.
		/// </summary>
		public ActionResult GetHtml()
		{
			return new HtmlResult("<h2>Привет мир!</h2>");
		}

		/// <summary>
		/// Отображает страницу покупки книги,
		/// используется для отображения страницы с конкретной книгой по её идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор книги, выбранной для покупки.</param>
		/// <returns>Представление с информацией о книге для покупки.</returns>
		[HttpGet]
		public ActionResult Buy(int id)
		{
			// Сохраняем идентификатор книги в ViewBag для передачи в представление
			ViewBag.BookId = id;

			return View();
		}

		/// <summary>
		/// Обрабатывает покупку книги,
		/// сохраняет информацию о покупке в базе данных и возвращает сообщение об успешной покупке.
		/// </summary>
		/// <param name="purchase">Информация о покупке, включающая данные покупателя и книги.</param>
		/// <returns>Сообщение, подтверждающее успешную покупку.</returns>
		[HttpPost]
		public string Buy(Purchase purchase)
		{
			// Устанавливаем дату покупки
			purchase.Date = DateTime.Now;

			// Добавляем покупку в контекст базы данных
			_bookContext.Purchases.Add(purchase);

			// Сохраняем изменения в базе данных
			_bookContext.SaveChanges();

			// Возвращаем сообщение с благодарностью
			return "Спасибо," + purchase.Person + ", за покупку!";
		}

		#endregion Public Methods
	}
}