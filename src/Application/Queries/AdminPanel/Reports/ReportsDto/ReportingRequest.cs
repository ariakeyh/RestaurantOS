using Common;
using Common.Exceptions;

namespace Application.Queries.AdminPanel.Reports.ReportsDto;

public abstract record ReportingRequest
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    
    public (DateTime From, DateTime To) GetNormalizedRange()
    {
        if (From is not null && To is not null && To < From)
            throw new BadRequestException(
                ErrorMessages.Reporting_DateRange_Invalid);

        return (
            From ?? DateTime.Today.AddDays(-10),
            To   ?? DateTime.Today.AddDays(1)
        );
    }
};