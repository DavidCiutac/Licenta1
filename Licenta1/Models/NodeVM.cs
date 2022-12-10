using Licenta1.Data;
namespace Licenta1.Models
{
    public class NodeVM
    {
        public int Id { get; set; }
        public Station Station { get; set; }
        public int StationId { get; set; }
        public string Neighbours { get; set; }

    }
}
