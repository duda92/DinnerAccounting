using System;

namespace UI.Models
{
    public enum GetReportType
    {
        LastWeek = 0,
        LastMonth = 1,
        LastYear = 2,
        CustomPeriod = 3
    }

    public class ReportParams
    {

        public ReportParams()
        {
            GetReportType = GetReportType.LastWeek;
        }

        public GetReportType GetReportType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}