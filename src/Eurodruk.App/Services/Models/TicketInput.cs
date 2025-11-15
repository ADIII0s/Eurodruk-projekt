namespace Eurodruk.App.Services.Models;

public record TicketInput(
    string Title,
    string ErrorCode,
    string MachineName,
    string ProductionLine,
    string Location,
    string OperatorName,
    string OperatorDescription
);
