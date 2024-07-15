
using JiraAppPractice.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JiraAppPractice.Data.Context;

public class JiraContext : DbContext
{
    public DbSet<Boards> Boards { get; set; }
    public DbSet<Statuses> Statuses { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JiraAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Boards>()
            .HasKey(t => t.Id);
        modelBuilder.Entity<Boards>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Statuses>()
            .HasKey(t => t.Id);
        modelBuilder.Entity<User>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Tasks>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        base.OnModelCreating(modelBuilder);
    }
}

