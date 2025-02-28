namespace BookStore.Models
{
	/// <summary>
	/// Модель книги.
	/// </summary>
	public class Book
	{
		/// <summary>
		/// Айди.
		/// </summary>
		public int BookId { get; set; }

		/// <summary>
		/// Наименование.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Автор.
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// Цена.
		/// </summary>
		public float Price { get; set; }

		/// <summary>
		/// Количество.
		/// </summary>
		public int Amount { get; set; }
	}
}