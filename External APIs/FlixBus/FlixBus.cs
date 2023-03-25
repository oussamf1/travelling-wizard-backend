namespace WebApplication1.External_APIs
{
    public class FlixBus : IDataSource
    {
        public Task<List<Trip>> GetTrips(VacationPlan vacation_plan)
        {
            throw new NotImplementedException();
        }
    }
}
