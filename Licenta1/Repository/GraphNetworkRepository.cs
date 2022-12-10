using AutoMapper;
using Licenta1.Contracts;
using Licenta1.Data;
using Licenta1.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenta1.Repository
{
    public class GraphNetworkRepository : GenericRepository<GraphNetwork>, IGraphNetworkRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IStationsRepository stationsRepository;
        private readonly IMapper mapper;
        public GraphNetworkRepository(ApplicationDbContext context, IStationsRepository stationsRepository, IMapper mapper) :base(context)
        {
            this.context = context;
            this.stationsRepository = stationsRepository;
            this.mapper = mapper;
        }
        public async Task<List<GraphNetwork>> GetGraphNetworkAsync1()
        {
            var graphs = await context.GraphNetworks.Include(q => q.Station1).Include(s => s.Station2).ToListAsync();
            return graphs;
        }
        public async Task<GraphNetwork> GetGraphNetworkAsync(int id)
        {
            var graphs = await context.GraphNetworks.Include(q => q.Station1).Include(s => s.Station2).ToListAsync();
            foreach (var item in graphs)
                if (item.Id == id) return item;

            return null;
        }
      
    }
}
