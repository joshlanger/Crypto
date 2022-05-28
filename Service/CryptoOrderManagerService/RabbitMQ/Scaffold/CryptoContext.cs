using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DomainModel
{
    public partial class CryptoContext : DbContext
    {
        public CryptoContext()
        {
        }

        public CryptoContext(DbContextOptions<CryptoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TradingPlatform> TradingPlatforms { get; set; }
        public virtual DbSet<TradingPlatformApiendpoint> TradingPlatformApiendpoints { get; set; }
        public virtual DbSet<TradingPlatformInterface> TradingPlatformInterfaces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Crypto;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TradingPlatform>(entity =>
            {
                entity.ToTable("TradingPlatform");

                entity.Property(e => e.TradingPlatformId).HasColumnName("TradingPlatformID");

                entity.Property(e => e.TradingPlatformName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TradingPlatformApiendpoint>(entity =>
            {
                entity.HasKey(e => e.TradingPlatformEndpointId)
                    .HasName("PK__TradingP__A5C0E34BDDACD1B2");

                entity.ToTable("TradingPlatformAPIEndpoint");

                entity.Property(e => e.TradingPlatformEndpointId).HasColumnName("TradingPlatformEndpointID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TradingPlatformInterfaceId).HasColumnName("TradingPlatformInterfaceID");

                entity.Property(e => e.Url)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                entity.HasOne(d => d.TradingPlatformInterface)
                    .WithMany(p => p.TradingPlatformApiendpoints)
                    .HasForeignKey(d => d.TradingPlatformInterfaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TradingPl__Tradi__29572725");
            });

            modelBuilder.Entity<TradingPlatformInterface>(entity =>
            {
                entity.ToTable("TradingPlatformInterface");

                entity.Property(e => e.TradingPlatformInterfaceId).HasColumnName("TradingPlatformInterfaceID");

                entity.Property(e => e.ApibaseUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("APIBaseURL");

                entity.Property(e => e.Apikey)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("APIKey");

                entity.Property(e => e.Apisecret)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("APISecret");

                entity.Property(e => e.TradingPlatformId).HasColumnName("TradingPlatformID");

                entity.HasOne(d => d.TradingPlatform)
                    .WithMany(p => p.TradingPlatformInterfaces)
                    .HasForeignKey(d => d.TradingPlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TradingPl__Tradi__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
