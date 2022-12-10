using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Contracts
{
    public interface IGraphNetworkRepository : IGenericRepository<GraphNetwork>
    {
        Task<List<GraphNetwork>> GetGraphNetworkAsync1();

        Task<GraphNetwork> GetGraphNetworkAsync(int id);

    }
}

