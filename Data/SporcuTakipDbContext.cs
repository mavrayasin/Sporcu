using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sporcu.Entity;
using Sporcu.Dtos;

namespace Sporcu.Data;

public partial class SporcuTakipDbContext : DbContext
{
    public SporcuTakipDbContext()
    {
    }

    public SporcuTakipDbContext(DbContextOptions<SporcuTakipDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblIl> TblIls { get; set; }

    public virtual DbSet<TblIlce> TblIlces { get; set; }

    public virtual DbSet<TblSporDali> TblSporDalis { get; set; }

    public virtual DbSet<TblSporcu> TblSporcus { get; set; }

    public virtual DbSet<TblSporcuSporDali> TblSporcuSporDalis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblIl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblIl__3214EC07D1E6F780");

            entity.ToTable("TblIl");

            entity.Property(e => e.Ad).HasMaxLength(100);
        });

        modelBuilder.Entity<TblIlce>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblIlce__3214EC071AE3288C");

            entity.ToTable("TblIlce");

            entity.Property(e => e.Ad).HasMaxLength(100);

            entity.HasOne(d => d.Il).WithMany(p => p.TblIlces)
                .HasForeignKey(d => d.IlId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblIlce__IlId__3E52440B");
        });

        modelBuilder.Entity<TblSporDali>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblSporD__3214EC077184949C");

            entity.ToTable("TblSporDali");

            entity.Property(e => e.Ad).HasMaxLength(100);
        });

        modelBuilder.Entity<TblSporcu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblSporc__3214EC0723998145");

            entity.ToTable("TblSporcu");

            entity.HasIndex(e => e.TcKimlikNo, "UQ__TblSporc__EB0EF14B369C9080").IsUnique();

            entity.Property(e => e.AdSoyad).HasMaxLength(100);
            entity.Property(e => e.Cinsiyet).HasMaxLength(10);
            entity.Property(e => e.TcKimlikNo)
                .HasMaxLength(11)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblSporcuSporDali>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblSporc__3214EC0783ABBFD7");

            entity.ToTable("TblSporcuSporDali");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<Sporcu.Dtos.SporcuSporDaliDTO> SporcuSporDaliDTO { get; set; } = default!;

public DbSet<Sporcu.Dtos.SporDetayCountDTO> SporDetayCountDTO { get; set; } = default!;
}
