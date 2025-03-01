namespace BookStore.Controllers
{
	using BookStore.Db.Contexts;
	using System.Web.Mvc;

	/// <summary>
	/// Контроллер по работе с MySql.
	/// </summary>
	public class MySqlCommandController : Controller
    {
		/// <summary>
		/// Получает книги.
		/// </summary>
		public ActionResult GetBooks()
        {
            return View(DatabaseViaCommand.GetBooks());
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

			return Content(DatabaseViaCommand.GetPackPriceOnBook(idBook.Value).ToString());
		}

		public ActionResult GetNewPrice(int? idBook)
		{
			if (!idBook.HasValue)
			{
				return Content("00.00");
			}

			return Content(DatabaseViaCommand.GetNewPriceOnBook(idBook.Value).ToString());
		}
		
		public ActionResult GetNewPriceOnSaleAuthor(int? idBook)
		{
			if (!idBook.HasValue)
			{
				return Content("00.00");
			}

			return Content(DatabaseViaCommand.GetNewPriceWithSale(idBook.Value).ToString());
		}

    }
}
