using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta1.Contracts;
using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Repository
{
    public class TrainRouteRepository : GenericRepository<TrainRoute>, ITrainRouteRepository
    {
        private readonly IStationsRepository stationsRepository;
        private readonly ApplicationDbContext context;

        public TrainRouteRepository(ApplicationDbContext context, IStationsRepository stationsRepository) : base(context)
        {
            this.stationsRepository = stationsRepository;
            this.context = context;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// a train route that is about to be created
        /// <returns> the model with an other station</returns>
        public async Task<TrainRouteCreateVM> CreateNeighbourList1(TrainRouteCreateVM model)
        {
            var station = await stationsRepository.GetAsync(model.CurrentStationId);
            if (model.Rute == null)
            {
                model.Rute += station.Name;
            }
            else
            {
                model.Rute += "-" + station.Name;
            }

            IEnumerable<Node> neighbours = await context.Nodes.Include(q => q.Station).ToListAsync();
            foreach (var item in neighbours)
            {
                if (station.Name == item.Station.Name)
                {
                    model.Neighbours = item.Neighbours;
                }
            }
            if (model.Neighbours == null)
                model.Rute = "-";
            string[] words = model.Neighbours.Split('-');

            IEnumerable<Station> stations = await stationsRepository.GetAllAsync();
            List<Station> selectedItems = new List<Station>();

            foreach (var item in words)
            {
                foreach (var station1 in stations)
                {
                    if (item == station1.Name)
                    {
                        selectedItems.Add(station1);
                    }
                }
            }
            model.CurrentStation = new SelectList(selectedItems, "Id", "Name");
            model.CurrentStationId = 0;

            return model;
        }
    }
}
