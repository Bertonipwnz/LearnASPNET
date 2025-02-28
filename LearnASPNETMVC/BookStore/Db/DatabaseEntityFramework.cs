﻿namespace BookStore.Db
{
	using BookStore.Models;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// База данных через EntitFramework.
	/// </summary>
	public static class DatabaseEntityFramework
	{
		/// <summary>
		/// Выполняет вызов запросов в ходе обучения.
		/// </summary>
		public static async Task InvokeLearnQueryes()
		{
			await DeleteDataTableBookAsync();
			await InsertInBookAsync("Мастер и маргарита", "Булкагов М.А.", 670.99f, 3);
			await InsertInBookAsync("Белая гвардия", "Булкагов М.А.", 540.50f, 5);
			await InsertInBookAsync("Идиот", "Достоевский Ф.М.", 460.00f, 10);
			await InsertInBookAsync("Братья Карамазовы", "Достоевский Ф.М.", 799.01f, 2);
			await InsertInBookAsync("Стихотворения и поэмы", "Есенин С.А.", 650.00f, 15);
		}

		/// <summary>
		/// Получает список книг из БД.
		/// </summary>
		public static List<Book> GetBooks()
		{
			List<Book> books = new List<Book>();
			using (BookContext db = new BookContext())
			{
				books = db.Books.ToList();
			}

			return books;
		}

		/// <summary>
		/// Удаляет данные таблицы книг.
		/// </summary>
		private static async Task DeleteDataTableBookAsync()
		{
			using (BookContext db = new BookContext())
			{
				// Получаем все записи, но без их загрузки в память
				var booksToDelete = db.Books.ToList();

				// Удаляем все записи за один раз
				db.Books.RemoveRange(booksToDelete);

				await db.SaveChangesAsync();
			}
		}

		/// <summary>
		/// Выполняет INSERT в базу данных с книгами.
		/// </summary>
		/// <param name="title">Заголовок.</param>
		/// <param name="author">Автор.</param>
		/// <param name="price">Цена.</param>
		/// <param name="amount">Количество.</param>
		private static async Task InsertInBookAsync(string title, string author, float price, int amount)
		{
			using (BookContext db = new BookContext())
			{
				Book book = new Book()
				{
					Title = title,
					Author = author,
					Price = price,
					Amount = amount
				};

				db.Books.Add(book);

				await db.SaveChangesAsync();
			}
		}
	}
}