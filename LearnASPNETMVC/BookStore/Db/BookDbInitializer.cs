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
			context.Books.Add(new Book { Title = "Мастер и маргарита", Author = "Булкагов М.А.", Price = 670.99f, Amount = 3 });
			context.Books.Add(new Book { Title = "Белая гвардия", Author = "Булкагов М.А.", Price = 540.50f, Amount = 5 });
			context.Books.Add(new Book { Title = "Идиот", Author = "Достоевский Ф.М.", Price = 460.00f, Amount =10 });
			context.Books.Add(new Book { Title = "Братья Карамазовы", Author = "Достоевский Ф.М.", Price = 799.01f, Amount=2 });
			context.Books.Add(new Book { Title = "Стихотворения и поэмы", Author = "Есенин С.А.", Price = 650.00f, Amount=15 });
		
			base.Seed(context);
		}
	}
}