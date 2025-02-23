namespace BookStore.Models
{
	using System.Data.Entity;
	
	/// <summary>
	/// Контекст данных для книг.
	/// </summary>
	public class BookContext :DbContext
	{
		/// <summary>
		/// Таблица книг.
		/// </summary>
		public DbSet<Book> Books { get; set; }

		/// <summary>
		/// Таблица покупок.
		/// </summary>
		public DbSet<Purchase> Purchases { get; set; }
	}
}