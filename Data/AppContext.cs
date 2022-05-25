using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionEmploye.Models;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employe>()
        .HasOne(b => b.Person)
        .WithOne(i => i.Employe)
        .HasForeignKey<Person>(b => b.EmployeId);

        modelBuilder.Entity<Admin>()
        .HasOne(b => b.Person)
        .WithOne(i => i.Admin)
        .HasForeignKey<Person>(b => b.AdminId);

        var conge = modelBuilder.Entity<Conge>();
        conge
            .HasOne(b => b.Employe)
            .WithMany(i => i.Conges)
            .HasForeignKey(b => b.EmployeId);

        conge.Property(b => b.DemandeTime)
        .HasDefaultValueSql("datetime()");

        var salary = modelBuilder.Entity<Salary>();
        salary
        .HasOne(b => b.Employe)
        .WithMany(i => i.Salaries)
        .HasForeignKey(b => b.EmployeId);


    }

    public DbSet<Employe> Employe { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<Admin> Admin { get; set; }

    public DbSet<Conge> Conge { get; set; }

}
