using WebApplication1.External_APIs;
using static System.Net.WebRequestMethods;

namespace WebApplication1
{
    //utility class
    public class VacationPlanner
    {
        private VacationPlanner() { }//private constructor to prevent instantiation
        //public static async Task<string>  Get_Flights() {
        //    Kiwi kiwi= new Kiwi();
        //}
        private Trip[] Get_Bus_Trips (VacationPlan vacation_plan) { return null;}
        private Trip[] Get_all_trips() { return null;}
        private Trip[] Get_Flights(VacationPlan vacation_plan) { return null; }
        private Trip[] Combine_Trips(VacationPlan vacation_plan)
        {
            Trip[] flights = Get_Flights(vacation_plan);
            Trip[] bus_trips = Get_Bus_Trips(vacation_plan);

            
            return null; }

        private TravelRoute[] GetVacations (VacationPlan vacations_plan) { return null;}
    }
}
