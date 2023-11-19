using Newtonsoft.Json.Linq;

namespace CAT_App
{

    public class Coordinate
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }


    public class GoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleMapsService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<JObject> GetDirectionsAsync(string origin, string destination)
        {
            string url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content);
        }

        public async Task<List<Coordinate>> ExtractCoordinatesAsync(string origin, string destination)
        {
            var directions = await GetDirectionsAsync(origin, destination);
            var routes = directions["routes"];
            var coordinates = new List<Coordinate>();

            if (routes != null && routes.Any())
            {
                var legs = routes[0]["legs"];
                if (legs != null && legs.Any())
                {
                    var steps = legs[0]["steps"];
                    if (steps != null)
                    {
                        foreach (var step in steps)
                        {
                            var startLocation = step["start_location"];
                            var endLocation = step["end_location"];

                            if (startLocation != null && endLocation != null)
                            {
                                var startCoord = new Coordinate
                                {
                                    lat = startLocation["lat"].Value<double>(),
                                    lng = startLocation["lng"].Value<double>()
                                };
                                var endCoord = new Coordinate
                                {
                                    lat = endLocation["lat"].Value<double>(),
                                    lng = endLocation["lng"].Value<double>()
                                };

                                coordinates.Add(startCoord);
                                coordinates.Add(endCoord);
                            }
                        }
                    }
                }
            }

            return coordinates;
        }


    }


}

