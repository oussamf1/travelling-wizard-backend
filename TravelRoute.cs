namespace WebApplication1
{
    public class TravelRoute
    {
        private DateTime start_date { get; }
        private DateTime end_date { get; }
        public List<Trip> trips_in_order { get; }
        public TravelRoute (List<Trip> tripsIOrder, DateTime startDate, DateTime endDate)
        {
            start_date= startDate;
            end_date= endDate;
            trips_in_order= tripsIOrder;
        }
     
    }
}
