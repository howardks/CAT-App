namespace CAT_App.Data
{
    public class Route
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }

        public Route(string symbol, string name, Color color)
        {
            Symbol = symbol;
            Name = name;
            Color = color;
        }
    }
}
