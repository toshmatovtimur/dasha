using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace HumanResourcesDepartmentWPFApp.Models;

public partial class OkContext : DbContext
{
    public OkContext()
    {
    }

    public OkContext(DbContextOptions<OkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<SubDivision> SubDivisions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlite(PacHt());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameArea).HasColumnName("nameArea");
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameJobTitle).HasColumnName("nameJobTitle");
            entity.Property(e => e.Salary).HasColumnName("salary");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.ToTable("Personal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adress).HasColumnName("adress");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.ChildrenCount)
                .HasColumnType("INT")
                .HasColumnName("childrenCount");
            entity.Property(e => e.Family).HasColumnName("family");
            entity.Property(e => e.Inn).HasColumnName("inn");
            entity.Property(e => e.JobTitle)
                .HasColumnType("INT")
                .HasColumnName("jobTitle");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.SubDivision)
                .HasColumnType("INT")
                .HasColumnName("subDivision");

            entity.HasOne(d => d.AreaNavigation).WithMany(p => p.Personals).HasForeignKey(d => d.Area);

            entity.HasOne(d => d.JobTitleNavigation).WithMany(p => p.Personals).HasForeignKey(d => d.JobTitle);

            entity.HasOne(d => d.SubDivisionNavigation).WithMany(p => p.Personals).HasForeignKey(d => d.SubDivision);
        });

        modelBuilder.Entity<SubDivision>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameDivisions).HasColumnName("nameDivisions");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    //Относительный путь
    static private string PacHt()
    {
        var x = Directory.GetCurrentDirectory();
        var y = Directory.GetParent(x).FullName;
        var c = Directory.GetParent(y).FullName;
        var r = "Data Source=" + Directory.GetParent(c).FullName + @"\DB\ok.db";
        return r;
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
