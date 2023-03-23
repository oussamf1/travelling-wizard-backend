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
            Kiwi kiwi= new Kiwi();  

             return await kiwi.GetTrips();
        }
    }
}
