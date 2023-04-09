using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WebApplication1.External_APIs;
using WebApplication1.External_APIs.Kiwi;
using static System.Net.WebRequestMethods;

namespace WebApplication1
{
    //utility class
    public class VacationPlanner
    {
        private VacationPlan vacation_plan;
        private VacationPlanGraph vacation_plan_graph;
        public VacationPlanner(VacationPlan vacationPlan) {
        vacation_plan= vacationPlan;
        vacation_plan_graph = new VacationPlanGraph();
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
        public async Task<List<Trip>> Get_BestTrips (VacationPlan vacation_plan)
        {
            string[] cities_to_visit = vacation_plan.CityDaysStayed.Keys.ToArray();
            List<Trip> trips = await Combine_Trips(vacation_plan);
            vacation_plan_graph.AddVertex(vacation_plan.Starting_Location.City_code);
            vacation_plan_graph.AddVertex(vacation_plan.Ending_Location.City_code);
            foreach(var city_to_visit in cities_to_visit)
            {
                vacation_plan_graph.AddVertex(city_to_visit);

            }
            foreach(var trip in trips)
            {
                vacation_plan_graph.AddEdge(trip.Trip_starting_location.City_code, trip.Trip_ending_location.City_code, trip);
            }
            var (path, cost) = vacation_plan_graph.TSP(vacation_plan);
            return path;
        }
  
    }
}
