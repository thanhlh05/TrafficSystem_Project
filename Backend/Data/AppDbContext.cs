using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TrafficStatus> TrafficStatuses { get; set; }
        public DbSet<TrafficConfig> TrafficConfigs { get; set; }
        public DbSet<TrafficLog> TrafficLogs { get; set; }
        public DbSet<ControlLog> ControlLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình Fluent API ánh xạ tên bảng chữ thường tương ứng schema MySQL nếu cần
            modelBuilder.Entity<TrafficStatus>().ToTable("traffic_status");
            modelBuilder.Entity<TrafficConfig>().ToTable("traffic_config");
            modelBuilder.Entity<TrafficLog>().ToTable("traffic_logs");
            modelBuilder.Entity<ControlLog>().ToTable("control_logs");
        }
    }
}