using Eurodruk.App.Services.Models;

namespace Eurodruk.App.Services;

public interface ITicketService
{
    Task<IReadOnlyList<WorkshopTicket>> GetTicketsAsync(TicketFilter filter, CancellationToken cancellationToken = default);
    Task<WorkshopTicket?> GetTicketAsync(int ticketId, CancellationToken cancellationToken = default);
    Task<WorkshopTicket> CreateTicketAsync(TicketInput input, CancellationToken cancellationToken = default);
    Task StartWorkAsync(int ticketId, string author, CancellationToken cancellationToken = default);
    Task CloseTicketAsync(int ticketId, string serviceDescription, string author, CancellationToken cancellationToken = default);
    Task<TicketPhoto> AddPhotoAsync(int ticketId, string base64Image, string? caption, string uploadedBy, CancellationToken cancellationToken = default);
}
