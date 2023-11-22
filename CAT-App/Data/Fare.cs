namespace CAT_App.Data
{
    public class Fare
    {
        // Fare attributes
        public string Type { get; set; }
        public double Cost { get; set; }

        public Fare(string type, double cost)
        {
            Type = type;
            Cost = cost;
        }

        public string FormattedCost()
        {
            return String.Format("{0:C2}", this.Cost);
        }
    }
}
