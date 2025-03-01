﻿namespace BookStore.Controllers
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

		public ActionResult GetPackPrice(int? idBook)
		{
			if (!idBook.HasValue)
			{
				return Content("00.00");
			}

			return Content(DatabaseViaCommand.GetPackPriceOnBook(idBook.Value).ToString());
		}
    }
}
