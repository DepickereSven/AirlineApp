using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AirlineApp.Entities
{
    public partial class AirlinesContext : DbContext
    {
        public virtual DbSet<Airlinecodes> Airlinecodes { get; set; }
        public virtual DbSet<Locatie> Locatie { get; set; }
        public virtual DbSet<Opgericht> Opgericht { get; set; }

        public AirlinesContext(DbContextOptions<AirlinesContext> options) : base(options)
        {

        }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-UUQ7FUO\SQLEXPRESS;Initial Catalog=Airlines;Integrated Security=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airlinecodes>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("airlinecodes");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Locatie>(entity =>
            {
                entity.HasKey(e => e.AirlineCode);

                entity.ToTable("locatie");

                entity.Property(e => e.AirlineCode)
                    .HasColumnName("airlineCode")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MainHub)
                    .IsRequired()
                    .HasColumnName("mainHub")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.StaatHoofkwartier)
                    .IsRequired()
                    .HasColumnName("staatHoofkwartier")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.StaatMainHub)
                    .IsRequired()
                    .HasColumnName("staatMainHub")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.StadHoofkwartier)
                    .IsRequired()
                    .HasColumnName("stadHoofkwartier")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.AirlineCodeNavigation)
                    .WithOne(p => p.Locatie)
                    .HasForeignKey<Locatie>(d => d.AirlineCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_locatie_airlinecodes");
            });

            modelBuilder.Entity<Opgericht>(entity =>
            {
                entity.HasKey(e => e.AirlineCode);

                entity.Property(e => e.AirlineCode)
                    .HasColumnName("airlineCode")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Gestopt)
                    .HasColumnName("gestopt")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Opgericht1)
                    .IsRequired()
                    .HasColumnName("opgericht")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.HasOne(d => d.AirlineCodeNavigation)
                    .WithOne(p => p.Opgericht)
                    .HasForeignKey<Opgericht>(d => d.AirlineCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Opgericht_airlinecodes");
            });
        }
    }
}
