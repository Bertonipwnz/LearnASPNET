#define MY_SQL_SERVER

namespace BookStore.Db.Contexts
{
	using BookStore.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;

	/// <summary>
	/// База данных.
	/// </summary>
	public class DatabaseViaCommand
	{
		#region Private Fields

#if MY_SQL_SERVER
		/// <summary>
		/// Строка соединения с БД.
		/// </summary>
		private static string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Bookstore.mdf';Integrated Security=True";
#endif

		/// <summary>
		/// Имя таблицы с книгами.
		/// </summary>
		private const string BOOK_TABLE_NAME = "book";

		#endregion Private Fields

		#region Public Methods

		/// <summary>
		/// Вызывает запросы которые были в ходе обучения.
		/// </summary>
		public static void InvokeLearnQueryes()
		{
			//1.1
			CreateBookTable();
			InsertDataInBookTable("Мастер и маргарита", "Булкагов М.А.", 670.99m, 3);
			InsertDataInBookTable("Белая гвардия", "Булкагов М.А.", 540.50m, 5);
			InsertDataInBookTable("Идиот", "Достоевский Ф.М.", 460.00m, 10);
			InsertDataInBookTable("Братья Карамазовы", "Достоевский Ф.М.", 799.01m, 2);
			InsertDataInBookTable("Стихотворения и поэмы", "Есенин С.А.", 650.00m, 15);
		}

		/// <summary>
		/// Запрос получения цены упаковки.
		/// </summary>
		/// <param name="bookId">Айди книги.</param>
		public static decimal GetPackPriceOnBook(int bookId)
		{
			string query = $"SELECT amount FROM {BOOK_TABLE_NAME} WHERE book_id = {bookId}";

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				// Открываем соединение с базой данных
				connection.Open();

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					// Выполняем запрос и получаем данные с помощью SqlDataReader
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							return Math.Round(1.65m * reader.GetInt32(0),2);
						}
					}
				}
			}

			return 0.0m;
		}

		/// <summary>
		/// Получает новую цену на книгу.
		/// </summary>
		/// <param name="bookId">Айди книги.</param>
		public static decimal GetNewPriceOnBook(int bookId)
		{
			string query = $"SELECT price, " +
				$"price - (price*30/100)/(1+30/100) AS newPrice " +
				$"FROM {BOOK_TABLE_NAME} WHERE book_id = {bookId}";

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				// Открываем соединение с базой данных
				connection.Open();

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					// Выполняем запрос и получаем данные с помощью SqlDataReader
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							return Math.Round(reader.GetDecimal(1),2);
						}
					}
				}
			}

			return 0.0m;
		}

		/// <summary>
		/// Получает книги.
		/// </summary>
		public static List<Book> GetBooks()
		{
			List<Book> books = new List<Book>();

			string query = $"SELECT * FROM {BOOK_TABLE_NAME}";

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				// Открываем соединение с базой данных
				connection.Open();

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					// Выполняем запрос и получаем данные с помощью SqlDataReader
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Book book = new Book
							{
								BookId = reader.GetInt32(0),
								Title = reader.GetString(1),
								Author = reader.GetString(2),
								Price = reader.GetDecimal(3),
								Amount = reader.GetInt32(4)
							};

							// Добавляем объект в список
							books.Add(book);
						}
					}
				}
			}

			return books;
		}


		#endregion Public Methods

		#region Private Methods

		/// <summary>
		/// Создат таблицу книг.
		/// </summary>
		private static void CreateBookTable()
		{
#if DEBUG
			RemoveTable(BOOK_TABLE_NAME);
#endif


#if MY_SQL_SERVER
			//NVARCHAR - для решения проблем с кодировкой.
			string sqlQuery = $@"CREATE TABLE {BOOK_TABLE_NAME} (
                        book_id INT PRIMARY KEY IDENTITY, 
						title NVARCHAR(50),
						author NVARCHAR(30),
						price DECIMAL(8,2),
						amount INT
                    )";
#else
			string sqlQuery = $@"CREATE TABLE {BOOK_TABLE_NAME} (
                        book_id INT PRIMARY KEY AUTO_INCREMENT, 
						title VARCHAR(50),
						author VARCHAR(30),
						price DECIMAL(8,2),
						amount INT
                    )";
#endif
			TryExecuteNonQuery(sqlQuery);
		}

		/// <summary>
		/// Выполняет запрос INSERT в таблицу книг.
		/// </summary>
		/// <param name="title">Заголовок.</param>
		/// <param name="author">Автор.</param>
		/// <param name="price">Цена.</param>
		/// <param name="amount">Количество.</param>
		private static void InsertDataInBookTable(string title, string author, decimal price, int amount)
		{
			string sqlQuery = $@"INSERT INTO {BOOK_TABLE_NAME} (title, author, price, amount) 
                         VALUES (@Title, @Author, @Price, @Amount)";

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand(sqlQuery, connection))
				{
					command.Parameters.AddWithValue("@Title", title);
					command.Parameters.AddWithValue("@Author", author);
					command.Parameters.AddWithValue("@Price", price);
					command.Parameters.AddWithValue("@Amount", amount);

					command.ExecuteNonQuery();
					command.Dispose();
				}

				connection.Close();
				connection.Dispose();
			}
		}

		/// <summary>
		/// Удаляет таблицу.
		/// </summary>
		/// <param name="tableName">Имя таблицы.</param>
		private static void RemoveTable(string tableName)
		{
			string sqlQuery = $@"
            IF EXISTS (SELECT * FROM sys.tables WHERE name = '{tableName}')
            BEGIN
                DROP TABLE {tableName};
            END";

			TryExecuteNonQuery(sqlQuery);
		}

		/// <summary>
		/// Пытается вызвать sql запрос.
		/// </summary>
		/// <param name="sqlQuery">Запрос.</param>
		private static void TryExecuteNonQuery(string sqlQuery)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(sqlQuery, connection))
					{
						command.ExecuteNonQuery();
						command.Dispose();
					}

					connection.Close();
					connection.Dispose();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Ошибка при выполнении SQL-запроса: " + ex.Message);
			}
		}

		#endregion Private Methods
	}
}