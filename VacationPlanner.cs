using static System.Net.WebRequestMethods;

namespace WebApplication1
{
    //utility class
    public class VacationPlanner
    {
        private VacationPlanner() { }//private constructor to prevent instantiation
        public static async Task<string>   Get_Flights() {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("apikey", "kQBxPTTxfXNHF3PzLjl5u-0F583dfPsx");
            string origin = "JFK";
            string destination = "LAX";
            string startDate = "01/04/2023";
            string endDate = "30/04/2023";
            int numAdults = 1;
            // string apiUrl = $"https://tequila-api.kiwi.com/v3/search?fly_from={origin}&fly_to={destination}&date_from={startDate}&date_to={endDate}&adults={numAdults}";
            string apiUrl = "https://api.tequila.kiwi.com/v2/search?fly_from=LGA&fly_to=MIA&dateFrom=01/04/2023&dateTo=02/04/2023";


            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string responseBody = await response.Content.ReadAsStringAsync();
            // Handle the response as needed
            Console.WriteLine(responseBody);
            return responseBody;
        }
        private Trip[] Get_Bus_Trips () { return null;}
        private Trip[] Get_trips() { return null;}
        private Vacation[] GetVacations (VacationPlan vacations_plans) { return null;}


    }
}
