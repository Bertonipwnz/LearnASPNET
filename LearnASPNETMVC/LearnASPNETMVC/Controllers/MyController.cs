namespace LearnASPNETMVC.Controllers
{
	using System.Web.Mvc;
	using System.Web.Routing;

	/// <summary>
	/// Кастомный контроллер.
	/// </summary>
	public class MyController : IController
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public void Execute(RequestContext requestContext)
		{
			var ip = requestContext.HttpContext.Request.UserHostAddress;
			var response = requestContext.HttpContext.Response;

			response.Write("<h2> Your IP Adress: " + ip + "</h2>");
		}
	}
}