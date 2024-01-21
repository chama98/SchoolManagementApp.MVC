using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementApp.MVC.Data;

public partial class SchoolManagementDbContext : DbContext
{
    public SchoolManagementDbContext()
    {
    }

    public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Lecturer> Lecturers { get; set; }

    public virtual DbSet<SchoolClass> SchoolClasses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:SchoolManagementDbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC07E1039E5D");

            entity.HasIndex(e => e.Code, "UQ__Courses__A25C5AA786C3F291").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(5);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Enrollme__3214EC07267FE206");

            entity.Property(e => e.Grade).HasMaxLength(2);

            entity.HasOne(d => d.SchoolClass).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.SchoolClassId)
                .HasConstraintName("FK__Enrollmen__Schoo__5629CD9C");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Enrollmen__Stude__5535A963");
        });

        modelBuilder.Entity<Lecturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lecturer__3214EC07170BBE60");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<SchoolClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SchoolCl__3214EC07E17CB2AE");

            entity.HasOne(d => d.Course).WithMany(p => p.SchoolClasses)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__SchoolCla__Cours__52593CB8");

            entity.HasOne(d => d.Lecturer).WithMany(p => p.SchoolClasses)
                .HasForeignKey(d => d.LecturerId)
                .HasConstraintName("FK__SchoolCla__Lectu__5165187F");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC0764D6D221");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
