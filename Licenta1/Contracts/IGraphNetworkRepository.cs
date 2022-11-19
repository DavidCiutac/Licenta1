using Licenta1.Data;

namespace Licenta1.Contracts
{
    public interface IGraphNetworkRepository : IGenericRepository<GraphNetwork>
    {
        Task<List<GraphNetwork>> GetGraphNetworkAsync1();

        Task<GraphNetwork> GetGraphNetworkByIdAsync1(int id);
    }
}

