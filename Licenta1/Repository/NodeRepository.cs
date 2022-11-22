using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Licenta1.Contracts;
using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Repository
{
    public class NodeRepository : GenericRepository<Node>, INodeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IGraphNetworkRepository networkRepository;
        public NodeRepository(ApplicationDbContext context, IMapper mapper, IGraphNetworkRepository networkRepository) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.networkRepository = networkRepository;
        }

        public async void GenerateNeighbours(Node node)
        {
            var name = node.Station.Name;
            var graphs = mapper.Map<List<GraphNetworkVM>>(await networkRepository.GetGraphNetworkAsync1());
            foreach(var item in graphs)
            {
                if(node.Neighbours!=null)
                {
                    if (name == item.Station1.Name) node.Neighbours += "-" + item.Station2.Name;
                    if (name == item.Station2.Name) node.Neighbours += "-" + item.Station1.Name;
                }
                else
                {
                    if (name == item.Station1.Name) node.Neighbours +=  item.Station2.Name;
                    if (name == item.Station2.Name) node.Neighbours +=  item.Station1.Name;
                }
                
            }
          
        }

      

        public async Task<List<Node>> GetNodesAsync1()
        {
            var nodes = await context.Nodes.Include(q => q.Station).ToListAsync();
            var model = mapper.Map<List<Node>>(nodes); 
            return model;
        }

        public async Task<Node> GetNodesAsync2(int? id)
        {
            var nodes = await context.Nodes.Include(q => q.Station).ToListAsync();
            foreach (var node in nodes)
            {
                if(node.Id==id)
                {
                    return node;
                }
            }
            return null;
             
            
        }
    }
}
