using Microsoft.AspNetCore.Mvc.Rendering;
using Licenta1.Data;

namespace Licenta1.Models
{
    public class GraphNetworkCreateVM
    {
        public int Id {get;set;}
        public int? Station1_Id { get;set;}
        public  SelectList? Station1 { get; set; }
        public int? Station2_Id { get; set; }
        public SelectList? Station2 { get; set; }
        public string? Name1 { get; set; }
        public string? Name2 { get; set; }
        public int Distance { get; set; }
    }
}
