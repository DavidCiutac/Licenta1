using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Contracts
{
    public interface INodeRepository : IGenericRepository<Node>
    {
        public void GenerateNeighbours(Node node);
        Task<List<NodeVM>>  GetNodesAsync1();
        Task<NodeVM> GetNodesAsync2(int? id);
    }
}
