using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace CAT_App.Data
{
    public class User
    {
		private static readonly HttpClient httpClient = new HttpClient();
        private static Uri apiUri = new Uri("http://10.0.0.107:3000/");

        public static bool LoggedIn { get; set; } = false;
        public static string Username { get; set; } 
        public static string Password { get; set; }
        public static string AccountType { get; set; }
        public static double Balance { get; set; }
		public static string[,] History { get; set; } 

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
        public static async Task<string> Login(string name, string pass)
        {
            if (httpClient.BaseAddress == null || httpClient.BaseAddress.Equals(""))
            {
                httpClient.BaseAddress = apiUri;
            }

            // Generate data to send to API
            var data = new
            {
                username = name,
                password = pass
            };
            string jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            
            // Send the POST request to /login
            HttpResponseMessage response = await httpClient.PostAsync("login", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonNode userInfo = JsonNode.Parse(responseBody);
			Console.WriteLine(responseBody);

			// Check if the request was successful
			if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("User logged in successfully!");

                // Add the data from responseBody to the User
                LoggedIn = true;
                Username = name;
                Password = pass;
                AccountType = (string)userInfo["accountType"];
                Balance = (double)userInfo["balance"];
                RetrieveHistory((JsonNode)userInfo["history"]);

                return (string)userInfo["success"];
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                
                return (string)userInfo["response"];
            }
        }

        public static async Task<string> Register(string name, string pass, string passRepeat, string aType)
        {
            if (httpClient.BaseAddress == null || httpClient.BaseAddress.Equals(""))
            {
                httpClient.BaseAddress = apiUri;
            }

            // Generate data to send to API
            var data = new
            {
                username = name,
                password = pass,
                passwordRepeat = passRepeat,
                accountType = aType
            };
			string jsonData = JsonSerializer.Serialize(data);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			// Send the POST request to /register
			HttpResponseMessage response = await httpClient.PostAsync("register", content);
			string responseBody = await response.Content.ReadAsStringAsync();
            JsonNode userInfo = JsonNode.Parse(responseBody);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("User registered successfully!");
                return (string)userInfo["success"];
            }
			else
			{
				Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return (string)userInfo["response"];
            }
		}

        public static async Task<string> UpdateBalance(string transaction, double changeAmount)
        {
            // Generate data to send to API
            var data = new
            {
                username = User.Username,
                password = User.Password,
                transactionType = transaction, 
                amount = changeAmount
            };
            string jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Send the PATCH request to /updateBalance
            HttpResponseMessage response = await httpClient.PatchAsync("updateBalance", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonNode userInfo = JsonNode.Parse(responseBody);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Balance updated successfully!");

                // Add the data from responseBody to the User
                Balance = (double)userInfo["balance"];
				RetrieveHistory((JsonNode)userInfo["history"]);

				return (string)userInfo["success"];
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return (string)userInfo["response"];
            }

        }

        public static async Task<string> DecreaseBalance() 
        {
            double amount = 0; 

            switch (User.AccountType)
            {
                case "child":
                    amount = -1.5;
                    break;
                case "student":
                    amount = -2.5;
                    break;
                case "adult":
                    amount = -4.0;
                    break;
                case "senior":
                    amount = -2.5;
                    break;
            }

            return await UpdateBalance("Bus Ride", amount);
        }

        public static void RetrieveHistory(JsonNode historyList)
        {
            JsonArray historyArray = historyList.AsArray();
            string[,] userHistory = new string[historyArray.Count, 2];

            for (int i = 0; i < historyArray.Count; i++)
            {
                userHistory[i, 0] = historyArray[i]["transactionType"].ToString();
                userHistory[i, 1] = String.Format("{0:C2}", Int64.Parse(historyArray[i]["amount"].ToString()));
            }

            User.History = userHistory;
        }

        public static string GetBalance()
        {
            return String.Format("{0:C2}", Balance);
        }
    }
}
