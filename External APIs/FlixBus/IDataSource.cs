namespace WebApplication1.External_APIs
{
    public interface IDataSource
    {
        Task<List<Trip>> GetTrips();

    }
}
