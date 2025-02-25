namespace BookStore.Controllers
{
	using BookStore.Models;
	using BookStore.Util;
	using System;
	using System.Collections.Generic;
	using System.IO;
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
		/// Метод действия возвращения частичного представления.
		/// </summary>
		public ActionResult Partial()
		{
			ViewBag.Message = "Partial view";
			return PartialView();
		}

		/// <summary>
		/// Отображает страницу.
		/// </summary>
		public ActionResult Index()
		{
			// Извлекаем все книги из контекста базы данных
			IEnumerable<Book> books = _bookContext.Books;

			// Передаем список книг в представление через ViewBag
			ViewBag.Books = books;

			//Установки cookie.
			HttpContext.Response.Cookies["id"].Value = "ca-4353w";

			//Установка сесси.
			Session["name"] = "Tom";
			ViewBag.Message = "Это вызов частичного представления из обычного";

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
		/// Метод действия по получению изображения.
		/// </summary>
		public ActionResult GetImage()
		{
			string path = "../Assets/test.png";
			return new ImageResult(path);
		}

		/// <summary>
		/// Метод действия отправки файла.
		/// </summary>
		public FileResult GetFile()
		{
			// Путь к файлу
			string file_path = Server.MapPath("~/Assets/test.png");
			// Тип файла - content-type
			string file_type = "application/png";
			
			string file_name = "PDFIcon.png";
			return File(file_path, file_type, file_name);
		}

		/// <summary>
		/// Метод действия отправки массива байтов.
		/// </summary>
		public FileResult GetBytes()
		{
			string path = Server.MapPath("~/Assets//test.png");
			byte[] mas = System.IO.File.ReadAllBytes(path);
			string file_type = "application/png";
			string file_name = "PDFIcon.png";
			return File(mas, file_type, file_name);
		}

		/// <summary>
		/// Метод действия отправки потока.
		/// </summary>
		public FileResult GetStream()
		{
			string path = Server.MapPath("~/Assets//test.png");

			FileStream fs = new FileStream(path, FileMode.Open);
			string file_type = "application/png";
			string file_name = "PDFIcon.png";
			return File(fs, file_type, file_name);
		}

		/// <summary>
		/// Получает данные контекста запроса.
		/// </summary>
		public string GetDataContextRequest()
		{
			string browser = HttpContext.Request.Browser.Browser;
			string user_agent = HttpContext.Request.UserAgent;
			string url = HttpContext.Request.RawUrl;
			string ip = HttpContext.Request.UserHostAddress;
			string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
			return "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
				"</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";
		}

		/// <summary>
		/// Получает данные контекста запроса и выдает ответ в виде другой кодировки.
		/// </summary>
		public string GetDataContextRequestWithAnotherCharset()
		{
			HttpContext.Response.Charset = "iso-8859-2";
			HttpContext.Response.Write("<h1>Hello World</h1>");

			string user_agent = HttpContext.Request.UserAgent;
			string url = HttpContext.Request.RawUrl;
			string ip = HttpContext.Request.UserHostAddress;
			string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
			return "<p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
				"</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";
		}

		/// <summary>
		/// Получает данные по пользователю.
		/// </summary>
		public string GetUserData()
		{
			string cookieId = HttpContext.Request.Cookies["id"].Value;
			bool IsAdmin = HttpContext.User.IsInRole("admin"); 
			bool IsAuth = HttpContext.User.Identity.IsAuthenticated; 
			string login = HttpContext.User.Identity.Name;
			string session = (string)Session["name"];
			
			//Удаляем значение сессии.
			Session["name"] = null;

			return "<p>IsAdmin: " + IsAdmin + "</p><p>IsAuth: " + IsAuth +
				"</p><p>login: " + login + "</p><p> Cookie id: " + cookieId + "</p><p> Session: " + session + "</p>";
		}

		/// <summary>
		/// Метод действия по получению контента.
		/// </summary>
		public ViewResult IndexHtml()
		{
			ViewBag.Head = "Привет мир!";
			return View("~/Views/Shared/Error.cshtml");
		}

		/// <summary>
		/// Метод действия временной переадресации.
		/// </summary>
		public RedirectResult TimeRedirect()
		{
			return Redirect("/Home/Index");
		}

		/// <summary>
		/// Метод действия пермаментной переадресации.
		/// </summary>
		/// <remarks>Однако методы RedirectPermanent и RedirectToActionPermanent не 
		/// рекомендуется использовать, а если и использовать, то с осторожностью.
		/// Так как неправильно настроенная постоянная переадресация может 
		/// ухудшить позиции в поисковиках или способствовать полному выпадению сайта из поиска.</remarks>
		public RedirectResult PermanentRedirect()
		{
			return RedirectPermanent("/Home/Index");
		}

		/// <summary>
		/// Метод действия переадресации по определенному маршруту вунтри домена.
		/// </summary>
		public RedirectToRouteResult RedirectToRouteResult()
		{
			return RedirectToRoute(new { controller = "Home", action = "Index" });
		}

		/// <summary>
		/// Метод действия переход к действию контролера.
		/// </summary>
		public RedirectToRouteResult RedirectToAction()
		{
			return RedirectToAction("Square", "Home", new { a = 10, h = 12 });
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
			if (id > 3)
			{
				return Redirect("/Home/Index");
			}

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

		/// <summary>
		/// Вычисляет площадь треугольнка по основанию и высоте.
		/// </summary>
		/// <param name="a">Основание.</param>
		/// <param name="h">Высота.</param>
		public ContentResult Square(int a, int h)
		{
			int s = a * h / 2;
			return Content("<h2>Площадь треугольника с основанием " + a +
					" и высотой " + h + " равна " + s + "</h2>");
		}

		/// <summary>
		/// Метод действия проверки возраста.
		/// </summary>
		/// <param name="age">Возраст.</param>
		public ActionResult CheckAge(int age)
		{
			if (age < 21)
			{
				return new HttpStatusCodeResult(404);
			}

			if (age > 121)
			{
				return HttpNotFound();
			}

			if(age == 25)
			{
				return new HttpUnauthorizedResult();
			}

			return Square(2,2);
		}

		#endregion Public Methods
	}
}