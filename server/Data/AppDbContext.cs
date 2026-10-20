using Microsoft.EntityFrameworkCore;
using RequestsManager.Api.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RequestsManager.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // הגדרת Concurrency Token
            modelBuilder.Entity<Ticket>()
                .Property(t => t.RowVersion)
                .IsRowVersion();

            // הוספת אינדקסים (Indexes) לשיפור ביצועי חיפוש וסינון
            modelBuilder.Entity<Ticket>().HasIndex(t => t.Status);
            modelBuilder.Entity<Ticket>().HasIndex(t => t.Priority);
            modelBuilder.Entity<Ticket>().HasIndex(t => t.OrganizationName);
            modelBuilder.Entity<Ticket>().HasIndex(t => t.AssignedTo);
        }
    }
}