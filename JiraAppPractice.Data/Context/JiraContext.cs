
using JiraAppPractice.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JiraAppPractice.Data.Context;

public class JiraContext : DbContext
{
    public DbSet<Boards> Boards { get; set; }
    public DbSet<Statuses> Statuses { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<User> Users { get; set; }

    public JiraContext(DbContextOptions<JiraContext> options) : base(options)
    {
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

        modelBuilder.Entity<Tasks>()
           .HasOne(t => t.Statuses)
           .WithMany(s => s.Tasks)
           .HasForeignKey(t => t.StatusId);
        modelBuilder.Entity<Tasks>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.AsigneeId);
        modelBuilder.Entity<Tasks>()
            .HasOne(t => t.Board)
            .WithMany(b => b.Tasks)
            .HasForeignKey(t => t.BoardId);
     

        base.OnModelCreating(modelBuilder);
    }
}

