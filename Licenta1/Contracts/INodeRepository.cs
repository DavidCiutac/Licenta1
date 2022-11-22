using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Contracts
{
    public interface INodeRepository : IGenericRepository<Node>
    {
        public void GenerateNeighbours(Node node);
        Task<List<Node>>  GetNodesAsync1();
        Task<Node> GetNodesAsync2(int? id);
    }
}
