using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta1.Data
{
    public class GraphNetwork
    {
        public int Id { get; set; }
        [ForeignKey("Station1_Id")]
        public int Station1_Id { get; set; }
        public Station Station1 { get; set; }
        [ForeignKey("Station2_Id")]
        public int Station2_Id { get; set; }
        public Station Station2 { get; set; }
    
     
        public int Distance { get; set; }
    }
}
