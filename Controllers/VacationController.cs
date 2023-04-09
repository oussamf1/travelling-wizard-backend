using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApplication1.External_APIs.Kiwi;

namespace WebApplication1.Controller.Kiwi_API
{
    [ApiController]
    [Route("[controller]")]
    public class VacationController : ControllerBase
    {
        [HttpGet(Name = "GetVacation")]
        public async Task <List<Trip>> Get()
        {
            DateTime start = new DateTime(2023, 5, 1);
            DateTime end = new DateTime(2023, 7, 30);
            Location starting_location = new Location("Budapest", "BUD", "airport1");
            Location ending_location = new Location("Madrid", "MAD", "airport1");
            Dictionary<string, int> cityDays = new Dictionary<string, int>();
            cityDays.Add("BER", 3);
            cityDays.Add("AMS", 5);
            cityDays.Add("ATH", 1);
            VacationPlan myVacationPlan = new VacationPlan(start, end, cityDays, starting_location, ending_location);
            VacationPlanner vacationPlanner = new VacationPlanner(myVacationPlan);
          return await vacationPlanner.Get_BestTrips(myVacationPlan);
        }
    }
}
