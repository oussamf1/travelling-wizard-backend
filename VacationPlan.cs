namespace WebApplication1
{
    public class VacationPlan
    {
        private string[] cities_to_visit;
        private DateTime vacation_start_date;
        private DateTime vacation_end_date;
        public DateTime Vacation_start_date { get { return vacation_start_date; } set { vacation_start_date = value; } }
        public DateTime Vacation_end_date { get { return vacation_end_date; } set { vacation_end_date = value; } }
        public string[] Cities_to_visit { get { return cities_to_visit; } set { cities_to_visit = value; } }
    }
}