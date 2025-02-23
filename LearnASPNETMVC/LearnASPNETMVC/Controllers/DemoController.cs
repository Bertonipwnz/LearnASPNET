namespace LearnASPNETMVC.Controllers
{
	using System.Web.Mvc;

    /// <summary>
    /// Пусто демоконтроллер.
    /// </summary>
	public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }
    }
}