namespace BookStore.Db.Contexts
{
	using System;
	using System.Data.SqlClient;

	/// <summary>
	/// База данных.
	/// </summary>
	public class DatabaseCommand
	{
		private static string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Bookstore.mdf';Integrated Security=True";

		private const string BOOK_TABLE_NAME = "book";

		public static void CreateBookTable()
		{
			string sqlQuery = $@"CREATE TABLE {BOOK_TABLE_NAME} (
                        Id INT PRIMARY KEY IDENTITY,
                        Name NVARCHAR(100),
                        Age INT
                    )";

			try
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					using (var command = new SqlCommand(sqlQuery, connection))
					{
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{

			}
		}
	}
}