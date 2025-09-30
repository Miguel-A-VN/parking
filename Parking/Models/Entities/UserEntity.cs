using System.ComponentModel.DataAnnotations;

namespace Parking.Models.Entities
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required, StringLength(20)]
        public string Role { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;

        // Relationships
        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
