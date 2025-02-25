namespace BookStore.Helpers
{
	using System.Web.Mvc;

	/// <summary>
	/// Хелпер по работе с изображениями
	/// </summary>
	public static class ImageHelper
	{
		/// <summary>
		/// Создает изображение.
		/// </summary>
		public static MvcHtmlString Image(this HtmlHelper html, string src, string alt)
		{
			TagBuilder img = new TagBuilder("img");
			img.MergeAttribute("src", src);
			img.MergeAttribute("alt", alt);

			return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
		}
	}
}