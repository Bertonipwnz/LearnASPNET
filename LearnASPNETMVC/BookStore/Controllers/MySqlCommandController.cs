namespace BookStore.Controllers
{
	using BookStore.Db.Contexts;
	using System.Web.Mvc;

	/// <summary>
	/// Контроллер по работе с MySql.
	/// </summary>
	public class MySqlCommandController : Controller
    {
        // GET: MySql
        public ActionResult GetBooks()
        {
            return View(DatabaseViaCommand.GetBooks());
        }
    }
}
