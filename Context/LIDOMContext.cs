using System;
using System.Collections.Generic;
using LIDOM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LIDOM.Context
{
    public partial class LIDOMContext : IdentityDbContext<IdentityUser>
    {
        public LIDOMContext()
        {
        }

        public LIDOMContext(DbContextOptions<LIDOMContext> options)
            : base(options)
        {
        }
        public virtual DbSet<EstadisticasEquipo> EstadisticasEquipos { get; set; }
        public virtual DbSet<RegisterUser> Administradores { get; set; } = null!;
        public virtual DbSet<Equipo> Equipos { get; set; } = null!;
        public virtual DbSet<Partido> Partidos { get; set; } = null!;
        public virtual DbSet<Temporada> Temporadas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ADALBERTO; Database=LIDOM; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);

            modelBuilder.Entity<RegisterUser>().HasKey(ru => ru.Email);
            modelBuilder.Entity<EstadisticasEquipo>().HasNoKey();

            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.HasKey(e => e.IdEquipo);

                entity.ToTable("equipos");

                entity.Property(e => e.IdEquipo).HasColumnName("id_equipo");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ciudad");

                entity.Property(e => e.Estadio)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estadio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Partido>(entity =>
            {
                entity.HasKey(e => e.IdPartido);

                entity.ToTable("partidos");

                entity.Property(e => e.IdPartido).HasColumnName("id_partido");

                entity.Property(e => e.CarrerasLocal).HasColumnName("carreras_local");

                entity.Property(e => e.CarrerasVisitante).HasColumnName("carreras_visitante");

                entity.Property(e => e.EquipoLocal).HasColumnName("equipo_local");

                entity.Property(e => e.EquipoVisitante).HasColumnName("equipo_visitante");

                entity.Property(e => e.ErroresLocal).HasColumnName("errores_local");

                entity.Property(e => e.ErroresVisitante).HasColumnName("errores_visitante");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.HitsLocal).HasColumnName("hits_local");

                entity.Property(e => e.HitsVisitante).HasColumnName("hits_visitante");

                entity.Property(e => e.IdTemporada).HasColumnName("id_temporada");

                entity.Property(e => e.Resultado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("resultado");

                entity.HasOne(d => d.EquipoLocalNavigation)
                    .WithMany(p => p.PartidoEquipoLocalNavigations)
                    .HasForeignKey(d => d.EquipoLocal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_partidos_equipos_local");

                entity.HasOne(d => d.EquipoVisitanteNavigation)
                    .WithMany(p => p.PartidoEquipoVisitanteNavigations)
                    .HasForeignKey(d => d.EquipoVisitante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_partidos_equipos_visitante");

                entity.HasOne(d => d.IdTemporadaNavigation)
                    .WithMany(p => p.Partidos)
                    .HasForeignKey(d => d.IdTemporada)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_partidos_temporadas");
            });

            modelBuilder.Entity<Temporada>(entity =>
            {
                entity.HasKey(e => e.IdTemporada);

                entity.ToTable("temporadas");

                entity.Property(e => e.IdTemporada).HasColumnName("id_temporada");

                entity.Property(e => e.Año).HasColumnName("año");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin"},
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }

                );
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
