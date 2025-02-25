namespace BookStore.Controllers
{
	using System.Data.Entity;
	using System.Threading.Tasks;
	using System.Net;
	using System.Web.Mvc;
	using BookStore.Models;

	/// <summary>
	/// Контроллер для управления книгами в магазине.
	/// Включает действия для отображения списка книг, деталей книги, создания, редактирования и удаления книг.
	/// </summary>
	public class StoreController : Controller
	{
		#region Private Fields

		/// <summary>
		/// Контекст БД.
		/// </summary>
		private BookContext _db = new BookContext();

		#endregion Private Fields

		#region Public Methods

		/// <summary>
		/// Отображает список всех книг в магазине.
		/// </summary>
		/// <returns>Представление с перечнем всех книг.</returns>
		public async Task<ActionResult> Index()
		{
			return View(await _db.Books.ToListAsync());
		}

		/// <summary>
		/// Отображает подробную информацию о выбранной книге.
		/// </summary>
		/// <param name="id">Идентификатор книги для отображения.</param>
		/// <returns>Представление с подробной информацией о книге.</returns>
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Book book = await _db.Books.FindAsync(id);

			if (book == null)
			{
				return HttpNotFound();
			}

			return View(book);
		}

		/// <summary>
		/// Возвращает форму для создания новой книги.
		/// </summary>
		/// <returns>Представление с формой для добавления книги.</returns>
		public ActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// Обрабатывает запрос на создание новой книги.
		/// </summary>
		/// <param name="book">Модель книги для добавления в базу данных.</param>
		/// <returns>Перенаправление на страницу списка книг, если книга была успешно добавлена.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,Name,Author,Price")] Book book)
		{
			if (ModelState.IsValid)
			{
				_db.Books.Add(book);
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View(book);
		}

		/// <summary>
		/// Возвращает форму для редактирования книги по ее идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор книги для редактирования.</param>
		/// <returns>Представление с формой для редактирования книги.</returns>
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Book book = await _db.Books.FindAsync(id);

			if (book == null)
			{
				return HttpNotFound();
			}

			return View(book);
		}

		/// <summary>
		/// Обрабатывает запрос на редактирование книги.
		/// </summary>
		/// <param name="book">Модель книги с обновленными данными.</param>
		/// <returns>Перенаправление на страницу списка книг, если книга была успешно отредактирована.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Author,Price")] Book book)
		{
			if (ModelState.IsValid)
			{
				_db.Entry(book).State = EntityState.Modified;
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View(book);
		}

		/// <summary>
		/// Возвращает форму для удаления книги по ее идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор книги для удаления.</param>
		/// <returns>Представление с подтверждением удаления книги.</returns>
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Book book = await _db.Books.FindAsync(id);
			if (book == null)
			{
				return HttpNotFound();
			}
			return View(book);
		}

		/// <summary>
		/// Обрабатывает запрос на удаление книги.
		/// </summary>
		/// <param name="id">Идентификатор книги для удаления.</param>
		/// <returns>Перенаправление на страницу списка книг после успешного удаления.</returns>
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Book book = await _db.Books.FindAsync(id);
			_db.Books.Remove(book);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Освобождает ресурсы, используемые контроллером.
		/// </summary>
		/// <param name="disposing">Если значение true, освобождаются все ресурсы, если false — только неуправляемые ресурсы.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_db.Dispose();
			}

			base.Dispose(disposing);
		}

		#endregion Public Methods
	}
}
