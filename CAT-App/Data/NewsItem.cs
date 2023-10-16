namespace CAT_App.Data
{
	public class NewsItem
	{
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
