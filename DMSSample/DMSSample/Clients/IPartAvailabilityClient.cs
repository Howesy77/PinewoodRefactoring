namespace Pinewood.DMSSample.Business.Clients
{
    public interface IPartAvailabilityClient : IDisposable
    {
        Task<int> GetAvailability(string stockCode);
    }
}
