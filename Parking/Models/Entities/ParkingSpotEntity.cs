using System.ComponentModel.DataAnnotations;

namespace Parking.Models.Entities
{
    public class ParkingSpotEntity
    {
        [Key]
        public int SpotId { get; set; }

        [Required, StringLength(10)]
        public string SpotCode { get; set; }  // Ejemplo: "A1", "B2"

        public bool IsAvailable { get; set; } = true;

        // Relación con ParkingEntity
        public ICollection<ParkingEntity> Parkings { get; set; }
    }
}
