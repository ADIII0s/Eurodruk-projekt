using Eurodruk.App.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Eurodruk.App.Data;

public class WorkshopDbContext : DbContext
{
    public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options) : base(options)
    {
    }

    public DbSet<Machine> Machines => Set<Machine>();
    public DbSet<WorkshopTicket> Tickets => Set<WorkshopTicket>();
    public DbSet<TicketPhoto> TicketPhotos => Set<TicketPhoto>();
    public DbSet<TicketAction> TicketActions => Set<TicketAction>();
    public DbSet<MaintenanceReport> Reports => Set<MaintenanceReport>();
    public DbSet<MaintenanceReportItem> ReportItems => Set<MaintenanceReportItem>();
    public DbSet<AcceptanceDecision> AcceptanceDecisions => Set<AcceptanceDecision>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Machine>()
            .HasIndex(m => m.Name)
            .IsUnique();

        modelBuilder.Entity<WorkshopTicket>()
            .HasMany(t => t.Photos)
            .WithOne(p => p.Ticket!)
            .HasForeignKey(p => p.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkshopTicket>()
            .HasMany(t => t.Actions)
            .WithOne(a => a.Ticket!)
            .HasForeignKey(a => a.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MaintenanceReportItem>()
            .HasOne(i => i.Ticket)
            .WithMany(t => t.ReportItems)
            .HasForeignKey(i => i.TicketId);

        modelBuilder.Entity<AcceptanceDecision>()
            .HasOne(d => d.Ticket)
            .WithMany(t => t.Decisions)
            .HasForeignKey(d => d.TicketId);
    }
}
