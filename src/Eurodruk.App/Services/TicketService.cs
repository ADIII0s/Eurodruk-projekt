using Eurodruk.App.Data;
using Eurodruk.App.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Eurodruk.App.Services;

public class TicketService : ITicketService
{
    private readonly WorkshopDbContext _context;

    public TicketService(WorkshopDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<WorkshopTicket>> GetTicketsAsync(TicketFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _context.Tickets
            .Include(t => t.Photos)
            .Include(t => t.Actions)
            .AsQueryable();

        if (filter.Status.HasValue)
        {
            query = query.Where(t => t.Status == filter.Status.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Machine))
        {
            query = query.Where(t => t.MachineName.Contains(filter.Machine));
        }

        if (!string.IsNullOrWhiteSpace(filter.Line))
        {
            query = query.Where(t => t.ProductionLine == filter.Line);
        }

        if (filter.From.HasValue)
        {
            query = query.Where(t => t.CreatedAt >= filter.From.Value);
        }

        if (filter.To.HasValue)
        {
            query = query.Where(t => t.CreatedAt <= filter.To.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Text))
        {
            var text = filter.Text.ToLowerInvariant();
            query = query.Where(t =>
                t.Title.ToLower().Contains(text) ||
                t.OperatorDescription.ToLower().Contains(text) ||
                (t.ServiceDescription != null && t.ServiceDescription.ToLower().Contains(text)));
        }

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<WorkshopTicket?> GetTicketAsync(int ticketId, CancellationToken cancellationToken = default)
    {
        return await _context.Tickets
            .Include(t => t.Photos)
            .Include(t => t.Actions.OrderByDescending(a => a.CreatedAt))
            .FirstOrDefaultAsync(t => t.Id == ticketId, cancellationToken);
    }

    public async Task<WorkshopTicket> CreateTicketAsync(TicketInput input, CancellationToken cancellationToken = default)
    {
        var ticket = new WorkshopTicket
        {
            Title = input.Title,
            ErrorCode = input.ErrorCode,
            MachineName = input.MachineName,
            ProductionLine = input.ProductionLine,
            Location = input.Location,
            OperatorName = input.OperatorName,
            OperatorDescription = input.OperatorDescription,
            CreatedAt = DateTime.UtcNow,
            Status = TicketStatus.Open,
            Actions =
            {
                new TicketAction
                {
                    Author = input.OperatorName,
                    Body = input.OperatorDescription,
                    Type = TicketActionType.OperatorComment
                }
            }
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync(cancellationToken);
        return ticket;
    }

    public async Task StartWorkAsync(int ticketId, string author, CancellationToken cancellationToken = default)
    {
        var ticket = await _context.Tickets.Include(t => t.Actions).FirstOrDefaultAsync(t => t.Id == ticketId, cancellationToken);
        if (ticket is null)
        {
            throw new InvalidOperationException("Ticket not found");
        }

        if (ticket.Status == TicketStatus.InProgress)
        {
            return;
        }

        ticket.Status = TicketStatus.InProgress;
        ticket.StartedAt = DateTime.UtcNow;
        ticket.Actions.Add(new TicketAction
        {
            Author = author,
            Body = "Rozpoczęto pracę",
            Type = TicketActionType.StatusChange
        });

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CloseTicketAsync(int ticketId, string serviceDescription, string author, CancellationToken cancellationToken = default)
    {
        var ticket = await _context.Tickets.Include(t => t.Actions).FirstOrDefaultAsync(t => t.Id == ticketId, cancellationToken);
        if (ticket is null)
        {
            throw new InvalidOperationException("Ticket not found");
        }

        ticket.Status = TicketStatus.Closed;
        ticket.ServiceDescription = serviceDescription;
        ticket.ClosedAt = DateTime.UtcNow;
        ticket.Actions.Add(new TicketAction
        {
            Author = author,
            Body = serviceDescription,
            Type = TicketActionType.ServiceComment
        });

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TicketPhoto> AddPhotoAsync(int ticketId, string base64Image, string? caption, string uploadedBy, CancellationToken cancellationToken = default)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId, cancellationToken);
        if (ticket is null)
        {
            throw new InvalidOperationException("Ticket not found");
        }

        var photo = new TicketPhoto
        {
            TicketId = ticketId,
            Url = base64Image,
            Caption = caption,
            UploadedBy = uploadedBy,
            CreatedAt = DateTime.UtcNow
        };

        _context.TicketPhotos.Add(photo);
        await _context.SaveChangesAsync(cancellationToken);
        return photo;
    }
}
