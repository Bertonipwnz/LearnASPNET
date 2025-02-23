namespace BookStore.Db
{
	using BookStore.Models;
	using System.Data.Entity;

	/// <summary>
	/// Инициализатор базы данных, который сбрасывает её при каждом запуске приложения и заполняет начальными данными.
	/// </summary>
	public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
	{
		/// <summary>
		/// Заполняет базу данных начальными данными после создания базы данных.
		/// </summary>
		/// <param name="context">Контекст базы данных <see cref="BookContext"/>, в который будут добавлены данные.</param>
		protected override void Seed(BookContext context)
		{
			context.Books.Add(new Book { Name = "Война и мир", Author = "Л. Толстой", Price = 220 });
			context.Books.Add(new Book { Name = "Отцы и дети", Author = "И. Тургенев", Price = 180 });
			context.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов", Price = 150 });
			context.Books.Add(new Book { Name = "тест", Author = "А. Чехов", Price = 150 });
		
			base.Seed(context);
		}
	}
}