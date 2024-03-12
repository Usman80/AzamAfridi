using AzamAfridi.Models;
using Microsoft.EntityFrameworkCore;

namespace AzamAfridi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<RouteDetail> RouteDetails { get; set; }
        public DbSet<StationName> StationNames { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RouteDetail>()
                .HasKey(rd => new { rd.RouteID, rd.BuiltyNo, });
            base.OnModelCreating(modelBuilder);


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StationName>().HasData(new StationName
            {
                StationId = 1,
                StationCode = "LHR",
                StationDescription = "Lahore"
            });
            modelBuilder.Entity<StationName>().HasData(new StationName
            {
                StationId = 2,
                StationCode = "KHI",
                StationDescription = "Karachi"
            });
            modelBuilder.Entity<StationName>().HasData(new StationName
            {
                StationId = 3,
                StationCode = "GUJ",
                StationDescription = "Gujrawala"
            });
            modelBuilder.Entity<StationName>().HasData(new StationName
            {
                StationId = 4,
                StationCode = "MLT",
                StationDescription = "Multan"
            });
            modelBuilder.Entity<StationName>().HasData(new StationName
            {
                StationId = 5,
                StationCode = "PESH",
                StationDescription = "Peshawar"
            });
            modelBuilder.Entity<StationName>().HasData(new StationName
            {
                StationId = 6,
                StationCode = "MUR",
                StationDescription = "Murree"
            });
            modelBuilder.Entity<StationName>().HasData(new StationName
            {
                StationId = 7,
                StationCode = "KHT",
                StationDescription = "Kohat"
            });
            modelBuilder.Entity<StationName>().HasData(new StationName
            {
                StationId = 8,
                StationCode = "SHK",
                StationDescription = "Sheikhapura"
            });
        }
    }
}
