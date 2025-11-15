using Eurodruk.App.Services.Models;

namespace Eurodruk.App.Services;

public interface IUserTicketService
{
    Task<IReadOnlyList<WorkshopTicket>> GetRecentAsync(int take = 10, CancellationToken cancellationToken = default);
}
