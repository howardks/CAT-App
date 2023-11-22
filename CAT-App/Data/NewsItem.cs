namespace CAT_App.Data
{
	public class NewsItem
	{
		// News item attributes
		public DateTime DateTime { get; set; }
		public string Story { get; set; }

		public NewsItem(DateTime dateTime, string story)
		{
			DateTime = dateTime;
			Story = story;
		}

		public override String ToString()
		{
			return DateTime.ToString("g") + " - " + Story;
		}
	}
}
