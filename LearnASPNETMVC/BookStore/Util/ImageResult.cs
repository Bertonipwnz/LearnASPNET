namespace BookStore.Util
{
	using System.Web.Mvc;

	/// <summary>
	/// Результат действий по получению изображения.
	/// </summary>
	public class ImageResult : ActionResult
	{
		#region Private Fields

		/// <summary>
		/// Путь к изображению.
		/// </summary>
		private string _path;

		#endregion Private Fields

		#region Public Constructors

		/// <summary>
		/// Создает экземпляр <see cref="ImageResult"/>
		/// </summary>
		/// <param name="path">Путь к изображению.</param>
		public ImageResult(string path)
		{
			_path = path;
		}

		#endregion Public Constructors

		#region Public Methods

		/// <summary>
		/// Выполнение результата действия.
		/// </summary>
		/// <param name="context">Информация о текущем запросе.</param>
		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.Write("<div style='width:100%;text-align:center;'>" +
				"<img style='max-width:600px;' src='" + _path + "' /></div>");
		}

		#endregion Public Methods
	}
}