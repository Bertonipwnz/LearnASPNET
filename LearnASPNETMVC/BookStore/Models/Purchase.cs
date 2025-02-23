namespace BookStore.Models
{
	using System;
	
	/// <summary>
	/// Модель покупки.
	/// </summary>
	public class Purchase
	{
		/// <summary>
		/// Айди покупки.
		/// </summary>
		public int PurchaseId { get; set; }

		/// <summary>
		/// Фамилия и имя покупателя.
		/// </summary>
		public string Person { get; set; }

		/// <summary>
		/// Адрес покупателя.
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Дата покупки.
		/// </summary>
		public DateTime Date { get;set; }

		/// <summary>
		/// Айди покупки.
		/// </summary>
		public int BookId { get; set; }
	}
}