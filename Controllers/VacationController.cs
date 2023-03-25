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
            DateTime start = new DateTime(2023, 6, 1);
            DateTime end = new DateTime(2023, 6, 23);
            string[] cities = new string[] { "PAR", "BUD", "TUN" };
            VacationPlan myVacationPlan = new VacationPlan(start, end, cities);
            VacationPlanner vacationPlanner = new VacationPlanner(myVacationPlan);

            return await vacationPlanner.Combine_Trips(myVacationPlan);
        }
    }
}
