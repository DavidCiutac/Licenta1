using Licenta1.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Licenta1.Models
{
    public class GraphNetworkAddNeighbourVM
    {
        public int Id { get; set; }
        public int? Station1_Id { get; set; }
        public StationVM? Station1 { get; set; }
        public int? Station2_Id { get; set; }
        public SelectList? Station2 { get; set; }
        public int Distance { get; set; }
    }
}
