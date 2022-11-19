using System.ComponentModel.DataAnnotations;

namespace Licenta1.Models
{
    public class StationVM
    {
        [Display(Name = "Id Statie")]
        public int Id { get; set; }
        [Display(Name="Nume")]
        [Required]
        public string Name { get; set; }
    }
}
