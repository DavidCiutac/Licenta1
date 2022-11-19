using Licenta1.Data;

namespace Licenta1.Models
{
    public class GraphNetworkVM
    {
        public int Id { get; set; }
        public int? Station1_Id { get; set; }
        public Station Station1 { get; set; }
        public int? Station2_Id { get; set; }
        public Station Station2 { get; set; }
        public string? Name1 { get; set; }
        public string? Name2 { get; set; }
        public int Distance { get; set; }
    }
}
