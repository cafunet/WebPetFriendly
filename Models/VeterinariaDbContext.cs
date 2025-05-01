using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebPetFriendly.Models;

public partial class VeterinariaDbContext : DbContext
{
    public VeterinariaDbContext()
    {
    }

    public VeterinariaDbContext(DbContextOptions<VeterinariaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CitaMedica> CitaMedicas { get; set; }

    public virtual DbSet<Mascotum> Mascota { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Veterinarium> Veterinaria { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=CAFUPC\\SQLEXPRESS; Database=BD_Veterinaria; Integrated Security = True; TrustServerCertificate=True; Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CitaMedica>(entity =>
        {
            entity.HasKey(e => e.IdCita);

            entity.ToTable("CitaMedica");

            entity.Property(e => e.IdCita).HasColumnName("Id_cita");
            entity.Property(e => e.Diagnostico).HasMaxLength(100);
            entity.Property(e => e.FechaCita).HasColumnType("datetime");
            entity.Property(e => e.Formula).HasMaxLength(50);
            entity.Property(e => e.Sintomas).HasMaxLength(50);

            entity.HasOne(d => d.Mascota).WithMany(p => p.CitaMedicas)
                .HasForeignKey(d => d.MascotaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CitaMedica_Mascota");

            entity.HasOne(d => d.Medico).WithMany(p => p.CitaMedicas)
                .HasForeignKey(d => d.MedicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CitaMedica_Medico1");
        });

        modelBuilder.Entity<Mascotum>(entity =>
        {
            entity.HasKey(e => e.IdMascota);

            entity.Property(e => e.IdMascota).HasColumnName("Id_mascota");
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Especie).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(30);
            entity.Property(e => e.Propietario).HasMaxLength(50);
            entity.Property(e => e.Raza).HasMaxLength(50);
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.IdMedico);

            entity.ToTable("Medico");

            entity.Property(e => e.IdMedico).HasColumnName("Id_Medico");
            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Cedula).HasMaxLength(15);
            entity.Property(e => e.Ciudad).HasMaxLength(30);
            entity.Property(e => e.Departamento).HasMaxLength(30);
            entity.Property(e => e.Direccion).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Genero).HasMaxLength(2);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Pais).HasMaxLength(30);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        modelBuilder.Entity<Veterinarium>(entity =>
        {
            entity.Property(e => e.Direccion).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Nit).HasMaxLength(15);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
