using System.ComponentModel.DataAnnotations;

namespace Parking.Models.Entities
{
    public class TariffEntity
    {
        [Key]
        public int TariffId { get; set; }

        [Required, StringLength(10)]
        public string VehicleType { get; set; }

        [Required, StringLength(10)]
        public string Status { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }
    }
}
