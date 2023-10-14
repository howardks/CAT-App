using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT_App.Data
{
	public class NewsItemService
	{
		public static readonly NewsItem[] news = new[]
		{
			new NewsItem(new DateTime(2023, 10, 7, 18, 01, 0), "75 year old cat lady attempted to bring her 12 cats aboard bus 12 today ending in catastrophe!"),
			new NewsItem(new DateTime(2023, 10, 6, 12, 19, 0), "News number two"),
			new NewsItem(new DateTime(2023, 10, 5, 09, 47, 0), "News number three")
		};
	}
}
