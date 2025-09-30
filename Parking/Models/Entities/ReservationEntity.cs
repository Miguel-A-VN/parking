using System.ComponentModel.DataAnnotations;

namespace Parking.Models.Entities
{
    public class ReservationEntity
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }

        public DateTime ReservedAt { get; set; } = DateTime.Now;
        public DateTime ExpiresAt { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; } // "Active", "Expired", "Used"
    }
}
