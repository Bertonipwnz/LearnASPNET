#define MY_SQL_SERVER

namespace BookStore.Db.Contexts
{
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.Diagnostics;


	/// <summary>
	/// База данных.
	/// </summary>
	public class DatabaseViaCommand
	{
#if MY_SQL_SERVER
		private static string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Bookstore.mdf';Integrated Security=True";
#endif

		private const string BOOK_TABLE_NAME = "book";

		public static void InvokeLearnQueryes()
		{
			CreateBookTable();
			InsertDataInBookTable("Мастер и маргарита", "Булкагов М.А.", 670.99f, 3);

			//TODO: Внести данные
//			INSERT INTO book(title, author, price, amount)
//VALUES('Белая гвардия', 'Булгаков М.А.', 540.50, 5),
//       ('Идиот', 'Достоевский Ф.М.', 460.00, 10),
//       ('Братья Карамазовы', 'Достоевский Ф.М.', 799.01, 2);

		}

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

		private static void InsertDataInBookTable(string title, string author, float price, int amount)
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


					command.ExecuteNonQuery(); // Выполнение запроса

					command.Dispose();
				}

				connection.Close();
				connection.Dispose();
			}
		}


		private static void RemoveTable(string tableName)
		{
			string sqlQuery = $@"
            IF EXISTS (SELECT * FROM sys.tables WHERE name = '{tableName}')
            BEGIN
                DROP TABLE {tableName};
            END";

			TryExecuteNonQuery(sqlQuery);
		}

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
	}
}