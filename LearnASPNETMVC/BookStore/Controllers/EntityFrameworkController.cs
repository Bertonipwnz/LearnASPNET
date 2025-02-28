namespace BookStore.Controllers
{
	using System.Web.Mvc;
	using BookStore.Db;

	/// <summary>
	/// Контроллер по работе с EntityFramework.
	/// </summary>
	public class EntityFrameworkController : Controller
    {
        // GET: EntityFramework
        public ActionResult GetBooks()
        {
            return View(DatabaseEntityFramework.GetBooks());
        }
    }
}
