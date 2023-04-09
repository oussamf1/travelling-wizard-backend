
using System.Data.SqlTypes;

namespace WebApplication1
{
    class VacationPlanGraph
    {
        private Dictionary<string, Dictionary<string, List<Trip>>> graph;

        public VacationPlanGraph()
        {
            graph = new Dictionary<string, Dictionary<string, List<Trip>>>();
        }

        public void AddVertex(string vertex)
        {
            if (!graph.ContainsKey(vertex))
            {
                graph[vertex] = new Dictionary<string, List<Trip>>();
            }
        }

        public void AddEdge(string start, string end, Trip trip)
        {
            AddVertex(start);
            AddVertex(end);

            if (!graph[start].ContainsKey(end))
            {
                graph[start][end] = new List<Trip>();
            }

            graph[start][end].Add(trip);
        }

        public (List<Trip>, decimal) TSP(VacationPlan vacationPlan)
        {
            var path = new List<string>();
            var visited = new HashSet<string>();
            var cost = decimal.MaxValue;
            var best_route = new List<Trip>();
            string start = vacationPlan.Starting_Location.City_code;
            string end  = vacationPlan.Ending_Location.City_code;
            // Add the start and end vertices to the visited set
            visited.Add(start);
            visited.Add(end);

            // Get all the vertices except the start and end vertices
            var vertices = graph.Keys.Except(new[] { start, end });

            // Generate all possible permutations of the vertices
            foreach (var permutation in GetPermutations(vertices.ToList()))
            {

                // Create a new path that starts with the start vertex
                var currentPath = new List<string> { start };

                // Add the vertices in the permutation to the path
                currentPath.AddRange(permutation);

                // Add the end vertex to the end of the path
                currentPath.Add(end);

                // Compute the cost of the path
                var (trips, currentCost) = ComputePathAndCost(currentPath,vacationPlan);

                // Update the best path and cost if the current path is better
                if (currentCost < cost)
                {
                    path = currentPath;
                    cost = currentCost;
                    best_route = trips;
                }
            }

            return (best_route, cost);
        }


        private (List<Trip>, decimal) ComputePathAndCost(List<string> path,VacationPlan vacationPlan)
        {
            decimal cost = 0;
            List<Trip> trips_queue= new List<Trip>();
            // Compute the cost of each edge in the path
            for (var i = 0; i < path.Count - 1; i++)
            {
                var edges = graph[path[i]][path[i + 1]];
                Trip cheapestTrip = null;
                foreach (var trip in edges) 
                {
                    if (IsValidTrip(trip,trips_queue,vacationPlan))
                    {
                        if (cheapestTrip == null || trip.Price < cheapestTrip.Price)
                        {

                            cheapestTrip = trip;
                        }
                    } 
                }
                trips_queue.Add(cheapestTrip);
                if (cheapestTrip != null)
                {
                    cost += cheapestTrip.Price;
                }
            }

            return (trips_queue,cost);
        }
        private static bool IsValidTrip(Trip trip, List<Trip> trips_queue,VacationPlan vacationPlan)
        {
            if (trips_queue.Count == 0)
            {
                return true;
            }
            else
            {
                Trip lastTrip = trips_queue.Last();
                if (lastTrip == null || lastTrip.Trip_route == null || lastTrip.Trip_route.Last() == null ||
                    trip == null || trip.Trip_route == null || trip.Trip_route.First() == null)
                {
                    return false;
                }
                else if (TripHasValidStay(lastTrip,trip,vacationPlan))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private static bool TripHasValidStay(Trip lastTrip,Trip nextTrip,VacationPlan vacationPlan)
        {
            int stay_duration = vacationPlan.CityDaysStayed[lastTrip.Trip_ending_location.City_code];
            if ((!lastTrip.Trip_route.Last().Arrival_utc.HasValue || !nextTrip.Trip_route.First().Departure_utc.HasValue ||
                 ( lastTrip.Trip_route.Last().Arrival_utc.Value.AddDays(stay_duration).CompareTo(nextTrip.Trip_route.First().Departure_utc.Value)< 0)
                 && (lastTrip.Trip_route.Last().Arrival_utc.Value.AddDays(stay_duration).AddHours(30).CompareTo(nextTrip.Trip_route.First().Departure_utc.Value) > 0)))
            {
                Console.WriteLine("Last Trip");
                Console.WriteLine(lastTrip.Trip_route.Last().Arrival_utc.Value);
                Console.WriteLine("Duration");
                Console.WriteLine(stay_duration);
                Console.WriteLine("Last trip plus duration");
                Console.WriteLine(lastTrip.Trip_route.Last().Arrival_utc.Value.AddDays(stay_duration));

                Console.WriteLine("Next");
                return true;
            }
            else
            {
                return false;
            }

        }



        private IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list)
        {
            if (list.Count() == 1)
            {
                yield return list;
            }
            else
            {
                foreach (var item in list)
                {
                    var remainingList = list.Where(i => !i.Equals(item));

                    foreach (var permutation in GetPermutations(remainingList))
                    {
                        yield return new[] { item }.Concat(permutation);
                    }
                }
            }
        }

    }
}