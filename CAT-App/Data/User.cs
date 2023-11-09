using System.Text;
using System.Text.Json;

namespace CAT_App.Data
{
    public class User
    {
		private static readonly HttpClient httpClient = new HttpClient();

		public static bool LoggedIn { get; set; } = true;
        public static string Username { get; set; } 
        public static string Password { get; set; }
        public static string Type { get; set; }
        public static double Balance { get; set; }
		public static string[][] History { get; set; } 

        public static void Logout()
        {
            LoggedIn = false;
            Username = null;
            Password = null;
            Type = null;
            Balance = 0;
            History = null;
        }

        // Maybe stuff for connecting to api here? 
        public static void Login(string name, string pass)
        {

        }

        public static async void Register(string name, string pass, string passRepeat)
        {
            Console.WriteLine(name + pass + passRepeat);

            Uri apiUri = new Uri("http://10.0.0.107:3000/");
            httpClient.BaseAddress = apiUri;

            var data = new
            {
                username = name,
                password = pass,
                passwordRepeat = passRepeat
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

        public static void UpdateBalance(double increase)
        {

        }

        public static string GetBalance()
        {
            return String.Format("{0:C2}", Balance);
        }
    }
}
