using Microsoft.AspNetCore.Mvc.Rendering;
using Licenta1.Data;

namespace Licenta1.Models
{
    public class TrainRouteCreateVM
    {
        public int Id { get; set; }
        public int CurrentStationId { get; set; }
        public SelectList? CurrentStation { get; set; }
        public string Rute { get; set; }
        public int IdNode   { get; set; }
        public string Neighbours { get; set; }
      
      
       

    }
}
