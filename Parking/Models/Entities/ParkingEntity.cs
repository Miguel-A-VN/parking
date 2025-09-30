using Parking.Models.Entities;
using System.ComponentModel.DataAnnotations;

public class ParkingEntity
{
    [Key]
    public int ParkingId { get; set; }

    [Required]
    public int VehicleId { get; set; }
    public VehicleEntity Vehicle { get; set; }

    [Required]
    public int UserId { get; set; }
    public UserEntity User { get; set; }

    // 🔹 Nuevo: espacio asignado
    [Required]
    public int SpotId { get; set; }
    public ParkingSpotEntity Spot { get; set; }

    public DateTime EntryTime { get; set; } = DateTime.Now;
    public DateTime? ExitTime { get; set; }

    [Required, StringLength(10)]
    public string Status { get; set; } // "Inside" o "Outside"

    public double? TotalHours { get; set; }
    public decimal? PaidAmount { get; set; }
}
