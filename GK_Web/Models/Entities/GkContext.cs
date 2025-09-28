using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GK_Web.Models.Entities;

public partial class GkContext : DbContext
{
    public GkContext()
    {
    }

    public GkContext(DbContextOptions<GkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDatSach> ChiTietDatSaches { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<DatSach> DatSaches { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDatSach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChiTietD__3214EC073020D09C");

            entity.ToTable("ChiTietDatSach");

            entity.HasOne(d => d.IdDatSachNavigation).WithMany(p => p.ChiTietDatSaches)
                .HasForeignKey(d => d.IdDatSach)
                .HasConstraintName("FK_ChiTietDatSach_DatSach");

            entity.HasOne(d => d.IdSachNavigation).WithMany(p => p.ChiTietDatSaches)
                .HasForeignKey(d => d.IdSach)
                .HasConstraintName("FK_ChiTietDatSach_Sach");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChiTietG__3214EC07EC65A282");

            entity.ToTable("ChiTietGioHang");

            entity.HasOne(d => d.IdGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.IdGioHang)
                .HasConstraintName("FK_ChiTietGioHang_GioHang");

            entity.HasOne(d => d.IdSachNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.IdSach)
                .HasConstraintName("FK_ChiTietGioHang_Sach");
        });

        modelBuilder.Entity<DatSach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DatSach__3214EC07A5A7B2BA");

            entity.ToTable("DatSach");

            entity.Property(e => e.NgayDat).HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(30);
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdTaiKhoanNavigation).WithMany(p => p.DatSaches)
                .HasForeignKey(d => d.IdTaiKhoan)
                .HasConstraintName("FK_DatSach_TaiKhoan");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GioHang__3214EC07E6434E9E");

            entity.ToTable("GioHang");

            entity.HasOne(d => d.IdTaiKhoanNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.IdTaiKhoan)
                .HasConstraintName("FK_GioHang_TaiKhoan");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sach__3214EC07967F4463");

            entity.ToTable("Sach");

            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenSach).HasMaxLength(255);
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaiKhoan__3214EC07FBAE2B5C");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TenTaiKhoan).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
