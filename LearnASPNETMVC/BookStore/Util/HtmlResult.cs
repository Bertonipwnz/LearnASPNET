namespace BookStore.Util
{
	using System.Web.Mvc;

	/// <summary>
	/// Результат действий по получению HTML.
	/// </summary>
	public class HtmlResult : ActionResult
	{
		#region Private Fields

		/// <summary>
		/// Строка html кода.
		/// </summary>
		private string _htmlCode;

		#endregion Private Fields

		#region Public Constructors

		/// <summary>
		/// Создает экземпляр <see cref="HtmlResult"/>
		/// </summary>
		/// <param name="html">Html строка.</param>
		public HtmlResult(string html)
		{
			_htmlCode = html;
		}

		#endregion Public Constructors

		#region Public Methods

		/// <summary>
		/// Выполнение результата действия.
		/// </summary>
		/// <param name="context">Информация о текущем запросе.</param>
		public override void ExecuteResult(ControllerContext context)
		{
			string fullHtmlCode = "<!DOCTYPE html><html><head>";
			fullHtmlCode += "<title>Главная страница</title>";
			fullHtmlCode += "<meta charset=utf-8 />";
			fullHtmlCode += "</head> <body>";
			fullHtmlCode += _htmlCode;
			fullHtmlCode += "</body></html>";
			context.HttpContext.Response.Write(fullHtmlCode);
		}

		#endregion Public Methods
	}
}