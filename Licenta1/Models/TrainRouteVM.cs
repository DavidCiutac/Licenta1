using System.ComponentModel.DataAnnotations;

namespace Licenta1.Models
{
    public class TrainRouteVM
    {
        public int Id { get; set; }
        [Display(Name = "Ruta")] 
        public string? RStations { get; set; }
    }
}
