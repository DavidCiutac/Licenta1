using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta1.Models
{
    public class GraphNetworkDetailsVM
    {
        public int Id { get; set; }
        public int Station1_Id { get; set; }
        public int Station2_Id { get; set; }
        public StationVM Station1 { get; set; }
        public StationVM Station2 { get; set; }

        //public string? Name1 { get; set; }
        //public string? Name2 { get; set; }
        public int Distance { get; set; }
    }
}
