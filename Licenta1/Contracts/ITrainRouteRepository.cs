using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Contracts
{
    public interface ITrainRouteRepository : IGenericRepository<TrainRoute>
    {
        Task<TrainRouteCreateVM> CreateNeighbourList1(TrainRouteCreateVM model);
    }

    
}
