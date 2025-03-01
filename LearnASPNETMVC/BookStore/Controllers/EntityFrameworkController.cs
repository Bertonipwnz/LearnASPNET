namespace BookStore.Controllers
{
	using System.Web.Mvc;
	using BookStore.Db;

	/// <summary>
	/// Контроллер по работе с EntityFramework.
	/// </summary>
	public class EntityFrameworkController : Controller
    {
        /// <summary>
        /// Получает книги.
        /// </summary>
        public ActionResult GetBooks()
        {
            return View(DatabaseEntityFramework.GetBooks());
        }

		/// <summary>
		/// Получает цену для упаковки.
		/// </summary>
		/// <param name="idBook">Айди книги.</param>
		public ActionResult GetPackPrice(int? idBook)
		{
			if (!idBook.HasValue)
			{
				return Content("00.00");
			}

			return Content(DatabaseEntityFramework.GetPackPriceOnBook(idBook.Value).ToString());
		}
	}
}
