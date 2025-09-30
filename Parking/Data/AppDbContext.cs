using Microsoft.EntityFrameworkCore;
using Parking.Models.Entities;

namespace Parking.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Entidades de la base de datos

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<ParkingEntity> Parkings { get; set; }
        public DbSet<TariffEntity> Tariffs { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<ParkingSpotEntity> ParkingSpots { get; set; }

    }
}
