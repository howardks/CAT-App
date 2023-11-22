namespace CAT_App.Data
{
    public class RouteService
    {
        // Available routes
        public static readonly Route[] routes = new[] 
        {
            new Route("3B", "3B Augusta Ave", Colors.Orange),
            new Route("3", "3 West Chatham/Sav HHI Airport", Colors.Brown),
            new Route("4", "4 Barnard", Colors.LightBlue),
            new Route("6", "6 Cross Town", Colors.LimeGreen),
            new Route("DOT", "7D-C Cloverdale and Carver Village", Colors.Purple),
            new Route("10", "10 East Savannah", Colors.DarkBlue),
            new Route("11", "11 Candler", Colors.LightPink),
            new Route("12", "12 Henry", Colors.SlateGray),
            new Route("14", "14 Abercorn", Colors.DarkKhaki),
            new Route("17", "17 Silk Hope", Colors.Coral),
            new Route("25", "25 MLK", Colors.DarkRed),
            new Route("27", "27 Waters", Colors.DarkViolet),
            new Route("28", "28 Waters", Colors.DarkGreen),
            new Route("29", "29 Gwinnett Cloverdale", Colors.DarkCyan),
            new Route("31", "31 Skidaway/Sandfly", Colors.HotPink)
        };
    }
}
