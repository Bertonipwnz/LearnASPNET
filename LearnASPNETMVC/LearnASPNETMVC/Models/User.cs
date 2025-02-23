namespace LearnASPNETMVC.Models
{
	/// <summary>
	/// Модель пользователя.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Айди пользователя.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string UserName { get; set; } = string.Empty;
	}
}