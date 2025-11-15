using Eurodruk.App.Services.Models;

namespace Eurodruk.App.Services;

public interface IAcceptanceService
{
    Task<IReadOnlyList<WorkshopTicket>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task<AcceptanceDecision> SaveDecisionAsync(int ticketId, bool accepted, string notes, string approver, CancellationToken cancellationToken = default);
}
