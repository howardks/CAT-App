namespace CAT_App.Data
{
	public class NewsItemService
	{
		public static readonly NewsItem[] news = new[]
		{
			new NewsItem(new DateTime(2023, 10, 7, 18, 01, 0), "75 year old cat lady attempted to bring her 12 cats aboard bus 12 today; ended in catastrophe."),
			new NewsItem(new DateTime(2023, 10, 6, 12, 19, 0), "Starting next Monday, Route 6 will be rerouted due to unexpected construction. Please check the updated map for current stops."),
			new NewsItem(new DateTime(2023, 10, 5, 09, 47, 0), "Riders of route 17, please be aware that a bus break down will cause delays of up to 20 minutes for the evening commute. We are working to resolve the situation and will provide updates as soon as possible")
		};
	}
}
