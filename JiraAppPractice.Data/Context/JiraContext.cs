
using JiraAppPractice.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JiraAppPractice.Data.Context;

public class JiraContext : IdentityDbContext<User>
{
    public DbSet<Boards> Boards { get; set; }
    public DbSet<Statuses> Statuses { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
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
        modelBuilder.Entity<Boards>()
            .Property(t => t.Name)
            .HasMaxLength(50);

        modelBuilder.Entity<Statuses>()
            .HasKey(t => t.Id);
        modelBuilder.Entity<Statuses>()
            .Property(t => t.Name)
            .HasMaxLength(25);

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
        modelBuilder.Entity<Tasks>()
            .Property(t => t.Title)
            .HasMaxLength(50);
        modelBuilder.Entity<Tasks>()
            .Property(t => t.Description)
            .HasMaxLength(200);
        
     

        base.OnModelCreating(modelBuilder);
    }
}

