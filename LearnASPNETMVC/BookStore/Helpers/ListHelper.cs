namespace BookStore.Helpers
{
	using System.Web.Mvc;

	/// <summary>
	/// Хелпер по работе со списками.
	/// </summary>
	public static class ListHelper
	{
		/// <summary>
		/// Создает список.
		/// </summary>
		/// <param name="html">Html хелпер.</param>
		/// <param name="items">Элементы.</param>
		/// <returns>Строка Html.</returns>
		public static MvcHtmlString CreateList(this HtmlHelper html, string[] items)
		{
			TagBuilder ul = new TagBuilder("ul");
			foreach (string item in items)
			{
				TagBuilder li = new TagBuilder("li");
				li.SetInnerText(item);
				ul.InnerHtml += li.ToString();
			}
			return new MvcHtmlString(ul.ToString());
		}
	}
}