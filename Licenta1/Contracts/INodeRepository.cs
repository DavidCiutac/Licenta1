using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Contracts
{
    public interface INodeRepository : IGenericRepository<Node>
    {
        Task GenerateNeighboursAsync(Node node);
        Task<List<Node>>  GetNodesAsync();
        Task<Node> GetNodeAsync(int? id);
        Task GenerateDatabaseAsync();
    }
}
