using Microsoft.EntityFrameworkCore;
using Configuration.Domain.Entities;

namespace Configuration.Infrastructure
{

    public class DatabaseContext : DbContext
    {

        #region DbSets

        public DbSet<DeviceConfig> Devices { get; set; }

        //public DbSet<NetworkConfig> Connections { get; set; }

        public DbSet<ComponentConfig> Components { get; set; }

        public DbSet<StreamProfileConfig> StreamProfiles { get; set; }

        #endregion

        #region Ctor

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceConfig>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(m => m.Id).ValueGeneratedOnAdd();
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
                entity.Property(d => d.Protocol).IsRequired().HasMaxLength(50);
                entity.Property(d => d.Version).IsRequired().HasMaxLength(50);
                entity.HasMany(d => d.Components).WithOne(c => c.Device).HasForeignKey(c => c.DeviceId);
            });

            modelBuilder.Entity<ComponentConfig>(entity =>
            {
                entity.HasKey(c => new { c.ComponentId, c.DeviceId });
                entity.Property(m => m.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ComponentConfig>()
            .HasOne(c => c.Device)
            .WithMany(d => d.Components)
            .HasForeignKey(c => c.DeviceId)
            .HasPrincipalKey(d => d.Id);

            modelBuilder.Entity<StreamProfileConfig>(entity =>
            {
                entity.HasKey(sp => new { sp.ComponentId, sp.DeviceId, sp.No });
                entity.Property(m => m.Id).ValueGeneratedOnAdd();
                entity.Property(sp => sp.Name).IsRequired().HasMaxLength(100);
                entity.Property(sp => sp.Url).IsRequired();
            });

            modelBuilder.Entity<StreamProfileConfig>()
            .HasOne(sp => sp.Component)
            .WithMany(c => c.Profiles)
            .HasForeignKey(sp => new { sp.ComponentId, sp.DeviceId })
            .HasPrincipalKey(c => new { c.ComponentId, c.DeviceId });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ConfigDb;Username=postgres;Password=admin;");
            }
        }

        #endregion

    }

}