namespace CAT_App.Data
{
    public class FareService
    {
        // Fares based on account types
        public static readonly Fare[] fares = new[]
        {
            new Fare("Child", 1.5), 
            new Fare("Student", 2.5), 
            new Fare("Adult", 4.0), 
            new Fare("Senior", 2.5)
        };
    }
}
