using AutoMapper;
using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Station, StationVM>().ReverseMap();
            CreateMap<GraphNetwork, GraphNetworkVM>().ReverseMap();
            CreateMap<GraphNetwork, GraphNetworkCreateVM>().ReverseMap();
            CreateMap<GraphNetwork, GraphNetworkDetailsVM>().ReverseMap();
            CreateMap<GraphNetworkVM, GraphNetworkCreateVM>().ReverseMap();

            CreateMap<Node, NodeVM>().ReverseMap();
            CreateMap<Node, NodeCreateVM>().ReverseMap();
            CreateMap<NodeVM, NodeCreateVM>().ReverseMap();
            CreateMap<TrainRoute, TrainRouteVM>().ReverseMap();
            CreateMap<TrainRoute, TrainRouteCreateVM>().ReverseMap();

            

        }
    }
}
