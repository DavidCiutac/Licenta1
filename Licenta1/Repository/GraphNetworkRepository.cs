using Licenta1.Contracts;
using Licenta1.Data;
using Microsoft.EntityFrameworkCore;

namespace Licenta1.Repository
{
    public class GraphNetworkRepository : GenericRepository<GraphNetwork>, IGraphNetworkRepository
    {
        public GraphNetworkRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<List<GraphNetwork>> GetGraphNetworkAsync1()
        {
            var graphs = await context.GraphNetworks.Include(q => q.Station1).Include(s => s.Station2).ToListAsync();
            return graphs;
        }
        public async Task<GraphNetwork> GetGraphNetworkByIdAsync1(int id)
        {
            var graphs = await context.GraphNetworks.Include(q => q.Station1).Include(s => s.Station2).ToListAsync();
            foreach (var item in graphs)
                if (item.Id == id) return item;

            return null;
        }
    }
}
