using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacationController : ControllerBase
    {
        [HttpGet(Name = "GetVacation")]
        public async Task <string> Get()
        {
           return await VacationPlanner.Get_Flights();
        }
    }
}
