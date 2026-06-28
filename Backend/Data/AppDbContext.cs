using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TrafficStatus> TrafficStatuses { get; set; }
        public DbSet<TrafficConfig> TrafficConfigs { get; set; }
        public DbSet<TrafficLog> TrafficLogs { get; set; }
        public DbSet<ControlLog> ControlLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrafficStatus>(entity =>
            {
                entity.ToTable("traffic_status");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NorthLight).HasColumnName("north_light");
                entity.Property(e => e.SouthLight).HasColumnName("south_light");
                entity.Property(e => e.EastLight).HasColumnName("east_light");
                entity.Property(e => e.WestLight).HasColumnName("west_light");
                entity.Property(e => e.RemainingTime).HasColumnName("remaining_time");
                entity.Property(e => e.Mode).HasColumnName("mode");
                entity.Property(e => e.Phase).HasColumnName("phase");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            modelBuilder.Entity<TrafficConfig>().ToTable("traffic_config");
            modelBuilder.Entity<TrafficLog>().ToTable("traffic_logs");
            modelBuilder.Entity<ControlLog>().ToTable("control_logs");
        }
    }
}