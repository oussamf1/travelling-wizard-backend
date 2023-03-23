namespace WebApplication1
{
    public class TravelRoute
    {
        private DateTime start_date;
        private DateTime end_date;
        public Trip[] trips_in_order;
        public Trip[] Trips_in_order
        {
            get { return trips_in_order; }
            set { trips_in_order = value; }
        }
        public DateTime Start_date { get { return start_date; } set { start_date = value; } }   
        public DateTime End_date { get { return end_date; } set { end_date = value; } }
    }
}
