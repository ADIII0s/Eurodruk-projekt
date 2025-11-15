using Eurodruk.App.Services.Models;

namespace Eurodruk.App.Services;

public interface IReportService
{
    Task<MaintenanceReport> CreateReportAsync(ReportInput input, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MaintenanceReport>> GetReportsAsync(CancellationToken cancellationToken = default);
}
