namespace CAT_App.Data
{
    public class User
    {
        public static bool LoggedIn { get; } = false;
        public static string Username { get; set; }
        public static string Password { get; set; }

        // Maybe stuff for connecting to api here? 

    }
}
