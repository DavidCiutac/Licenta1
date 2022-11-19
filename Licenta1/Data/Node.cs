using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta1.Data
{
    public class Node
    {
        public int NodeId { get; set; }
        
        [ForeignKey("StationId")]
        public Station Station { get; set; }
        public int StationId { get; set; }
        public string Neighbours { get; set; }
                        
        
    }
}
