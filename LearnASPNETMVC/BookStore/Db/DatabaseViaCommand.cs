namespace BookStore.Db.Contexts
{
	using System;
	using System.Data.SqlClient;

	/// <summary>
	/// База данных.
	/// </summary>
	public class DatabaseViaCommand
	{
		private static string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Bookstore.mdf';Integrated Security=True";

		private const string BOOK_TABLE_NAME = "book";

		public static void CreateBookTable()
		{
#if DEBUG
			RemoveTable(BOOK_TABLE_NAME);
#endif

			string sqlQuery = $@"CREATE TABLE {BOOK_TABLE_NAME} (
                        Id INT PRIMARY KEY IDENTITY,
                        Name NVARCHAR(100),
                        Age INT
                    )";

			TryExecuteNonQuery(sqlQuery);
		}

		public static void RemoveTable(string tableName)
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