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
        private readonly IStationsRepository stationsRepository;
        public NodeRepository(ApplicationDbContext context, IMapper mapper, IGraphNetworkRepository networkRepository, IStationsRepository stationsRepository) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.networkRepository = networkRepository;
            this.stationsRepository = stationsRepository;

        }

        public async Task GenerateNeighboursAsync(Node node)
        {
            var name = node.Station.Name;

            var graphs = mapper.Map<List<GraphNetworkVM>>(await networkRepository.GetGraphNetworkAsync1());


            foreach (var item in graphs)
            {
                if (node.Neighbours != null)
                {
                    if (name == item.Station1.Name) node.Neighbours += "-" + item.Station2.Name;
                    if (name == item.Station2.Name) node.Neighbours += "-" + item.Station1.Name;
                }
                else
                {
                    if (name == item.Station1.Name) node.Neighbours += item.Station2.Name;
                    if (name == item.Station2.Name) node.Neighbours += item.Station1.Name;

                }

            }
           

        }




        public async Task<List<Node>> GetNodesAsync()
        {
            var nodes = await context.Nodes.Include(q => q.Station).ToListAsync();
            return nodes;
        }




        public async Task<Node> GetNodeAsync(int? id)
        {
            var nodes = await context.Nodes.Include(q => q.Station).ToListAsync();
            foreach (var node in nodes)
            {

                if (node.Id == id)
                {
                    return node;

                    if (node.Id == id)
                    { 
                        return node;
                    }
                }
            }
            return null;
        }
        public async Task GenerateDatabaseAsync()
        {
            var nodes = await GetAllAsync();
            foreach (var node in nodes)
            {
                await DeleteAsync(node.Id);
            }

            List<Node> nodesList = new List<Node>();
            var stations = await stationsRepository.GetAllAsync();         
            foreach (var station in stations)
            {
                var model = new Node();
                model.Station = station;
                await GenerateNeighboursAsync(model);
                nodesList.Add(model);
            }
            await MultipleAddAsync(nodesList);
            
        }
    }
}
