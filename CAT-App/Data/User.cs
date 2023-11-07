namespace CAT_App.Data
{
    public class User
    {
        public static bool LoggedIn { get; set; } = true;
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Type { get; set; }
        public static double Balance { get; set; }

        public static void Logout()
        {
            LoggedIn = false;
            Username = null;
            Password = null;
            Type = null;
            Balance = 0;
        }

        // Maybe stuff for connecting to api here? 

    }
}
