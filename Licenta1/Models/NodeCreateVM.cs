using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Licenta1.Models
{
    public class NodeCreateVM
    { 
        public int NodeId { get; set; }

        public SelectList? Station { get; set; }
        public int StationId { get; set; }
        public string? Neighbours { get; set; }

       
    }
}
