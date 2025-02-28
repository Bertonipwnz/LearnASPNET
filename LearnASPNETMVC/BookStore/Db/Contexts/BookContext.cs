namespace BookStore.Models
{
	using System.Data.Entity;

	/// <summary>
	/// Контекст данных для книг.
	/// </summary>
	public class BookContext : DbContext
	{
		#region Public Properties

		/// <summary>
		/// Таблица книг.
		/// </summary>
		public DbSet<Book> Books { get; set; }

		/// <summary>
		/// Таблица покупок.
		/// </summary>
		public DbSet<Purchase> Purchases { get; set; }

		#endregion Public Properties

		#region Public Constructors

		/// <summary>
		/// Создает экземпляр <see cref="AppContext"/>
		/// </summary>
		public BookContext() : base("BookContext")
		{

		}

		#endregion Public Constructors

		#region Protected Methods

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>()
				.HasKey(b => b.BookId);

			base.OnModelCreating(modelBuilder);
		}

		#endregion Protected Methods
	}
}