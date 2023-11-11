using System.Text;
using System.Text.Json;

namespace CAT_App.Data
{
    public class User
    {
		private static readonly HttpClient httpClient = new HttpClient();
        private static Uri apiUri = new Uri("http://10.0.0.107:3000/");

        public static bool LoggedIn { get; set; } = true;
        public static string Username { get; set; } 
        public static string Password { get; set; }
        public static string AccountType { get; set; }
        public static double Balance { get; set; }
		public static string[][] History { get; set; } 

        public static void Logout()
        {
            LoggedIn = false;
            Username = null;
            Password = null;
            AccountType = null;
            Balance = 0;
            History = null;
        }

        // Maybe stuff for connecting to api here? 
        public static void Login(string name, string pass)
        {
            httpClient.BaseAddress = apiUri;


        }

        public static async void Register(string name, string pass, string passRepeat, string aType)
        {
            httpClient.BaseAddress = apiUri;

            var data = new
            {
                username = name,
                password = pass,
                passwordRepeat = passRepeat,
                accountType = aType
            };
			string jsonData = JsonSerializer.Serialize(data);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			// Send the POST request
			HttpResponseMessage response = await httpClient.PostAsync("register", content);
			string responseBody = await response.Content.ReadAsStringAsync();
			Console.WriteLine(responseBody);

			// Check if the request was successful
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("User registered successfully!");
			}
			else
			{
				Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
			}
		}

        public static void IncreaseBalance(double increase)
        {
            httpClient.BaseAddress = apiUri;


        }

        public static void DecreaseBalance() 
        {
            httpClient.BaseAddress = apiUri;


        }

        public static void RetrieveHistory()
        {

        }

        public static string GetBalance()
        {
            return String.Format("{0:C2}", Balance);
        }
    }
}
