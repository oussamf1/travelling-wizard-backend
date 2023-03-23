using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Transactions;
using WebApplication1.External_APIs.Kiwi;

namespace WebApplication1.External_APIs.Kiwi
{
    public class Kiwi : IDataSource
    {
        public async Task<List<Trip>> GetTrips()
        {
            FlightstInfo flights_info = await GetFlights();
            List <Trip> trips = await FormatData(flights_info);
            //Trip[] trips = FormatData(flights_string_format);
            return trips;
        }
        public async Task<FlightstInfo> GetFlights()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("apikey", "kQBxPTTxfXNHF3PzLjl5u-0F583dfPsx");
            string origin = "JFK";
            string destination = "LAX";
            string startDate = "01/04/2023";
            string endDate = "30/04/2023";
            int numAdults = 1;
            // string apiUrl = $"https://tequila-api.kiwi.com/v3/search?fly_from={origin}&fly_to={destination}&date_from={startDate}&date_to={endDate}&adults={numAdults}";
            string apiUrl = "https://api.tequila.kiwi.com/v2/search?fly_from=BUD&fly_to=TUN&dateFrom=01/04/2023&dateTo=02/04/2023";
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string responseBody = await response.Content.ReadAsStringAsync();
            FlightstInfo flightsInfo = JsonConvert.DeserializeObject<FlightstInfo>(responseBody);
            return flightsInfo;
        }
        private async Task <List<Trip>> FormatData(FlightstInfo flightsInfo)
        {

            List<Trip> trips = new List<Trip>();
            foreach (Flight flight in flightsInfo.data)
            {
                Location trip_starting_location = new Location(flight.cityFrom, flight.cityCodeFrom, flight.flyFrom);
                Location trip_ending_location = new Location(flight.cityTo, flight.cityCodeTo, flight.flyTo);
                List<Passage> passages = new List<Passage>();
                foreach (var transit in flight.route)
                {
                    Location passage_starting_location = new Location(transit.cityFrom, transit.cityCodeFrom, transit.flyFrom);
                    Location passage_ending_location = new Location(transit.cityTo, transit.cityCodeTo, transit.flyTo);
                    Passage passage = new Passage(passage_starting_location, passage_ending_location, transit.airline, transit.utc_arrival, transit.utc_departure, transit.flight_no);
                    passages.Add(passage);

                }
                Trip trip = new Trip(trip_starting_location, trip_ending_location, true, false, false, passages, flight.duration.total, flight.price, flightsInfo.curreny);
                trips.Add(trip);
            }
            return trips;
        }
    }
}
