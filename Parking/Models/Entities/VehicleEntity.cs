using System.ComponentModel.DataAnnotations;

namespace Parking.Models.Entities
{
    public class VehicleEntity
    {
        [Key]
        public int VehicleId { get; set; }

        [Required]
        public int UserId { get; set; } // Owner
        public UserEntity User { get; set; }

        [Required, StringLength(10)]
        public string Plate { get; set; }

        [Required, StringLength(10)]
        public string Type { get; set; } // "Car" or "Motorcycle"

        public bool Active { get; set; } = true;

        // Relationships
        public ICollection<ParkingEntity> Parkings { get; set; }
        public ICollection<ReservationEntity> Reservations { get; set; }
    }
}
