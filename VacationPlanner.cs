using System.Collections.Generic;
using WebApplication1.External_APIs;
using WebApplication1.External_APIs.Kiwi;
using static System.Net.WebRequestMethods;

namespace WebApplication1
{
    //utility class
    public class VacationPlanner
    {
        private VacationPlan vacation_plan;
        public VacationPlanner(VacationPlan vacationPlan) {
        vacation_plan= vacationPlan;
        }
        private List<Trip> Get_Bus_Trips (VacationPlan vacation_plan) {
            return null;
        }
        private async Task<List<Trip>>  Get_Flights(VacationPlan vacation_plan) {
            Kiwi kiwi = new Kiwi();
            List<Trip> trips_flights = new List<Trip>();
            List<Trip> kiwi_trips = await kiwi.GetTrips(vacation_plan);
            trips_flights.AddRange(kiwi_trips);
            return trips_flights;
             }
        public async Task<List<Trip>> Combine_Trips(VacationPlan vacation_plan)
        {
            List<Trip> trips = new List<Trip>();
            List<Trip> trips_flights =await Get_Flights(vacation_plan);
            trips.AddRange(trips_flights);
            return trips;
        }

        private List<TravelRoute> GetVacations (VacationPlan vacations_plan) {

            return null;
        
        }
    }
}
