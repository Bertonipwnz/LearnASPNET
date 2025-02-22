namespace LearnASPNETMVC.Models.Db
{
	using System.Data.Entity;

	/// <summary>
	/// Контекст базы данных.
	/// </summary>
	public class AppContext : DbContext
	{
		#region Public Properties

		/// <summary>
		/// Таблица пользователей.
		/// </summary>
		public DbSet<User> Users { get; set; }

		#endregion Public Properties

		#region Public Constructors

		/// <summary>
		/// Создает экземпляр <see cref="AppContext"/>
		/// </summary>
		public AppContext() : base("DefaultConnection")
		{

		}

		#endregion Public Constructors
	}
}